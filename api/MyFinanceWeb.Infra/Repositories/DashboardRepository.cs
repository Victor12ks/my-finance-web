using MyFinanceWeb.Domain.Interfaces.Repositories;
using MyFinanceWeb.Domain.Models;
using MyFinanceWeb.Infra.Contexts;

namespace MyFinanceWeb.Infra.Repositories
{
    public class DashboardRepository(MyFinanceDbContext dbContext) : IDashboardRepository
    {
        private readonly MyFinanceDbContext _dbContext = dbContext;

        public List<DataChart> GetTransacoesPorTipo(DateTime startDate, DateTime endDate)
        {
            var resultados = _dbContext.Transacao
                .Where(t => t.DataHora >= startDate && t.DataHora <= endDate)
                .GroupBy(t => new { t.PlanoConta.Id, t.PlanoConta.Descricao })
                .Select(g => new DataChart()
                {
                    Descricao = g.Key.Descricao,
                    Valor = g.Count().ToString(),
                })
                .ToList();

            return resultados;
        }
    }
}
