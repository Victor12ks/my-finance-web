import React, { useEffect, useState } from "react";
import { Table, Button, Modal, Tag, Flex } from "antd";
import { ColumnsType } from "antd/es/table";
import { PlanoContaModel } from "../../types/planoConta";

type ActionModal = "Update" | "Delete";

interface TableResultProps {
  planoConta: PlanoContaModel[];
  onChange: (action: ActionModal, model: PlanoContaModel) => void;
}

const TableResult = ({ planoConta, onChange }: TableResultProps) => {
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [selectedRecord, setSelectedRecord] = useState<PlanoContaModel | null>(
    null
  );

  // const showModal = (record: PlanoContaModel) => {
  //   setSelectedRecord(record);
  //   setIsModalVisible(true);
  // };

  // const handleOk = () => {
  //   setIsModalVisible(false);
  //   setSelectedRecord(null);
  // };

  // const handleCancel = () => {
  //   setIsModalVisible(false);
  //   setSelectedRecord(null);
  // };

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
          <Button type="primary" onClick={() => onChange("Update", record)}>
            Alterar
          </Button>
          <Button
            danger
            type="primary"
            onClick={() => onChange("Delete", record)}
          >
            Desativar
          </Button>
        </Flex>
      ),
    },
  ];

  return (
    <>
      <Table columns={columns} dataSource={planoConta} />
      {/* <Modal
        title="Record Details"
        open={isModalVisible}
        onOk={handleOk}
        onCancel={handleCancel}
      >
        {selectedRecord && (
          <div>
            <p>Código: {selectedRecord.id}</p>
            <p>Descrição: {selectedRecord.descricao}</p>
            <p>Tipo: {selectedRecord.tipo}</p>
          </div>
        )}
      </Modal> */}
    </>
  );
};

export default TableResult;
