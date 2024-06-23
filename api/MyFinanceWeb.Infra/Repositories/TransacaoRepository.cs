using Microsoft.EntityFrameworkCore;
using MyFinanceWeb.Domain.Dtos;
using MyFinanceWeb.Domain.Interfaces.Repositories;
using MyFinanceWeb.Infra.Contexts;

namespace MyFinanceWeb.Infra.Repositories
{
    public class TransacaoRepository(MyFinanceDbContext dbContext) : ITransacaoRepository
    {
        private readonly MyFinanceDbContext _dbContext = dbContext;

        public List<Transacao> GetAll()
        {
            return _dbContext.Transacao.AsNoTracking().Include(x => x.PlanoConta).ToList();
        }

        public Transacao? GetById(int id)
        {
            return _dbContext.Transacao.AsNoTracking().Include(x => x.PlanoConta).First(x => x.Id == id);
        }

        public bool HasTransacao(int id)
        {
            return _dbContext.Transacao.Any(t => t.Id.Equals(id));
        }

        public bool Add(Transacao transacao)
        {
            _dbContext.Add(transacao).State = EntityState.Added;
            var result = _dbContext.SaveChanges();
            return result > 0;
        }

        public bool Remove(Transacao transacao)
        {
            _dbContext.Remove(transacao).State = EntityState.Deleted;
            var result = _dbContext.SaveChanges();
            return result > 0;
        }

        public bool Update(Transacao transacao)
        {
            _dbContext.Transacao.Attach(transacao);
            _dbContext.Entry(transacao).State = EntityState.Modified;
            var result = _dbContext.SaveChanges();
            return result > 0;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
    }
}
