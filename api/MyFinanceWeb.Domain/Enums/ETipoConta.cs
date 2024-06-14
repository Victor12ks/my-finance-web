using System.ComponentModel;

namespace MyFinanceWeb.Domain.Enums
{
    public enum ETipoConta
    {
        [Description("D")]
        Despesa,
        [Description("R")]
        Receita
    }
}
