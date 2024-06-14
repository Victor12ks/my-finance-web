// src/PlanoConta.tsx
import React, { useEffect, useState } from "react";
import { Button, Skeleton, Table, Tag, Flex } from "antd";
import axios from "axios";
import { PlanoContaModel } from "../../types/planoConta";
import { ColumnsType } from "antd/es/table";
import "./styles.css";
import EditModal from "./EditModal";
import { getPlanosConta, createPlanoConta } from "../../api/planoContaApi";

const PlanoConta: React.FC = () => {
  const [data, setData] = useState<PlanoContaModel[]>([]);
  const [loading, setLoading] = useState(true);

  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingRecord, setEditingRecord] = useState<PlanoContaModel | null>(
    null
  );

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const products = await getPlanosConta();
        if (products && products.data) setData(products.data);
      } catch (error) {
        // setError(error.message);
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
  }, []);

  const handleCreateProduct = async (planoConta: PlanoContaModel) => {
    try {
      const createdProduct = await createPlanoConta(planoConta);
      console.log("Product created successfully:", createdProduct);
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
            onClick={() => handleEdit(record)}
          >
            Alterar
          </Button>
          {!record.ativo ? (
            <Button
              style={{ backgroundColor: "green", color: "white" }}
              type="primary"
              onClick={() => handleEdit(record)}
            >
              Ativar
            </Button>
          ) : (
            <Button danger type="primary" onClick={() => handleEdit(record)}>
              Desativar
            </Button>
          )}
        </Flex>
      ),
    },
  ];

  const handleEdit = (record: PlanoContaModel) => {
    setEditingRecord(record);
    setIsModalVisible(true);
  };

  const handleAdd = () => {
    setEditingRecord({} as PlanoContaModel);
    setIsModalVisible(true);
  };

  const handleCancel = () => {
    setIsModalVisible(false);
    setEditingRecord(null);
  };

  const handleSave = (updatedRecord: PlanoContaModel) => {
    if (updatedRecord?.id) console.log("EDITAR");
    else createPlanoConta(updatedRecord);

    setIsModalVisible(false);
    setEditingRecord(null);
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
          onSave={handleSave}
          initialValues={editingRecord}
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
