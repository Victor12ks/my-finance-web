namespace MyFinanceWeb.Domain.Dtos
{
    public record PlanoConta
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public char Tipo { get; set; }
        public bool Ativo { get; set; }
    };
}
