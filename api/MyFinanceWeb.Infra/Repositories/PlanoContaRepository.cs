using Microsoft.EntityFrameworkCore;
using MyFinanceWeb.Domain.Dtos;
using MyFinanceWeb.Domain.Interfaces.Repositories;
using MyFinanceWeb.Infra.Contexts;

namespace MyFinanceWeb.Infra.Repositories
{
    public class PlanoContaRepository(MyFinanceDbContext dbContext) : IPlanoContaRepository
    {
        private readonly MyFinanceDbContext _dbContext = dbContext;

        public List<PlanoConta> GetAll()
        {
            return _dbContext.PlanoConta.AsNoTracking().ToList();
        }
        public bool HasPlanoConta(int id)
        {
            return _dbContext.PlanoConta.Any(t => t.Id.Equals(id));
        }

        public PlanoConta? GetById(int id)
        {
            return _dbContext.PlanoConta.AsNoTracking().First(x => x.Id == id);
        }

        public bool Add(PlanoConta planoConta)
        {
            var result = _dbContext.Add(planoConta).State = EntityState.Added;
            return _dbContext.SaveChanges() > 0;
        }

        public bool Update(PlanoConta planoConta)
        {
            _dbContext.PlanoConta.Update(planoConta);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
