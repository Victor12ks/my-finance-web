using MyFinanceWeb.Domain.Dtos;

namespace MyFinanceWeb.Domain.Interfaces.Repositories
{
    public interface IPlanoContaRepository
    {
        IEnumerable<PlanoContaDto> GetAll();
        PlanoContaDto? GetById(int id);
        bool Add(PlanoContaDto PlanoConta);
        bool Update(PlanoContaDto PlanoConta);
        int Save();
    }
}
