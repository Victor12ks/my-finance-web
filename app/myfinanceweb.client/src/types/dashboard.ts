export interface DashboardModel {
  transacoesTipo: DataChart[];
  transacoesTipoContaDespesa: DataChart[];
  transacoesTipoContaReceita: DataChart[];
  despesasPorMes: DataChart[];
  receitasPorMes: DataChart[];
  maiorDespesa: DataChart;
  maiorReceita: DataChart;
}

export interface DataChart {
  descricao: string;
  valor: string;
}
