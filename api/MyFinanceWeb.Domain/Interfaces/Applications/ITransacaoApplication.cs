using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Domain.Interfaces.Applications
{
    public interface ITransacaoApplication
    {
        public Response<TransacaoModel> GetById(int id);
        public Response<IEnumerable<TransacaoModel>> GetAll();
        public Response<IEnumerable<TransacaoModel>> GetByPlanoContaId(int transacao);
        public Response<TransacaoModel> Register(TransacaoModel transacao);
        public Response<TransacaoModel> Update(TransacaoModel transacao);
        public Response<bool> Remove(int id);
    }
}
