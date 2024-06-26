import React, { useCallback, useEffect, useState } from "react";
import { Button, Skeleton, Table, Tag, Flex, notification, Alert } from "antd";
import { TransacaoModel } from "../../types/transacao";
import { ColumnsType, TablePaginationConfig } from "antd/es/table";
import EditModal from "./EditModal";
import * as apiTransacao from "../../api/transacaoApi";
import { EModalAction } from "../../types/utils";
import { IconType } from "antd/es/notification/interface";
import moment from "moment";
import { ResponseBase } from "../../types/reponse";
import { PlusOutlined, EditOutlined, DeleteOutlined } from "@ant-design/icons";
import { hasAnyPlanoConta } from "../../api/planoContaApi";

const Transacao: React.FC = () => {
  const [data, setData] = useState<TransacaoModel[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalAction, setModalAction] = useState<EModalAction>();
  const [hasPlanoConta, setHasPlanoConta] = useState(true);

  const [isModalVisible, setIsModalVisible] = useState(false);
  const [pagination, setPagination] = useState<TablePaginationConfig>({
    current: 1,
    pageSize: 10,
    total: data.length,
  });
  const [editingRecord, setEditingRecord] = useState<TransacaoModel | null>(
    null
  );

  const [api, contextHolder] = notification.useNotification();

  const fetchTransacoes = useCallback(async () => {
    try {
      const planosConta = await apiTransacao.getTransacoes();
      if (planosConta && planosConta.data) setData(planosConta.data);
    } catch (error) {
      showErroOperacao();
    } finally {
      setLoading(false);
    }
  }, []);

  const fetchPlanoConta = useCallback(async () => {
    try {
      const anyPlanoConta = await hasAnyPlanoConta();
      if (anyPlanoConta) setHasPlanoConta(anyPlanoConta.data as boolean);
    } catch (error) {
      showErroOperacao();
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchPlanoConta();
    fetchTransacoes();
  }, []);

  const handleCreateTransacao = async (trancasao: TransacaoModel) => {
    try {
      const result = await apiTransacao.createTransacao(trancasao);
      showMessageResponse(result);
    } catch (error) {
      showErroOperacao();
    }
  };

  const handleUpdateTransacao = async (trancasao: TransacaoModel) => {
    try {
      const result = await apiTransacao.updateTransacao(trancasao);
      showMessageResponse(result);
    } catch (error) {
      showErroOperacao();
    }
  };

  const handleDisableEnableTransacao = async (trancasao: TransacaoModel) => {
    try {
      const result = await apiTransacao.removeTransacao(trancasao);
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

    fetchTransacoes();
  };

  const columns: ColumnsType<TransacaoModel> = [
    {
      title: "Código",
      dataIndex: "codigo",
      key: "codigo",
      width: "10%",
    },
    {
      title: "Descrição",
      dataIndex: "historico",
      key: "historico",
      width: "28%",
    },
    {
      title: "Data",
      dataIndex: "dataHora",
      key: "dataHora",
      width: "15%",
      render: (date) => moment(date).format("DD/MM/YYYY HH:mm"),
    },
    {
      title: "Tipo",
      key: "tipo",
      dataIndex: "tipo",
      width: "15%",
      render: (_, { planoConta }) => (
        <>
          <Tag color={planoConta.corTag} key={planoConta.id}>
            {planoConta.descricao}
          </Tag>
        </>
      ),
    },
    {
      title: "Valor",
      dataIndex: "valor",
      key: "valor",
      width: "13%",
      render: (value: number) => {
        return new Intl.NumberFormat("pt-BR", {
          style: "currency",
          currency: "BRL",
        }).format(value);
      },
    },
    {
      title: "Action",
      key: "action",
      width: "29%",
      render: (record) => (
        <Flex wrap gap="small">
          <Button
            icon={<EditOutlined />}
            type="primary"
            onClick={() => handleEdit(record, EModalAction.Update)}
          >
            Alterar
          </Button>
          <Button
            icon={<DeleteOutlined />}
            danger
            type="primary"
            onClick={() => handleEdit(record, EModalAction.Delete)}
          >
            Excluir
          </Button>
        </Flex>
      ),
    },
  ];

  const handleEdit = (record: TransacaoModel, action: EModalAction) => {
    setModalAction(action);
    setEditingRecord(record);
    setIsModalVisible(true);
  };

  const handleAdd = () => {
    setModalAction(EModalAction.Create);
    setEditingRecord({} as TransacaoModel);
    setIsModalVisible(true);
  };

  const handleCancel = () => {
    setModalAction(undefined);
    setIsModalVisible(false);
    setEditingRecord(null);
  };

  const handleConfirm = (updatedRecord: TransacaoModel) => {
    if (modalAction === EModalAction.Create)
      handleCreateTransacao(updatedRecord);
    else if (modalAction === EModalAction.Update)
      handleUpdateTransacao(updatedRecord);
    else if (modalAction === EModalAction.Delete)
      handleDisableEnableTransacao(updatedRecord);

    setIsModalVisible(false);
    setEditingRecord(null);
    setModalAction(undefined);
  };

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

  const handleTableChange = (newPagination: TablePaginationConfig) => {
    setPagination(newPagination);
  };

  return (
    <>
      {!hasPlanoConta && (
        <Alert
          showIcon
          message="Atenção"
          description="Vi aqui que você ainda não cadastrou nenhum tipo de transação :( faça o cadastro no menu acima (Tipo Transações) para começar lançar suas transações."
          type="warning"
        />
      )}
      <br></br>
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
          disabled={!hasPlanoConta}
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
          pagination={pagination}
          onChange={handleTableChange}
          columns={columns}
          dataSource={data}
        />
      )}
    </>
  );
};

export default Transacao;
