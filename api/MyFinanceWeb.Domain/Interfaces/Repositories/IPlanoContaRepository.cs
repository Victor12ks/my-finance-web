using MyFinanceWeb.Domain.Dtos;

namespace MyFinanceWeb.Domain.Interfaces.Repositories
{
    public interface IPlanoContaRepository
    {
        IEnumerable<PlanoConta> GetAll();
        PlanoConta? GetById(int id);
        bool Add(PlanoConta PlanoConta);
        bool Update(PlanoConta PlanoConta);
        int Save();
    }
}
