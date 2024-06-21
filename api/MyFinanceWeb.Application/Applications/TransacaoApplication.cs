using Microsoft.Extensions.Logging;
using MyFinanceWeb.Domain.Constants;
using MyFinanceWeb.Domain.Core;
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

        public Response<IEnumerable<TransacaoModel>> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message + ex?.StackTrace);
                return new Response<IEnumerable<TransacaoModel>>(Message.Error.DEFAULT_ERROR);
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

        public Response<IEnumerable<TransacaoModel>> GetByPlanoContaId(int transacao)
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
                if (_service.GetById(id) is not null)
                    return _service.Remove(id);
                else
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
                if (_service.GetById(transacao.Codigo) is not null)
                    return _service.Update(transacao);
                else
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
