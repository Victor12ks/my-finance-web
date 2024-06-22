using Microsoft.Extensions.Logging;
using MyFinanceWeb.Domain.Interfaces.Repositories;
using MyFinanceWeb.Domain.Interfaces.Services;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Application.Services
{

    public class TransacaoService(ITransacaoRepository repository, ILogger<TransacaoModel> logger) : ITransacaoService
    {
        private readonly ITransacaoRepository _repository = repository;
        private readonly ILogger<TransacaoModel> _logger = logger;

        public TransacaoModel? Add(TransacaoModel transacaoModel)
        {
            try
            {
                var transacao = _repository.Add(transacaoModel.CastModalToDto());

                if (transacao)
                    return transacaoModel;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                throw;
            }
        }

        public List<TransacaoModel> GetAll()
        {
            try
            {
                var result = new List<TransacaoModel>();
                var transacoes = _repository.GetAll();

                if (transacoes is null || transacoes.Count == 0)
                    return result;

                transacoes.ForEach(transacao =>
                {
                    result.Add(new TransacaoModel(transacao));
                });

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message + ex?.StackTrace);
                return [];
            }
        }

        public TransacaoModel? GetById(int id)
        {
            try
            {
                var transacao = _repository.GetById(id);

                if (transacao is null)
                    return new TransacaoModel();

                return new TransacaoModel(transacao);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message + ex?.StackTrace);
                throw;
            }
        }

        public bool HasTransacao(int id)
        {
            try
            {
                return _repository.HasTransacao(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message + ex?.StackTrace);
                return false;
            }
        }


        public bool Remove(TransacaoModel transacaoModel)
        {
            try
            {
                return _repository.Remove(transacaoModel.CastModalToDto());

            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return false;
            }
        }

        public TransacaoModel? Update(TransacaoModel transacaoModel)
        {
            try
            {

                var sucess = _repository.Update(transacaoModel.CastModalToDto());

                if (sucess)
                    return transacaoModel;

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
