using MyFinanceWeb.Domain.Dtos;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Domain.Interfaces.Repositories
{
    public interface ITransacaoRepository
    {
        List<Transacao> GetAll();
        Transacao? GetById(int id);
        bool Add(Transacao transacao);
        bool HasTransacao(int id);
        bool Update(Transacao transacao);
        bool Remove(Transacao transacao);
        //List<DataChart> GetTransacoesByTipoConta(DateTime dataInicio, DateTime dataFim);
        //List<DataChart> GetTransacoesByTipo(DateTime dataInicio, DateTime dataFim);
        List<Transacao> GetTransacoesByData(DateTime dataInicio, DateTime dataFim);
        int Save();
    }
}
