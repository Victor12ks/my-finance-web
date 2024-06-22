using Microsoft.Extensions.Logging;
using MyFinanceWeb.Domain.Constants;
using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Interfaces.Repositories;
using MyFinanceWeb.Domain.Interfaces.Services;
using MyFinanceWeb.Domain.Models;
using MyFinanceWeb.Domain.Utils;

namespace MyFinanceWeb.Application.Services
{

    public class PlanoContaService : IPlanoContaService
    {
        private readonly IPlanoContaRepository _repository;
        private readonly ILogger<PlanoContaModel> _logger;
        public PlanoContaService(IPlanoContaRepository repository, ILogger<PlanoContaModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public Response<PlanoContaModel> Add(PlanoContaModel planoConta)
        {
            try
            {
                var sucess = _repository.Add(planoConta.CastModalToDto());

                if (sucess)
                    return new Response<PlanoContaModel>(planoConta);

                return new Response<PlanoContaModel>(Message.Error.DEFAULT_ERROR);
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
                var planosConta = _repository.GetAll();

                if (planosConta is null || planosConta.Count() <= 0)
                    return new Response<List<PlanoContaModel>>();

                var result = planosConta.Clone<List<PlanoContaModel>>();
                result.ForEach(pc => pc.CreateObject());

                return new Response<List<PlanoContaModel>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message + ex?.StackTrace);
                return new Response<List<PlanoContaModel>>();
            }
        }

        public Response<PlanoContaModel> GetById(int id)
        {
            try
            {
                var planoConta = _repository.GetById(id);

                if (planoConta is null)
                    return new Response<PlanoContaModel>();

                var result = planoConta.Clone<PlanoContaModel>();
                result.CreateObject();
                return new Response<PlanoContaModel>(result);
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
                var hasPlanoConta = GetById(planoConta.Id)?.Data is not null;

                if (!hasPlanoConta)
                    return new Response<PlanoContaModel>();

                var sucess = _repository.Update(planoConta.CastModalToDto());

                if (sucess)
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
