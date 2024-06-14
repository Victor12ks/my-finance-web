using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Domain.Interfaces.Services
{
    public interface IPlanoContaService
    {
        Response<IEnumerable<PlanoContaModel>> GetAll();
        Response<PlanoContaModel> GetById(int id);
        PlanoContaModel Remove(PlanoContaModel planoConta);
        Response<PlanoContaModel> Add(PlanoContaModel planoConta);
        PlanoContaModel Update(PlanoContaModel planoConta);
    }
}
