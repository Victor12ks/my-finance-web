using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Domain.Interfaces.Repositories
{
    public interface IDashboardRepository
    {
        List<DataChart> GetTransacoesPorTipo(DateTime startDate, DateTime endDate);
    }
}
