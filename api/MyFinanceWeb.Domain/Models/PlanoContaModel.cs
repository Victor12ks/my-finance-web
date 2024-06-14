using MyFinanceWeb.Domain.Dtos;
using MyFinanceWeb.Domain.Utils;
namespace MyFinanceWeb.Domain.Models
{
    public class PlanoContaModel
    {
        public PlanoContaModel()
        {

        }

        public PlanoContaModel(PlanoContaDto planoConta)
        {
            Ativo = planoConta.Ativo;
            Descricao = planoConta.Descricao;
            Id = planoConta.Id;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public string Tipo { get; set; }
        public string? CorTag { get; set; }

        public PlanoContaModel CastDtoToModel(PlanoContaDto planoConta)
        {
            var result = new PlanoContaModel(planoConta);
            result.SetTipo(planoConta.Tipo.ToString());
            return result;
        }

        public PlanoContaDto CastModalToDto()
        {
            var planoContaDto = new PlanoContaDto(Id: this.Id, Descricao: this.Descricao, Tipo: this.Tipo[0], Ativo: true);
            return planoContaDto;
        }


        public void Builder()
        {
            SetTipo(Tipo.ToString());
        }

        public void SetTipo(string tipo)
        {
            if (tipo.IsEqualTo("R"))
            {
                CorTag = "volcano";
                Tipo = "Receita";
            }
            else if (tipo.IsEqualTo("D"))
            {
                CorTag = "green";
                Tipo = "Despesa";
            }
        }
    }
}
