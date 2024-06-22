using MyFinanceWeb.Domain.Dtos;
using MyFinanceWeb.Domain.Utils;

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
            //_ = decimal.TryParse(Valor, out decimal valor);
            return new()
            {
                Id = Codigo ?? 0,
                Historico = this.Historico,
                Data = DateTime.Parse(DataHora),
                Valor = Valor,
                PlanoContaId = (int)PlanoContaId
            };
        }
        public void CastDtoToModal(Transacao transacao)
        {
            Historico = transacao.Historico;
            DataHora = transacao.Data.ToString("yyyy-MM-dd HH:mm:ss");
            Valor = transacao.Valor;
            PlanoContaId = transacao.PlanoContaId;
            Codigo = transacao.Id;
            PlanoConta = new PlanoContaModel(transacao?.PlanoConta);
        }
    }
}
