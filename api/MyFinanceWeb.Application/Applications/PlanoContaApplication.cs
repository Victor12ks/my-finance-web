using Microsoft.Extensions.Logging;
using MyFinanceWeb.Domain.Constants;
using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Interfaces.Applications;
using MyFinanceWeb.Domain.Interfaces.Services;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Application.Applications
{
    public class PlanoContaApplication : IPlanoContaApplication
    {
        private readonly IPlanoContaService _service;
        private readonly ILogger<PlanoContaModel> _logger;
        public PlanoContaApplication(IPlanoContaService service, ILogger<PlanoContaModel> logger)
        {
            _service = service;
            _logger = logger;
        }
        public Response<PlanoContaModel> Add(PlanoContaModel planoConta)
        {
            try
            {
                return _service.Add(planoConta);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return new Response<PlanoContaModel>(Message.Error.DEFAULT_ERROR);
            }
        }

        public Response<IEnumerable<PlanoContaModel>> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message + ex?.StackTrace);
                return new Response<IEnumerable<PlanoContaModel>>(Enumerable.Empty<PlanoContaModel>());
            }
        }

        public Response<PlanoContaModel> GetById(int id)
        {
            try
            {
                return _service.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message + ex?.StackTrace);
                return new Response<PlanoContaModel>(Message.Error.DEFAULT_ERROR);
            }
        }

        public Response<PlanoContaModel> Update(PlanoContaModel planoConta)
        {
            try
            {
                return _service.Update(planoConta);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return new Response<PlanoContaModel>(Message.Error.DEFAULT_ERROR);
            }
        }

        public Response<PlanoContaModel> DisableEnable(PlanoContaModel planoConta)
        {
            try
            {
                planoConta.DisableEnable();
                var result = Update(planoConta);

                if (result.Success)
                    return new Response<PlanoContaModel>(planoConta);

                return new Response<PlanoContaModel>(Message.Error.DEFAULT_ERROR);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return new Response<PlanoContaModel>(Message.Error.DEFAULT_ERROR);
            }
        }
    }
}
