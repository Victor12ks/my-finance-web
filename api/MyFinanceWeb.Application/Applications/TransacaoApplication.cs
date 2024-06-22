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

        public Response<List<TransacaoModel>> GetAll()
        {
            try
            {
                return new Response<List<TransacaoModel>>(_service.GetAll());
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
                var transacao = _service.GetById(id);
                if (transacao is not null)
                    return new Response<TransacaoModel>(transacao);

                return new Response<TransacaoModel>("Não foi localizada nenhuma transação com esse código.");
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

        public Response<TransacaoModel> Register(TransacaoModel transacaoModel)
        {
            try
            {
                var transacao = _service.Add(transacaoModel);

                if (transacao is not null)
                    return new Response<TransacaoModel>(transacao);

                return new Response<TransacaoModel>("Não foi possível adicionar essa transação.");

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
                var transacao = _service.GetById(id);

                if (transacao is not null)
                    return new Response<bool>(_service.Remove(transacao));

                return new Response<bool>("Não foi possível remover essa transação.");
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return new Response<bool>(Message.Error.DEFAULT_ERROR);
            }
        }

        public Response<TransacaoModel> Update(TransacaoModel transacaoModal)
        {
            try
            {
                if (!_service.HasTransacao(transacaoModal.Codigo ?? 0))
                    return new Response<TransacaoModel>("Não foi encontrado nenhuma transação para atualizar.");

                var transacao = _service.Update(transacaoModal);

                if (transacao is not null)
                    return new Response<TransacaoModel>(transacao);

                return new Response<TransacaoModel>("Não foi possível atualizar essa transação.");


            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return new Response<TransacaoModel>(Message.Error.DEFAULT_ERROR);
            }
        }
    }
}
