using Microsoft.EntityFrameworkCore;
using MyFinanceWeb.Domain.Dtos;
using MyFinanceWeb.Domain.Interfaces.Repositories;
using MyFinanceWeb.Infra.Contexts;

namespace MyFinanceWeb.Infra.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly MyFinanceDbContext _dbContext;
        public TransacaoRepository(MyFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Transacao> GetAll()
        {
            return _dbContext.Transacao.AsNoTracking().Include(x => x.PlanoConta).ToList();
        }

        public Transacao? GetById(int id)
        {
            return _dbContext.Transacao.AsNoTracking().First(x => x.Id == id);
        }

        public bool Add(Transacao Transacao)
        {
            var result = _dbContext.Add(Transacao).State = EntityState.Added;
            _dbContext.SaveChanges();
            return result == EntityState.Added;
        }

        public bool Update(Transacao Transacao)
        {
            _dbContext.Transacao.Attach(Transacao);
            _dbContext.Entry(Transacao).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return true;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
    }
}
