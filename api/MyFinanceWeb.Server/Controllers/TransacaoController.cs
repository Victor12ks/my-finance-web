using Microsoft.AspNetCore.Mvc;
using MyFinanceWeb.Domain.Interfaces.Applications;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Server.Controllers
{
    [ApiController]
    [Route("[controller]/v1")]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoApplication _application;

        public TransacaoController(ITransacaoApplication application)
        {
            _application = application;
        }



        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_application.GetAll());
        }

        [HttpPost]
        public IActionResult Save(TransacaoModel modal)
        {
            return Ok(_application.Register(modal));
        }

        [HttpPut]
        public IActionResult Update(TransacaoModel modal)
        {
            var result = _application.Update(modal);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            return Ok(_application.Remove(id));
        }

        //public static List<TransacaoModel> GerarTransacoesAleatorias(int quantidade)
        //{
        //    var random = new Random();
        //    var transacoes = new List<TransacaoModel>();

        //    for (int i = 0; i < quantidade; i++)
        //    {
        //        var transacao = new TransacaoModel
        //        {
        //            Codigo = random.Next(1000, 9999), // Código aleatório entre 1000 e 9999
        //            Historico = $"Historico {i + 1}",
        //            DataHora = DateTime.Now.AddMinutes(random.Next(-10000, 10000)).ToString("yyyy-MM-dd HH:mm:ss"), // Data e hora aleatória no intervalo de +- 10000 minutos a partir de agora
        //            Valor = random.Next(100, 10000).ToString("C") // Valor aleatório entre 100 e 10000
        //        };

        //        transacoes.Add(transacao);
        //    }

        //    return transacoes;
        //}
    }
}
