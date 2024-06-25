using Microsoft.Extensions.Logging;
using MyFinanceWeb.Domain.Constants;
using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Interfaces.Repositories;
using MyFinanceWeb.Domain.Interfaces.Services;
using MyFinanceWeb.Domain.Models;
using MyFinanceWeb.Domain.Utils;

namespace MyFinanceWeb.Application.Services
{

    public class PlanoContaService(IPlanoContaRepository repository, ILogger<PlanoContaModel> logger) : IPlanoContaService
    {
        private readonly IPlanoContaRepository _repository = repository;
        private readonly ILogger<PlanoContaModel> _logger = logger;

        public PlanoContaModel? Add(PlanoContaModel planoContaModel)
        {
            try
            {
                var planoConta = _repository.Add(planoContaModel.CastModalToDto(true));

                if (planoConta)
                    return planoContaModel;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                throw;
            }
        }

        public List<PlanoContaModel> GetAll()
        {
            try
            {
                var result = new List<PlanoContaModel>();
                var transacoes = _repository.GetAll();

                if (transacoes?.Count == 0)
                    return result;

                transacoes?.ForEach(transacao =>
                {
                    result.Add(new PlanoContaModel(transacao));
                });

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message + ex?.StackTrace);
                return [];
            }
        }

        public bool HasAnyPlanoConta()
        {
            return _repository.HasAnyPlanoConta();
        }

        public bool HasPlanoConta(int id)
        {
            try
            {
                return _repository.HasPlanoConta(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message + ex?.StackTrace);
                return false;
            }
        }

        public PlanoContaModel? Update(PlanoContaModel planoContaModel)
        {
            try
            {

                var sucess = _repository.Update(planoContaModel.CastModalToDto(null));

                if (sucess)
                    return planoContaModel;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return null;
            }
        }
    }
}
