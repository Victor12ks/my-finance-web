using Microsoft.AspNetCore.Mvc;
using MyFinanceWeb.Domain.Interfaces.Applications;

namespace MyFinanceWeb.Server.Controllers
{
    [ApiController]
    [Route("[controller]/v1")]
    public class DashboardController(IDashboardApplication application) : ControllerBase
    {
        private readonly IDashboardApplication _application = application;

        [HttpGet]
        public IActionResult Get([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
        {
            return Ok(_application.GetDataDashboard(dataInicio, dataFim));
        }
    }
}
