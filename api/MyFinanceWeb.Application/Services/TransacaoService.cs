using Microsoft.Extensions.Logging;
using MyFinanceWeb.Domain.Interfaces.Repositories;
using MyFinanceWeb.Domain.Interfaces.Services;
using MyFinanceWeb.Domain.Models;
using MyFinanceWeb.Domain.Utils;

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

        public List<DataChart> GetTransacoesByData(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var transacoes = _repository.GetTransacoesByData(dataInicio, dataFim);

                var transacoesPorMes = transacoes
                .GroupBy(t => new { t.Data.Year, t.Data.Month })
                .Select(g => new
                {
                    Ano = g.Key.Year,
                    Mes = g.Key.Month,
                    TotalValor = g.Sum(t => t.Valor),
                    Transacoes = g.ToList()
                })
                .OrderBy(g => g.Ano)
                .ThenBy(g => g.Mes)
                .ToList();

                var result = new List<DataChart>();
                transacoesPorMes.ForEach(transacao =>
                {
                    result.Add(new DataChart($"{transacao.Mes}/{transacao.Ano}", transacao.TotalValor.ToString()));
                });

                return result;
            }
            catch
            {
                return [];
            }
        }

        public List<DataChart> GetTransacoesByTipo(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var transacoes = _repository.GetTransacoesByData(dataInicio, dataFim);
                var transacoesTipo = transacoes
               .GroupBy(t => t.PlanoConta.Tipo)
                .Select(g => new
                {
                    TipoPlanoConta = g.Key,
                    TotalValor = g.Sum(t => t.Valor)
                })
                .ToList();

                var result = new List<DataChart>();

                transacoesTipo.ForEach(transacao =>
                {
                    var tipo = transacao.TipoPlanoConta == 'R' ? "Receita" : "Despesa";
                    result.Add(new DataChart(tipo, transacao.TotalValor.ToString()));
                });

                return result;
            }
            catch
            {
                return [];
            }
        }

        public List<DataChart> GetTransacoesByTipoConta(char tipo, DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var transacoes = _repository.GetTransacoesByData(dataInicio, dataFim);
                var transacoesTipo = transacoes
                .Where(t => t.PlanoConta.Tipo.Equals(tipo))
                .GroupBy(t => new { t.PlanoConta.Descricao, t.PlanoConta.Tipo })
                .Select(g => new
                {
                    g.Key.Descricao,
                    g.Key.Tipo,
                    TotalValor = g.Sum(t => t.Valor)
                })
                .OrderByDescending(g => g.TotalValor)
                .ToList();

                var result = new List<DataChart>();

                transacoesTipo.ForEach(transacao =>
                {
                    result.Add(new DataChart(transacao.Descricao, transacao.TotalValor.ToString()));
                });

                return result;
            }
            catch
            {
                return [];
            }
        }

        public (string Mes, string Valor)? GetMaiorValorByTipo(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var transacoes = _repository.GetTransacoesByData(dataInicio, dataFim);

                var result = transacoes
                .GroupBy(t => t.PlanoConta.Tipo)
                .Select(g => new
                {
                    Tipo = g.Key,
                    TotalValor = g.Sum(t => t.Valor)
                })
                .OrderByDescending(g => g.TotalValor)
                .FirstOrDefault();

                return (result.Tipo.ToString(), result.TotalValor.ToString());
            }
            catch
            {
                return null;
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
