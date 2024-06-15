using Microsoft.EntityFrameworkCore;
using MyFinanceWeb.Domain.Dtos;
using MyFinanceWeb.Domain.Interfaces.Repositories;
using MyFinanceWeb.Infra.Contexts;

namespace MyFinanceWeb.Infra.Repositories
{
    public class PlanoContaRepository : IPlanoContaRepository
    {
        private readonly MyFinanceDbContext _dbContext;
        public PlanoContaRepository(MyFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<PlanoContaDto> GetAll()
        {
            return _dbContext.PlanoConta.AsNoTracking().ToList();
        }

        public PlanoContaDto? GetById(int id)
        {
            return _dbContext.PlanoConta.AsNoTracking().First(x => x.Id == id);
        }

        public bool Add(PlanoContaDto planoConta)
        {
            var result = _dbContext.Add(planoConta).State = EntityState.Added;
            _dbContext.SaveChanges();
            return result == EntityState.Added;
        }

        public bool Update(PlanoContaDto planoConta)
        {
            _dbContext.PlanoConta.Attach(planoConta);
            _dbContext.Entry(planoConta).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return true;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
    }
}
