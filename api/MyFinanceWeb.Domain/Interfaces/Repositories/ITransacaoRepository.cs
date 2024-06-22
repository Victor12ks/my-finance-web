using MyFinanceWeb.Domain.Dtos;

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
        int Save();
    }
}
