using MyFinanceWeb.Domain.Dtos;

namespace MyFinanceWeb.Domain.Interfaces.Repositories
{
    public interface ITransacaoRepository
    {
        IEnumerable<TransacaoDto> GetAll();
        Task<List<TransacaoDto>> GetAllAsync();
        TransacaoDto GetById(int id);
        Task<TransacaoDto> GetByIdAsync(int id);
        bool Remove(int id);
        void Add(in TransacaoDto sender);
        void Update(in TransacaoDto sender);
        int Save();
        Task<int> SaveAsync();
    }
}
