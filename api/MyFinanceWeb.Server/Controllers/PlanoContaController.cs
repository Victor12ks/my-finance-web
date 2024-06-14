using Microsoft.AspNetCore.Mvc;
using MyFinanceWeb.Domain.Interfaces.Services;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Server.Controllers
{
    [ApiController]
    [Route("[controller]/v1")]
    public class PlanoContaController : ControllerBase
    {
        private readonly ILogger<PlanoContaController> _logger;
        private readonly IPlanoContaService _service;

        public PlanoContaController(ILogger<PlanoContaController> logger, IPlanoContaService service)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult Save(PlanoContaModel modal)
        {
            return Ok(_service.Add(modal));
        }
    }
}
