using MyFinanceWeb.Domain.Constants;
using MyFinanceWeb.Domain.Core;
using MyFinanceWeb.Domain.Interfaces.Applications;
using MyFinanceWeb.Domain.Interfaces.Services;
using MyFinanceWeb.Domain.Models;

namespace MyFinanceWeb.Application.Applications
{
    public class DashboardApplication(ITransacaoService transacaoService) : IDashboardApplication
    {
        private readonly ITransacaoService _transacaoService = transacaoService;
        public Response<DashboardModel> GetDataDashboard(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var receitaPorMes = _transacaoService.GetTransacoesByData('R', dataInicio, dataFim);
                var despesasPorMes = _transacaoService.GetTransacoesByData('D', dataInicio, dataFim);
                var agrupamentoPorTipo = _transacaoService.GetTransacoesByTipo(dataInicio, dataFim);
                var agrupamentoReceitas = _transacaoService.GetTransacoesByTipoConta('R', dataInicio, dataFim);
                var agrupamentoDespesas = _transacaoService.GetTransacoesByTipoConta('D', dataInicio, dataFim);

                var result = new DashboardModel()
                {
                    ReceitasPorMes = receitaPorMes,
                    DespesasPorMes = despesasPorMes,
                    TransacoesTipo = agrupamentoPorTipo,
                    TransacoesTipoContaDespesa = agrupamentoDespesas,
                    TransacoesTipoContaReceita = agrupamentoReceitas
                };

                result.GetMaiorDespesa();
                result.GetMaiorReceita();

                return new Response<DashboardModel>(result);
            }
            catch (Exception)
            {
                return new Response<DashboardModel>(Message.Error.DEFAULT_ERROR);
            }
        }
    }
}
