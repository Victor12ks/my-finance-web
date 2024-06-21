namespace MyFinanceWeb.Domain.Dtos
{
    public record Transacao
    {
        public int Id { get; init; }
        public string Historico { get; init; }
        public DateTime Data { get; init; }
        public decimal Valor { get; init; }
        public int PlanoContaId { get; init; }
        public virtual PlanoConta? PlanoConta { get; init; }

        //public Transacao() { }

        //public Transacao(int id, string historico, DateTime data, decimal valor, int planoContaId, PlanoConta planoConta)
        //{
        //    Id = id;
        //    Historico = historico;
        //    Data = data;
        //    Valor = valor;
        //    PlanoContaId = planoContaId;
        //    PlanoConta = planoConta;
        //}
    }
}