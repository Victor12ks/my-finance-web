using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Domain.Interfaces.Services
{
    public interface ITransacaoService
    {
        Response<List<TransacaoModel>> GetAll();
        Response<TransacaoModel> GetById(int id);
        Response<TransacaoModel> Add(TransacaoModel transacao);
        Response<TransacaoModel> Update(TransacaoModel transacao);
        Response<bool> Remove(TransacaoModel transacao);
    }
}
