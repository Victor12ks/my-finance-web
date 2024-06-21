using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Domain.Interfaces.Services
{
    public interface ITransacaoService
    {
        Response<IEnumerable<TransacaoModel>> GetAll();
        Response<TransacaoModel> GetById(int id);
        Response<TransacaoModel> Add(TransacaoModel Transacao);
        Response<TransacaoModel> Update(TransacaoModel Transacao);
        Response<bool> Remove(int id);
    }
}
