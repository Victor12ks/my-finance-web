import React from "react";
import { Modal, Form, Input, Radio } from "antd";
import { PlanoContaModel } from "../../types/planoConta";

interface EditModalProps {
  visible: boolean;
  onCancel: () => void;
  onSave: (values: PlanoContaModel) => void;
  initialValues: PlanoContaModel;
}

const EditModal: React.FC<EditModalProps> = ({
  visible,
  onCancel,
  onSave,
  initialValues,
}) => {
  const [form] = Form.useForm();

  const handleSave = () => {
    form
      .validateFields()
      .then((values) => {
        form.resetFields();
        onSave({ ...initialValues, ...values });
      })
      .catch((info) => {
        console.log("Validate Failed:", info);
      });
  };

  return (
    <Modal
      visible={visible}
      title="Edit Record"
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
  );
};

export default EditModal;
