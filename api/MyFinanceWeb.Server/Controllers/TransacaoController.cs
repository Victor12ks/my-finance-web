using Microsoft.AspNetCore.Mvc;
using MyFinanceWeb.Domain.Interfaces.Applications;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Server.Controllers
{
    [ApiController]
    [Route("[controller]/v1")]
    public class TransacaoController(ITransacaoApplication application) : ControllerBase
    {
        private readonly ITransacaoApplication _application = application;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_application.GetAll());
        }

        [HttpPost]
        public IActionResult Save(TransacaoModel model)
        {
            return Ok(_application.Register(model));
        }

        [HttpPut]
        public IActionResult Update(TransacaoModel model)
        {
            return Ok(_application.Update(model));
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            return Ok(_application.Remove(id));
        }
    }
}
