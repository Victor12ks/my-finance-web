using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Domain.Interfaces.Applications
{
    public interface IPlanoContaApplication
    {
        Response<List<PlanoContaModel>> GetAll();
        Response<PlanoContaModel> GetById(int id);
        Response<PlanoContaModel> DisableEnable(PlanoContaModel planoConta);
        Response<PlanoContaModel> Add(PlanoContaModel planoConta);
        Response<PlanoContaModel> Update(PlanoContaModel planoConta);
    }
}
