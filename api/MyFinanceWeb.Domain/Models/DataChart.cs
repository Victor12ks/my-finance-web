namespace MyFinanceWeb.Domain.Models
{
    public class DataChart
    {
        public DataChart()
        {
            
        }
        public DataChart(string descricao, string valor)
        {
            Descricao = descricao;
            Valor = valor;
        }

        public string Descricao { get; set; }
        public string Valor { get; set; }
    }
}
