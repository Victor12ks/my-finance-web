import React, { useEffect, useState } from "react";
import {
  Modal,
  Form,
  Input,
  Button,
  Select,
  DatePicker,
  InputNumber,
  notification,
} from "antd";
import { TransacaoModel } from "../../types/transacao";
import { EModalAction } from "../../types/utils";
import { PlanoContaModel } from "../../types/planoConta";
import { getPlanosConta } from "../../api/planoContaApi";
import locale from "antd/lib/date-picker/locale/pt_BR";
import dayjs from "dayjs";

interface EditModalProps {
  visible: boolean;
  onCancel: () => void;
  onConfirm: (values: TransacaoModel) => void;
  initialValues: TransacaoModel;
  action?: EModalAction;
}

const EditModal: React.FC<EditModalProps> = ({
  visible,
  onCancel,
  onConfirm,
  initialValues,
  action,
}) => {
  const [planosConta, setPlanosConta] = useState<PlanoContaModel[]>([]);
  const [api] = notification.useNotification();
  const [form] = Form.useForm();
  const { Option } = Select;

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

  useEffect(() => {
    const fetchPlanosConta = async () => {
      try {
        const PlanosConta = await getPlanosConta();
        if (PlanosConta && PlanosConta.data) setPlanosConta(PlanosConta.data);
      } catch (error) {
        api.open({
          message: "Ops...",
          description: "Ocorreu um erro ao realizar a sua operação.",
          duration: 3,
          type: "error",
        });
      }
    };
    fetchPlanosConta();
  }, []);

  return (
    <>
      {action === EModalAction.Delete ? (
        <Modal
          title={"Excluir"}
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
              Excluir
            </Button>,
          ]}
        >
          <p>
            Tem certeza que deseja Excluir{" "}
            <strong>{initialValues.historico}</strong> ?
          </p>
        </Modal>
      ) : (
        <Modal
          open={visible}
          title="Transação"
          onCancel={onCancel}
          onOk={handleSave}
        >
          <Form
            form={form}
            layout="vertical"
            initialValues={{
              ...initialValues,
              dataHora: initialValues.dataHora
                ? dayjs(initialValues.dataHora, "YYYY-MM-DD HH:mm")
                : dayjs(),
            }}
          >
            {initialValues.codigo && (
              <Form.Item label="Código" name="codigo">
                {initialValues.codigo}
              </Form.Item>
            )}
            <Form.Item
              label="Descrição"
              name="historico"
              rules={[
                {
                  required: true,
                  message: "A descrição da transação deve ser informada.",
                },
              ]}
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="Data"
              name="dataHora"
              rules={[
                {
                  required: true,
                  message: "A data da transação deve ser informada.",
                },
              ]}
            >
              <DatePicker
                showTime
                format="DD/MM/YYYY HH:mm"
                locale={locale}
                placeholder="Selecione a data da Transação"
              />
            </Form.Item>
            <Form.Item
              label="Valor"
              name="valor"
              rules={[
                {
                  required: true,
                  message: "O valor da transação deve ser informada.",
                },
              ]}
            >
              <InputNumber<number>
                prefix="R$"
                formatter={(value) =>
                  `${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ",")
                }
                style={{ width: "100%" }}
              />
            </Form.Item>
            {planosConta && planosConta.length > 0 && (
              <Form.Item
                label="Tipo"
                name="planoContaId"
                rules={[
                  {
                    required: true,
                    message: "O tipo de transação deve ser informado.!",
                  },
                ]}
              >
                <Select
                  placeholder="Selecione o tipo de Transação"
                  defaultValue={
                    planosConta.find((p) => p.id == initialValues.planoContaId)
                      ?.id
                  }
                >
                  {planosConta.map((planoConta) => (
                    <Option key={planoConta.id} value={planoConta.id}>
                      {planoConta.descricao}
                    </Option>
                  ))}
                </Select>
              </Form.Item>
            )}
          </Form>
        </Modal>
      )}
    </>
  );
};
export default EditModal;
