using Microsoft.Extensions.Logging;
using MyFinanceWeb.Domain.Constants;
using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Interfaces.Repositories;
using MyFinanceWeb.Domain.Interfaces.Services;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Application.Services
{

    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _repository;
        private readonly ILogger<TransacaoModel> _logger;
        public TransacaoService(ITransacaoRepository repository, ILogger<TransacaoModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public Response<TransacaoModel> Add(TransacaoModel transacao)
        {
            try
            {
                var sucess = _repository.Add(transacao.CastModalToDto());

                if (sucess)
                    return new Response<TransacaoModel>(transacao);

                return new Response<TransacaoModel>(Message.Error.DEFAULT_ERROR);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return new Response<TransacaoModel>(Message.Error.DEFAULT_ERROR);
            }
        }

        public Response<List<TransacaoModel>> GetAll()
        {
            try
            {
                var result = new Response<List<TransacaoModel>>();
                var transacoes = _repository.GetAll();

                if (transacoes is null || transacoes.Count() <= 0)
                    return result;

                result.Data = new List<TransacaoModel>();

                transacoes.ForEach(transacao =>
                {
                    result.Data.Add(new TransacaoModel(transacao));
                });

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message + ex?.StackTrace);
                return new Response<List<TransacaoModel>>();
            }
        }

        public Response<TransacaoModel> GetById(int id)
        {
            try
            {
                var transacao = _repository.GetById(id);

                if (transacao is null)
                    return new Response<TransacaoModel>();

                var result = new TransacaoModel(transacao);

                return new Response<TransacaoModel>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message + ex?.StackTrace);
                return new Response<TransacaoModel>(Message.Error.DEFAULT_ERROR);
            }
        }

        public Response<bool> Remove(TransacaoModel transacao)
        {
            try
            {
                var sucess = _repository.Remove(transacao.CastModalToDto());

                if (sucess)
                    return new Response<bool>();

                return new Response<bool>(Message.Error.DEFAULT_ERROR);
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
                var hasTransacao = GetById(transacao.Codigo ?? 0)?.Data is not null;

                if (!hasTransacao)
                    return new Response<TransacaoModel>();

                var sucess = _repository.Update(transacao.CastModalToDto());

                if (sucess)
                    return new Response<TransacaoModel>(transacao);

                return new Response<TransacaoModel>(Message.Error.DEFAULT_ERROR);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return new Response<TransacaoModel>(Message.Error.DEFAULT_ERROR);
            }
        }
    }
}
