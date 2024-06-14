using Microsoft.Extensions.Logging;
using MyFinanceWeb.Domain.Constants;
using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Dtos;
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

        public Response<IEnumerable<PlanoContaModel>> GetAll()
        {
            try
            {
                //var planosConta = _repository.GetAll();

                //if (planosConta is null || planosConta.Count() <= 0)
                //    return new Response<IEnumerable<PlanoContaModel>>(Enumerable.Empty<PlanoContaModel>());

                var ok = new List<PlanoContaDto>
                {
                    new PlanoContaDto(1,"Investimento",'R', true),
                    new PlanoContaDto(2, "Gasolina",'D', true),
                    new PlanoContaDto(3, "Investimento",'R', true),
                    new PlanoContaDto(4, "Investimento",'D', false),
                    new PlanoContaDto(5, "Educação",'D', true),
                    new PlanoContaDto(6, "Salário",'R', true),
                };


                var result = ok.Clone<List<PlanoContaModel>>();
                result.ForEach(pc => pc.Builder());

                return new Response<IEnumerable<PlanoContaModel>>(result);
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
                var planoConta = _repository.GetById(id);

                if (planoConta is null)
                    return new Response<PlanoContaModel>();


                var result = planoConta.Clone<PlanoContaModel>();
                result.Builder();

                return new Response<PlanoContaModel>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message + ex?.StackTrace);
                return new Response<PlanoContaModel>(Message.Error.DEFAULT_ERROR);
            }
        }

        public PlanoContaModel Remove(PlanoContaModel planoConta)
        {
            throw new NotImplementedException();
        }

        public PlanoContaModel Update(PlanoContaModel planoConta)
        {
            throw new NotImplementedException();
        }
    }
}
