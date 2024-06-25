import React, { useCallback, useEffect, useState } from "react";
import { Button, Skeleton, Table, Tag, Flex, notification } from "antd";
import { PlanoContaModel } from "../../types/planoConta";
import { ColumnsType } from "antd/es/table";
import "./styles.css";
import EditModal from "./EditModal";
import * as apiPlanoConta from "../../api/planoContaApi";
import { EModalAction } from "../../types/utils";
import { IconType } from "antd/es/notification/interface";
import {
  PlusOutlined,
  CheckOutlined,
  CloseOutlined,
  EditOutlined,
} from "@ant-design/icons";
import { ResponseBase } from "../../types/reponse";

const PlanoConta: React.FC = () => {
  const [data, setData] = useState<PlanoContaModel[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalAction, setModalAction] = useState<EModalAction>();

  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingRecord, setEditingRecord] = useState<PlanoContaModel | null>(
    null
  );

  const fetchPlanosConta = useCallback(async () => {
    try {
      const PlanosConta = await apiPlanoConta.getPlanosConta();
      if (PlanosConta && PlanosConta.data) setData(PlanosConta.data);
    } catch (error) {
      showErroOperacao();
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchPlanosConta();
  }, []);

  const handleCreatePlanoConta = async (planoConta: PlanoContaModel) => {
    try {
      const result = await apiPlanoConta.createPlanoConta(planoConta);
      showMessageResponse(result);
    } catch (error) {
      showErroOperacao();
    }
  };

  const handleUpdatePlanoConta = async (planoConta: PlanoContaModel) => {
    try {
      const result = await apiPlanoConta.updatePlanoConta(planoConta);
      showMessageResponse(result);
    } catch (error) {
      showErroOperacao();
    }
  };

  const handleDisableEnablePlanoConta = async (planoConta: PlanoContaModel) => {
    try {
      const result = await apiPlanoConta.disableEnablePlanoConta(planoConta);
      showMessageResponse(result);
    } catch (error) {
      showErroOperacao();
    }
  };

  const showErroOperacao = (message?: string) => {
    openNotification(
      "Ops...",
      message ?? "Ocorreu um erro ao realizar a sua operação.",
      "error"
    );
  };

  const showSucessoOperacao = () => {
    openNotification(
      "Legal :)",
      "Sua operação foi realizada com sucesso.",
      "success"
    );
  };

  const showMessageResponse = (response: ResponseBase<any>) => {
    if (response.success) showSucessoOperacao();
    else showErroOperacao(response?.message);
    fetchPlanosConta();
  };
  const getRowClassName = (record: PlanoContaModel) => {
    return !record.ativo ? "even-row" : "odd-row";
  };

  const columns: ColumnsType<PlanoContaModel> = [
    {
      title: "Código",
      dataIndex: "id",
      key: "id",
      width: "10%",
    },
    {
      title: "Descrição",
      dataIndex: "descricao",
      key: "descricao",
      width: "40%",
    },
    {
      title: "Tipo",
      key: "tipo",
      dataIndex: "tipo",
      width: "25%",
      render: (_, { tipo, corTag }) => (
        <>
          <Tag color={corTag} key={tipo}>
            {tipo}
          </Tag>
        </>
      ),
    },
    {
      title: "Action",
      key: "action",
      width: "25%",
      render: (record) => (
        <Flex wrap gap="small">
          <Button
            icon={<EditOutlined />}
            disabled={!record.ativo}
            type="primary"
            onClick={() => handleEdit(record, EModalAction.Update)}
          >
            Alterar
          </Button>
          {!record.ativo ? (
            <Button
              icon={<CheckOutlined />}
              style={{ backgroundColor: "green", color: "white" }}
              type="primary"
              onClick={() => handleEdit(record, EModalAction.Delete)}
            >
              Ativar
            </Button>
          ) : (
            <Button
              icon={<CloseOutlined />}
              danger
              type="primary"
              onClick={() => handleEdit(record, EModalAction.Delete)}
            >
              Desativar
            </Button>
          )}
        </Flex>
      ),
    },
  ];

  const handleEdit = (record: PlanoContaModel, action: EModalAction) => {
    setModalAction(action);
    setEditingRecord(record);
    setIsModalVisible(true);
  };

  const handleAdd = () => {
    setModalAction(EModalAction.Create);
    setEditingRecord({} as PlanoContaModel);
    setIsModalVisible(true);
  };

  const handleCancel = () => {
    setModalAction(undefined);
    setIsModalVisible(false);
    setEditingRecord(null);
  };

  const handleConfirm = (updatedRecord: PlanoContaModel) => {
    if (modalAction === EModalAction.Create)
      handleCreatePlanoConta(updatedRecord);
    else if (modalAction === EModalAction.Update)
      handleUpdatePlanoConta(updatedRecord);
    else if (modalAction === EModalAction.Delete)
      handleDisableEnablePlanoConta(updatedRecord);

    setIsModalVisible(false);
    setEditingRecord(null);
    setModalAction(undefined);
  };
  const [api, contextHolder] = notification.useNotification();

  const openNotification = (
    title: string,
    description: string,
    type: IconType
  ) => {
    api.open({
      message: title,
      description: description,
      duration: 3,
      type: type,
    });
  };

  return (
    <>
      {contextHolder}
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          marginBottom: "16px",
        }}
      >
        <div>{}</div>
        <Button
          size="large"
          type="primary"
          onClick={handleAdd}
          icon={<PlusOutlined />}
        >
          Novo
        </Button>
      </div>
      {editingRecord && (
        <EditModal
          visible={isModalVisible}
          onCancel={handleCancel}
          onConfirm={handleConfirm}
          initialValues={editingRecord}
          action={modalAction}
        />
      )}
      {loading ? (
        <Skeleton active />
      ) : (
        <Table
          columns={columns}
          dataSource={data}
          rowClassName={getRowClassName}
        />
      )}
    </>
  );
};

export default PlanoConta;
