import React from "react";
import { Modal, Form, Input, Radio, Button } from "antd";
import { PlanoContaModel } from "../../types/planoConta";
import { EModalAction } from "../../types/utils";

interface EditModalProps {
  visible: boolean;
  onCancel: () => void;
  onConfirm: (values: PlanoContaModel) => void;
  initialValues: PlanoContaModel;
  action?: EModalAction;
}

const EditModal: React.FC<EditModalProps> = ({
  visible,
  onCancel,
  onConfirm,
  initialValues,
  action,
}) => {
  const [form] = Form.useForm();

  const handleSave = () => {
    form
      .validateFields()
      .then((values) => {
        form.resetFields();
        onConfirm({ ...initialValues, ...values });
      })
      .catch((info) => {
        console.log("Validate Failed:", info);
      });
  };

  return (
    <>
      {action === EModalAction.Delete ? (
        <Modal
          title={initialValues.ativo ? "Desativar" : "Ativar"}
          open={visible}
          onOk={() => onConfirm(initialValues)}
          onCancel={onCancel}
          footer={[
            <Button key="cancel" onClick={onCancel}>
              Cancelar
            </Button>,
            <Button
              key="confirm"
              type="primary"
              onClick={() => onConfirm(initialValues)}
            >
              {initialValues.ativo ? "Desativar" : "Ativar"}
            </Button>,
          ]}
        >
          <p>
            Tem certeza que deseja{" "}
            {initialValues.ativo ? "DESATIVAR" : "ATIVAR"} o tipo{" "}
            <strong>{initialValues.descricao}</strong>?
          </p>
        </Modal>
      ) : (
        <Modal
          open={visible}
          title="Plano Conta"
          onCancel={onCancel}
          onOk={handleSave}
        >
          <Form form={form} layout="vertical" initialValues={initialValues}>
            <Form.Item
              name="descricao"
              label="Descrição"
              rules={[
                {
                  required: true,
                  message: "A descrição do tipo de conta deve ser informada.",
                },
              ]}
            >
              <Input />
            </Form.Item>

            <Form.Item
              label="Tipo"
              name="tipo"
              rules={[
                {
                  required: true,
                  message: "O tipo de conta deve ser informado.",
                },
              ]}
            >
              <Radio.Group>
                <Radio value="Receita">Receita</Radio>
                <Radio value="Despesa">Despesa</Radio>
              </Radio.Group>
            </Form.Item>
          </Form>
        </Modal>
      )}
    </>
  );
};

export default EditModal;
