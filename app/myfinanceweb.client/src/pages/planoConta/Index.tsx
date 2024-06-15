import React, { useEffect, useState } from "react";
import { Button, Skeleton, Table, Tag, Flex } from "antd";
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
        // setError(error.message);
      } finally {
        setLoading(false);
      }
    };

    fetchPlanosConta();
  }, []);

  const handleCreatePlanoConta = async (planoConta: PlanoContaModel) => {
    try {
      const createdPlanoConta = await createPlanoConta(planoConta);
      console.log("PlanoConta created successfully:", createdPlanoConta);
    } catch (error) {
      console.error("Error creating product:", error);
    }
  };

  const handleUpdatePlanoConta = async (planoConta: PlanoContaModel) => {
    try {
      const createdPlanoConta = await updatePlanoConta(planoConta);
      console.log("PlanoConta created successfully:", createdPlanoConta);
    } catch (error) {
      console.error("Error creating PlanoConta:", error);
    }
  };

  const handleDisableEnablePlanoConta = async (planoConta: PlanoContaModel) => {
    try {
      const createdPlanoConta = await disableEnablePlanoConta(planoConta);
      console.log("PlanoConta created successfully:", createdPlanoConta);
    } catch (error) {
      console.error("Error creating product:", error);
    }
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
    if(modalAction === "Create")
      handleCreatePlanoConta(updatedRecord);
    else if(modalAction === "Update")
      handleUpdatePlanoConta(updatedRecord);
    else if(modalAction === "Delete")
      handleDisableEnablePlanoConta(updatedRecord);

    setIsModalVisible(false);
    setEditingRecord(null);
    setModalAction(undefined);
  };

  return (
    <>
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
