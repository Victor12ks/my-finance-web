using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Domain.Interfaces.Applications
{
    public interface IDashboardApplication
    {
        public Response<DashboardModel> GetDataDashboard(DateTime dataInicio, DateTime dataFim);
    }
}
