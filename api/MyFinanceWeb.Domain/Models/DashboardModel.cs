namespace MyFinanceWeb.Domain.Models
{
    public class DashboardModel
    {
        public List<DataChart> TransacoesTipo { get; set; }
        public List<DataChart> TransacoesTipoContaDespesa { get; set; }
        public List<DataChart> TransacoesTipoContaReceita { get; set; }
        public List<DataChart> TransacoesMes { get; set; }
        public DataChart MaiorDespesa { get; set; }
        public DataChart MaiorReceita { get; set; }

        public void GetMaiorReceita()
        {
            MaiorReceita = TransacoesTipoContaReceita.MaxBy(x => Convert.ToDecimal(x.Valor));
        }
        public void GetMaiorDespesa()
        {
            MaiorDespesa = TransacoesTipoContaDespesa.MaxBy(x => Convert.ToDecimal(x.Valor));
        }
    }
}
