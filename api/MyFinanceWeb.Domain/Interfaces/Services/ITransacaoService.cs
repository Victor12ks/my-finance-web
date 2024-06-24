using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Dtos;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Domain.Interfaces.Services
{
    public interface ITransacaoService
    {
        List<TransacaoModel> GetAll();
        TransacaoModel? GetById(int id);
        bool HasTransacao(int id);
        TransacaoModel? Add(TransacaoModel transacaoModel);
        TransacaoModel? Update(TransacaoModel transacaoModel);
        public List<DataChart> GetTransacoesByData(DateTime dataInicio, DateTime dataFim);

        public List<DataChart> GetTransacoesByTipo(DateTime dataInicio, DateTime dataFim);

        public List<DataChart> GetTransacoesByTipoConta(char tipo, DateTime dataInicio, DateTime dataFim);

        public (string Mes, string Valor)? GetMaiorValorByTipo(DateTime dataInicio, DateTime dataFim);
        bool Remove(TransacaoModel transacaoModel);
    }
}
