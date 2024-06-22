using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Domain.Interfaces.Applications
{
    public interface ITransacaoApplication
    {
        public Response<TransacaoModel> GetById(int id);
        public Response<List<TransacaoModel>> GetAll();
        public Response<List<TransacaoModel>> GetByPlanoContaId(int transacao);
        public Response<TransacaoModel> Register(TransacaoModel transacaoModel);
        public Response<TransacaoModel> Update(TransacaoModel transacaoModel);
        public Response<bool> Remove(int id);
    }
}
