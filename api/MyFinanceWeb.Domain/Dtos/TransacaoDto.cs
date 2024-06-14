namespace MyFinanceWeb.Domain.Dtos
{
    public class TransacaoDto
    {
        public int Id { get; set; }
        public string? Historico { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public int PLanoContaId { get; set; }
        public PlanoContaDto PlanoConta { get; set; }
    }
}
