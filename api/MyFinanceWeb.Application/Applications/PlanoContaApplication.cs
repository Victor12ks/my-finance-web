using Microsoft.Extensions.Logging;
using MyFinanceWeb.Domain.Constants;
using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Dtos;
using MyFinanceWeb.Domain.Interfaces.Applications;
using MyFinanceWeb.Domain.Interfaces.Services;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Application.Applications
{
    public class PlanoContaApplication(IPlanoContaService service, ILogger<PlanoContaModel> logger) : IPlanoContaApplication
    {
        private readonly IPlanoContaService _service = service;
        private readonly ILogger<PlanoContaModel> _logger = logger;

        public Response<PlanoContaModel> Add(PlanoContaModel planoContaModel)
        {
            try
            {
                var planoConta = _service.Add(planoContaModel);

                if (planoConta is not null)
                    return new Response<PlanoContaModel>(planoConta);

                return new Response<PlanoContaModel>("Não foi possível adicionar esse tipo de transação.");
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return new Response<PlanoContaModel>(Message.Error.DEFAULT_ERROR);
            }
        }

        public Response<List<PlanoContaModel>> GetAll()
        {
            try
            {
                return new Response<List<PlanoContaModel>>(_service.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message + ex?.StackTrace);
                return new Response<List<PlanoContaModel>>(Message.Error.DEFAULT_ERROR);
            }
        }

        public Response<PlanoContaModel> Update(PlanoContaModel planoContaModel)
        {
            try
            {
                if (!_service.HasPlanoConta(planoContaModel.Id))
                    return new Response<PlanoContaModel>("Não foi encontrado nenhum tipo de transação para atualizar.");

                var planoConta = _service.Update(planoContaModel);

                if (planoConta is not null)
                    return new Response<PlanoContaModel>(planoContaModel);

                return new Response<PlanoContaModel>("Não foi possível atualizar esse tipo de transação.");
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
                if (!_service.HasPlanoConta(planoConta.Id))
                    return new Response<PlanoContaModel>("Não foi localizada nenhum tipo de transação com esse código.");

                planoConta.DisableEnable();

                var result = _service.Update(planoConta);

                if (result is not null)
                    return new Response<PlanoContaModel>(planoConta);

                return new Response<PlanoContaModel>("Não foi atualizar esse tipo de transação.");
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return new Response<PlanoContaModel>(Message.Error.DEFAULT_ERROR);
            }
        }

        public Response<bool> HasAnyPlanoConta()
        {
            try
            {
                return new Response<bool>(_service.HasAnyPlanoConta());
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return new Response<bool>(Message.Error.DEFAULT_ERROR);
            }
        }
    }
}
