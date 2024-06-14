using FluentValidation;
using MyFinanceWeb.Domain.Models;
using MyFinanceWeb.Domain.Validations.Messages;

namespace MyFinanceWeb.Domain.Validations
{
    public class PlanoContaValidator : AbstractValidator<PlanoContaModel>
    {
        public PlanoContaValidator()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage(PlanoContaMessage.DescricaoIsRequired);
            //RuleFor(x => x.Tipo)
            //    .IsInEnum()
            //    .WithMessage(PlanoContaMessage.DescricaoIsRequired);
        }
    }
}
