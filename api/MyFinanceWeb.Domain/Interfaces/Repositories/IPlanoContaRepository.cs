using MyFinanceWeb.Domain.Dtos;

namespace MyFinanceWeb.Domain.Interfaces.Repositories
{
    public interface IPlanoContaRepository
    {
        List<PlanoConta> GetAll();
        PlanoConta? GetById(int id);
        bool HasPlanoConta(int id);
        bool HasAnyPlanoConta();
        bool Add(PlanoConta PlanoConta);
        bool Update(PlanoConta PlanoConta);
    }
}
