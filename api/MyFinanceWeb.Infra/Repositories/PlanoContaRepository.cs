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
            return _dbContext.PlanoConta.ToList();
        }

        //public Task<List<PlanoConta>> GetAllAsync()
        //{
        //    return _dbContext.PlanoConta.ToListAsync();
        //}

        public PlanoContaDto? GetById(int id)
        {
            return _dbContext.PlanoConta.Find(id);
        }

        //public async Task<PlanoConta> GetByIdAsync(int id)
        //{
        //    return await _dbContext.PlanoConta.FindAsync(id);
        //}

        //public bool Remove(int id)
        //{
        //    var PlanoConta = _dbContext.PlanoConta.Find(id);
        //    if (PlanoConta is { })
        //    {
        //        _dbContext.PlanoConta.Remove(PlanoConta);
        //        return true;
        //    }

        //    return false;
        //}

        public bool Add(PlanoContaDto planoConta)
        {
            var result = _dbContext.Add(planoConta).State = EntityState.Added;
            _dbContext.SaveChanges();
            return result == EntityState.Added;
        }

        public bool Update(PlanoContaDto planoConta)
        {
            _dbContext.Entry(planoConta).State = EntityState.Modified;
            return true;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        //private bool _disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!_disposed)
        //    {
        //        if (disposing)
        //        {
        //            _dbContext.Dispose();
        //        }
        //    }
        //    _disposed = true;
        //}
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}
