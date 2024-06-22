import React, { useEffect, useState } from "react";
import {
  Button,
  Skeleton,
  Table,
  Tag,
  Flex,
  notification,
  DatePicker,
} from "antd";
import { TransacaoModel } from "../../types/transacao";
import { ColumnsType } from "antd/es/table";
import EditModal from "./EditModal";
import {
  getTransacoes,
  createTransacao,
  removeTransacao,
  updateTransacao,
} from "../../api/transacaoApi";
import { EModalAction } from "../../types/utils";
import { IconType } from "antd/es/notification/interface";
import moment, { Moment } from "moment";

const Transacao: React.FC = () => {
  const [data, setData] = useState<TransacaoModel[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalAction, setModalAction] = useState<EModalAction>();

  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingRecord, setEditingRecord] = useState<TransacaoModel | null>(
    null
  );

  useEffect(() => {
    const fetchPlanosConta = async () => {
      try {
        const PlanosConta = await getTransacoes();
        if (PlanosConta && PlanosConta.data) setData(PlanosConta.data);
      } catch (error) {
        showErroOperacao();
      } finally {
        setLoading(false);
      }
    };

    fetchPlanosConta();
  }, []);

  const handleCreateTransacao = async (trancasao: TransacaoModel) => {
    try {
      const result = await createTransacao(trancasao);
      if (result && result.success) showSucessoOperacao();
      else showErroOperacao();
    } catch (error) {
      showErroOperacao();
    }
  };

  const handleUpdateTransacao = async (trancasao: TransacaoModel) => {
    try {
      const result = await updateTransacao(trancasao);
      if (result && result.success) showSucessoOperacao();
      else showErroOperacao();
    } catch (error) {
      showErroOperacao();
    }
  };

  const handleDisableEnableTransacao = async (trancasao: TransacaoModel) => {
    try {
      const result = await removeTransacao(trancasao);
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
      width: "30%",
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
      width: "15%",
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
      width: "25%",
      render: (text, record) => (
        <Flex wrap gap="small">
          <Button type="primary" onClick={() => handleEdit(record, "Update")}>
            Alterar
          </Button>
          <Button
            danger
            type="primary"
            onClick={() => handleEdit(record, "Delete")}
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
    setModalAction("Create");
    setEditingRecord({} as TransacaoModel);
    setIsModalVisible(true);
  };

  const handleCancel = () => {
    setModalAction(undefined);
    setIsModalVisible(false);
    setEditingRecord(null);
  };

  const handleConfirm = (updatedRecord: TransacaoModel) => {
    console.log({ updatedRecord });
    if (modalAction === "Create") handleCreateTransacao(updatedRecord);
    else if (modalAction === "Update") handleUpdateTransacao(updatedRecord);
    else if (modalAction === "Delete")
      handleDisableEnableTransacao(updatedRecord);

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
        <Table columns={columns} dataSource={data} />
      )}
    </>
  );
};

export default Transacao;
