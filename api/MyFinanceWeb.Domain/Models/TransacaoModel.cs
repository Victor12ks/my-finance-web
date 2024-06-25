using MyFinanceWeb.Domain.Dtos;

namespace MyFinanceWeb.Domain.Models
{
    public class TransacaoModel
    {
        public TransacaoModel()
        {

        }
        public TransacaoModel(Transacao transacao)
        {
            CastDtoToModal(transacao);
        }

        public int? Codigo { get; set; }
        public string Historico { get; set; }
        public string DataHora { get; set; }
        public PlanoContaModel? PlanoConta { get; set; }
        public int? PlanoContaId { get; set; }
        public decimal Valor { get; set; }


        public Transacao CastModalToDto()
        {
            return new()
            {
                Id = Codigo ?? 0,
                Historico = this.Historico,
                DataHora = DateTime.Parse(DataHora),
                Valor = Valor,
                PlanoContaId = PlanoContaId ?? 0
            };
        }
        public void CastDtoToModal(Transacao transacao)
        {
            Historico = transacao.Historico;
            DataHora = transacao.DataHora.ToString("yyyy-MM-dd HH:mm:ss");
            Valor = transacao.Valor;
            PlanoContaId = transacao.PlanoContaId;
            Codigo = transacao.Id;
            PlanoConta = new PlanoContaModel(transacao?.PlanoConta);
        }
    }
}
