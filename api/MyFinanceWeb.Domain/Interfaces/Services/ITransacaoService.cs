using MyFinanceWeb.Domain.Core;
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
        bool Remove(TransacaoModel transacaoModel);
    }
}
