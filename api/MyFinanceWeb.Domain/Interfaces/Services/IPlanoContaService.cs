using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Domain.Interfaces.Services
{
    public interface IPlanoContaService
    {
        List<PlanoContaModel> GetAll();
        bool HasPlanoConta(int id);
        PlanoContaModel? Add(PlanoContaModel planoConta);
        PlanoContaModel? Update(PlanoContaModel planoConta);
    }
}
