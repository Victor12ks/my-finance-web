using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Domain.Interfaces.Services
{
    public interface IPlanoContaService
    {
        Response<IEnumerable<PlanoContaModel>> GetAll();
        Response<PlanoContaModel> GetById(int id);
        Response<PlanoContaModel> Add(PlanoContaModel planoConta);
        Response<PlanoContaModel> Update(PlanoContaModel planoConta);
    }
}
