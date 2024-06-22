using MyFinanceWeb.Domain.Dtos;

namespace MyFinanceWeb.Domain.Interfaces.Repositories
{
    public interface ITransacaoRepository
    {
        List<Transacao> GetAll();
        Transacao? GetById(int id);
        bool Add(Transacao Transacao);
        bool Update(Transacao Transacao);
        bool Remove(Transacao Transacao);
        int Save();
    }
}
