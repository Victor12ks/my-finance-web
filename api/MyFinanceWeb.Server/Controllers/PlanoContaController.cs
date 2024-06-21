using Microsoft.AspNetCore.Mvc;
using MyFinanceWeb.Domain.Interfaces.Applications;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Server.Controllers
{
    [ApiController]
    [Route("[controller]/v1")]
    public class PlanoContaController : ControllerBase
    {
        private readonly IPlanoContaApplication _application;

        public PlanoContaController(IPlanoContaApplication application)
        {
            _application = application;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_application.GetAll());
        }

        [HttpPost]
        public IActionResult Save(PlanoContaModel modal)
        {
            return Ok(_application.Add(modal));
        }

        [HttpPut]
        public IActionResult Update(PlanoContaModel modal)
        {
            return Ok(_application.Update(modal));
        }

        [HttpPut("disable-enable")]
        public IActionResult DisableEnable(PlanoContaModel modal)
        {
            return Ok(_application.DisableEnable(modal));
        }
    }
}
