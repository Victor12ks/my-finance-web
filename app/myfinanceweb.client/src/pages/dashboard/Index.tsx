import { useEffect, useState } from "react";
import { Chart } from "react-google-charts";
import { Card, Col, Divider, Row, Statistic } from "antd";
import { getDashboard } from "../../api/dashboardApi";
import SelectDate from "./SelectDate";
import { DataChart } from "../../types/dashboard";
import { ArrowUpOutlined, ArrowDownOutlined } from "@ant-design/icons";

const Dashboard = () => {
  const [transacoesTipo, setTransacoesTipo] = useState<any>([]);
  const [transacoesTipoContaDespesa, setTransacoesTipoContaDespesa] =
    useState<any>([]);
  const [transacoesTipoContaReceita, setTransacoesTipoContaReceita] =
    useState<any>([]);
  const [despesasMes, setDespesasMes] = useState<any>([]);
  const [receitasMes, setReceitasMes] = useState<any>([]);
  const [datasConsulta, setDatasConsulta] = useState<{
    dataInicial: string;
    dataFinal: string;
  }>();

  const [maiorReceita, setMaiorReceita] = useState<DataChart>();
  const [maiorDespesa, setMaiorDespesa] = useState<DataChart>();

  const onHandlerSelectDate = (datas: {
    dataInicial: string;
    dataFinal: string;
  }) => {
    setDatasConsulta(datas);
  };

  const setDadosTransacoesTipo = (dados: DataChart[]) => {
    const dadosFormatados = [["Tipo Transação", "Valor em R$"]];
    dados?.forEach((item: any) => {
      dadosFormatados.push([item.descricao, parseFloat(item.valor)]);
    });
    setTransacoesTipo(dadosFormatados);
  };

  const setDadosTransacoesTipoContaDespesa = (dados: DataChart[]) => {
    const dadosFormatados = [["Despesas", "Valor em R$"]];
    dados?.forEach((item: any) => {
      dadosFormatados.push([item.descricao, parseFloat(item.valor)]);
    });
    setTransacoesTipoContaDespesa(dadosFormatados);
  };

  const setDadosTransacoesTipoContaReceita = (dados: DataChart[]) => {
    const dadosFormatados = [["Receitas", "Valor em R$"]];
    dados?.forEach((item: any) => {
      dadosFormatados.push([item.descricao, parseFloat(item.valor)]);
    });
    setTransacoesTipoContaReceita(dadosFormatados);
  };

  const setDadosReceitasMes = (dados: DataChart[]) => {
    const dadosFormatados = [["Mês/Ano", "Valor em R$"]];
    dados?.forEach((item: any) => {
      dadosFormatados.push([item.descricao, parseFloat(item.valor)]);
    });
    setReceitasMes(dadosFormatados);
  };

  const setDadosDespesasMes = (dados: DataChart[]) => {
    const dadosFormatados = [["Mês/Ano", "Valor em R$"]];
    dados?.forEach((item: any) => {
      dadosFormatados.push([item.descricao, parseFloat(item.valor)]);
    });
    setDespesasMes(dadosFormatados);
  };

  useEffect(() => {
    handleUpdateTransacao();
  }, [datasConsulta]);

  const handleUpdateTransacao = async () => {
    if (datasConsulta) {
      try {
        const result = await getDashboard(
          datasConsulta.dataInicial,
          datasConsulta.dataFinal
        );
        if (result && result.data) {
          setDadosDespesasMes(result.data.despesasPorMes);
          setDadosReceitasMes(result.data.receitasPorMes);
          setDadosTransacoesTipo(result.data.transacoesTipo);
          setDadosTransacoesTipoContaReceita(
            result.data.transacoesTipoContaReceita
          );
          setDadosTransacoesTipoContaDespesa(
            result.data.transacoesTipoContaDespesa
          );
          setMaiorReceita(result.data.maiorReceita);
          setMaiorDespesa(result.data.maiorDespesa);
        }
        // showMessageResponse(result);
      } catch (error) {
        // showErroOperacao();
      }
    }
  };

  const getColors = () => {
    return [
      "#397367",
      "#4EA099",
      "#63CCCA",
      "#60B8B2",
      "#5DA399",
      "#dab712",
      "#42858C",
      "#3C5F64",
      "#35393C",
      "#60b17d",
      "#474B4E",
      "#256e73",
      "#188c6b",
      "#097d74",
      "#91b74b",
      "#0d876f",
      "#77ce7d",
      "#95ca5a",
    ].sort(() => Math.random() - 0.5);
  };

  return (
    <div>
      <SelectDate onSelect={onHandlerSelectDate}></SelectDate>
      <br></br>
      <br></br>
      {""}
      <Row gutter={16}>
        <Col span={12}>
          <Card bordered={false}>
            <Statistic
              title={`Maior Receita - ${maiorReceita?.descricao}`}
              value={maiorReceita?.valor}
              precision={2}
              valueStyle={{ color: "#3f8600" }}
              suffix={<ArrowUpOutlined />}
              prefix="R$ "
            />
          </Card>
        </Col>
        <Col span={12}>
          <Card bordered={false}>
            <Statistic
              title={`Maior Despesa - ${maiorDespesa?.descricao}`}
              value={maiorDespesa?.valor}
              precision={2}
              valueStyle={{ color: "#cf1322" }}
              suffix={<ArrowDownOutlined />}
              prefix="R$ "
            />
          </Card>
        </Col>
      </Row>
      <Divider />
      <Row gutter={16}>
        <Col span={8}>
          <Chart
            width={"100%"}
            height={"400px"}
            chartType="PieChart"
            loader={<div>Carregando Gráfico...</div>}
            data={transacoesTipo}
            options={{
              colors: getColors(),
              title: "Balanço",
              chartArea: { width: "50%" },
              hAxis: {
                title: "Quantidade de Transações",
                minValue: 0,
              },
              vAxis: {
                title: "Tipo de Plano de Conta",
              },
            }}
          />
        </Col>

        <Col span={8}>
          <Chart
            width={"100%"}
            height={"400px"}
            chartType="PieChart"
            loader={<div>Carregando Gráfico...</div>}
            data={transacoesTipoContaDespesa}
            options={{
              colors: getColors(),
              title: "Saídas",
              chartArea: { width: "50%" },
              hAxis: {
                title: "Quantidade de Transações",
                minValue: 0,
              },
              vAxis: {
                title: "Tipo de Plano de Conta",
              },
            }}
          />
        </Col>
        <Col span={8}>
          <Chart
            width={"100%"}
            height={"400px"}
            chartType="PieChart"
            loader={<div>Carregando Gráfico...</div>}
            data={transacoesTipoContaReceita}
            options={{
              colors: getColors(),
              title: "Entradas",
              chartArea: { width: "50%" },
            }}
          />
        </Col>
      </Row>
      <Divider></Divider>
      <Row gutter={16}>
        <Col span={12}>
          <Chart
            width={"100%"}
            height={"400px"}
            chartType="Bar"
            loader={<div>Carregando Gráfico...</div>}
            data={receitasMes}
            options={{
              is3D: true,
              colors: getColors(),
              title: "Receitas por mês",
              chartArea: { width: "50%" },
              hAxis: {
                title: "Valor das Transações",
                minValue: 0,
              },
              vAxis: {
                title: "Mês/Ano",
              },
            }}
          />
        </Col>
        <Col span={12}>
          <Chart
            width={"100%"}
            height={"400px"}
            chartType="Bar"
            loader={<div>Carregando Gráfico...</div>}
            data={despesasMes}
            options={{
              is3D: true,
              colors: getColors(),
              title: "Despesas por mês",
              chartArea: { width: "50%" },
              hAxis: {
                title: "Valor das Transações",
                minValue: 0,
              },
              vAxis: {
                title: "Mês/Ano",
              },
            }}
          />
        </Col>
      </Row>
    </div>
  );
};

export default Dashboard;
