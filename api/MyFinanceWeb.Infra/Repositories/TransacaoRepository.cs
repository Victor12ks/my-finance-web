using Microsoft.EntityFrameworkCore;
using MyFinanceWeb.Domain.Dtos;
using MyFinanceWeb.Domain.Interfaces.Repositories;
using MyFinanceWeb.Infra.Contexts;

namespace MyFinanceWeb.Infra.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly MyFinanceDbContext _dbContext;
        public TransacaoRepository()
        {

        }

        public IEnumerable<TransacaoDto> GetAll()
        {
            return _dbContext.Transacao.ToList();
        }

        public Task<List<TransacaoDto>> GetAllAsync()
        {
            return _dbContext.Transacao.ToListAsync();
        }

        public TransacaoDto GetById(int id)
        {
            return _dbContext.Transacao.Find(id);
        }

        public async Task<TransacaoDto> GetByIdAsync(int id)
        {
            return await _dbContext.Transacao.FindAsync(id);
        }

        public bool Remove(int id)
        {
            var Transacao = _dbContext.Transacao.Find(id);
            if (Transacao is { })
            {
                _dbContext.Transacao.Remove(Transacao);
                return true;
            }

            return false;
        }

        public void Add(in TransacaoDto sender)
        {
            _dbContext.Add(sender).State = EntityState.Added;
        }

        public void Update(in TransacaoDto sender)
        {
            _dbContext.Entry(sender).State = EntityState.Modified;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
