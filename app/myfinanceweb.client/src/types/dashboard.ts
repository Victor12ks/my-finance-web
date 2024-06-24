export interface DashboardModel {
  transacoesTipo: DataChart[];
  transacoesTipoContaDespesa: DataChart[];
  transacoesTipoContaReceita: DataChart[];
  transacoesMes: DataChart[];
  maiorDespesa: DataChart;
  maiorReceita: DataChart;
}

export interface DataChart {
  descricao: string;
  valor: string;
}
