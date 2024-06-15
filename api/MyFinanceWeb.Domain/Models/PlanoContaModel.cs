using MyFinanceWeb.Domain.Dtos;
using MyFinanceWeb.Domain.Utils;
using System.Reflection.Metadata.Ecma335;
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
            //SetTipo(Tipo.ToString());
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public string Tipo { get; set; }
        public string? CorTag { get; set; }

        //public PlanoContaModel CastDtoToModel(PlanoContaDto planoConta)
        //{
        //    var result = new PlanoContaModel(planoConta);
        //    result.CreateObject(planoConta.Tipo.ToString());
        //    return result;
        //}

        public PlanoContaDto CastModalToDto()
        {
            var planoContaDto = new PlanoContaDto(Id: this.Id, Descricao: this.Descricao, Tipo: this.Tipo[0], Ativo: this.Ativo);
            return planoContaDto;
        }


        public void DisableEnable()
        {
            this.Ativo = !this.Ativo;
        }

        public void CreateObject()
        {
            if (Tipo.IsEqualTo("R"))
            {
                CorTag = "volcano";
                Tipo = "Receita";
            }
            else if (Tipo.IsEqualTo("D"))
            {
                CorTag = "green";
                Tipo = "Despesa";
            }
        }
    }
}
