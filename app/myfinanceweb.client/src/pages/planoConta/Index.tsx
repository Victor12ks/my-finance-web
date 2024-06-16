import React, { useEffect, useState } from "react";
import { Button, Skeleton, Table, Tag, Flex, notification } from "antd";
import { PlanoContaModel } from "../../types/planoConta";
import { ColumnsType } from "antd/es/table";
import "./styles.css";
import EditModal from "./EditModal";
import {
  getPlanosConta,
  createPlanoConta,
  disableEnablePlanoConta,
  updatePlanoConta,
} from "../../api/planoContaApi";
import { EModalAction } from "../../types/utils";
import { IconType } from "antd/es/notification/interface";

const PlanoConta: React.FC = () => {
  const [data, setData] = useState<PlanoContaModel[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalAction, setModalAction] = useState<EModalAction>();

  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingRecord, setEditingRecord] = useState<PlanoContaModel | null>(
    null
  );

  useEffect(() => {
    const fetchPlanosConta = async () => {
      try {
        const PlanosConta = await getPlanosConta();
        if (PlanosConta && PlanosConta.data) setData(PlanosConta.data);
      } catch (error) {
        showErroOperacao();
      } finally {
        setLoading(false);
      }
    };

    fetchPlanosConta();
  }, []);

  const handleCreatePlanoConta = async (planoConta: PlanoContaModel) => {
    try {
      const result = await createPlanoConta(planoConta);
      if (result && result.success) showSucessoOperacao();
      else showErroOperacao();
    } catch (error) {
      showErroOperacao();
    }
  };

  const handleUpdatePlanoConta = async (planoConta: PlanoContaModel) => {
    try {
      const result = await updatePlanoConta(planoConta);
      if (result && result.success) showSucessoOperacao();
      else showErroOperacao();
    } catch (error) {
      showErroOperacao();
    }
  };

  const handleDisableEnablePlanoConta = async (planoConta: PlanoContaModel) => {
    try {
      const result = await disableEnablePlanoConta(planoConta);
      if (result && result.success) showSucessoOperacao();
      else showErroOperacao();
    } catch (error) {
      showErroOperacao();
    }
  };

  const showErroOperacao = () => {
    openNotification(
      "Ops...",
      "Ocorreu um erro ao realizar a sua operação.",
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
      render: (text, record) => (
        <Flex wrap gap="small">
          <Button
            disabled={!record.ativo}
            type="primary"
            onClick={() => handleEdit(record, "Update")}
          >
            Alterar
          </Button>
          {!record.ativo ? (
            <Button
              style={{ backgroundColor: "green", color: "white" }}
              type="primary"
              onClick={() => handleEdit(record, "Delete")}
            >
              Ativar
            </Button>
          ) : (
            <Button
              danger
              type="primary"
              onClick={() => handleEdit(record, "Delete")}
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
    setModalAction("Create");
    setEditingRecord({} as PlanoContaModel);
    setIsModalVisible(true);
  };

  const handleCancel = () => {
    setModalAction(undefined);
    setIsModalVisible(false);
    setEditingRecord(null);
  };

  const handleConfirm = (updatedRecord: PlanoContaModel) => {
    if (modalAction === "Create") handleCreatePlanoConta(updatedRecord);
    else if (modalAction === "Update") handleUpdatePlanoConta(updatedRecord);
    else if (modalAction === "Delete")
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
      <Button type="primary" onClick={handleAdd}>
        Adicionar
      </Button>
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
