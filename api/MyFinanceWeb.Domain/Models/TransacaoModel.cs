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

        public int Codigo { get; set; }
        public string Historico { get; set; }
        public string DataHora { get; set; }
        public int? PlanoContaId { get; set; }
        public string Valor { get; set; }


        public Transacao CastModalToDto()
        {
            return new()
            {
                Historico = this.Historico,
                Data = DateTime.Parse(DataHora),
                Valor = decimal.Parse(Valor),
                PlanoContaId = (int)PlanoContaId
            };
        }
        public void CastDtoToModal(Transacao transacao)
        {
            Historico = transacao.Historico;
            DataHora = transacao.Data.ToString("dd/MM/yyyy");
            Valor = transacao.Valor.ConvertToReal();
            PlanoContaId = transacao.PlanoContaId;
            Codigo = transacao.Id;
        }
    }
}
