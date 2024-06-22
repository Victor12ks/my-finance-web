using Microsoft.Extensions.Logging;
using MyFinanceWeb.Domain.Constants;
using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Dtos;
using MyFinanceWeb.Domain.Interfaces.Applications;
using MyFinanceWeb.Domain.Interfaces.Services;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Application.Applications
{
    public class TransacaoApplication : ITransacaoApplication
    {
        private readonly ITransacaoService _service;
        private readonly ILogger<TransacaoModel> _logger;
        public TransacaoApplication(ITransacaoService service, ILogger<TransacaoModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        public Response<List<TransacaoModel>> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message + ex?.StackTrace);
                return new Response<List<TransacaoModel>>(Message.Error.DEFAULT_ERROR);
            }
        }

        public Response<TransacaoModel> GetById(int id)
        {
            try
            {
                return _service.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message + ex?.StackTrace);
                return new Response<TransacaoModel>(Message.Error.DEFAULT_ERROR);
            }
        }

        public Response<List<TransacaoModel>> GetByPlanoContaId(int transacao)
        {
            throw new NotImplementedException();
        }

        public Response<TransacaoModel> Register(TransacaoModel transacao)
        {
            try
            {
                return _service.Add(transacao);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return new Response<TransacaoModel>(Message.Error.DEFAULT_ERROR);
            }
        }

        public Response<bool> Remove(int id)
        {
            try
            {
                var transacao = _service.GetById(id)?.Data;

                if (transacao is not null)
                    return _service.Remove(transacao);

                return new Response<bool>("Não foi encontrado nenhum registro com esse código.");
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return new Response<bool>(Message.Error.DEFAULT_ERROR);
            }
        }

        public Response<TransacaoModel> Update(TransacaoModel transacao)
        {
            try
            {
                if (_service.GetById(transacao.Codigo ?? 0) is not null)
                    return _service.Update(transacao);

                return new Response<TransacaoModel>("Não foi encontrado nenhum registro com esse código.");
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return new Response<TransacaoModel>(Message.Error.DEFAULT_ERROR);
            }
        }
    }
}
