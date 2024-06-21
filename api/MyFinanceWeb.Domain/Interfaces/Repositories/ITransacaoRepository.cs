using MyFinanceWeb.Domain.Dtos;

namespace MyFinanceWeb.Domain.Interfaces.Repositories
{
    public interface ITransacaoRepository
    {
        IEnumerable<Transacao> GetAll();
        Transacao? GetById(int id);
        bool Add(Transacao Transacao);
        bool Update(Transacao Transacao);
        int Save();
    }
}
