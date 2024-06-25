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

        public PlanoContaModel(PlanoConta planoConta)
        {
            Ativo = planoConta.Ativo;
            Descricao = planoConta.Descricao;
            Id = planoConta.Id;
            CreateObject(planoConta.Tipo);
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public string Tipo { get; set; }
        public string? CorTag { get; set; }

        public PlanoConta CastModalToDto(bool? ativo)
        {
            return new()
            {
                Id = this.Id,
                Descricao = this.Descricao,
                Tipo = this.Tipo[0],
                Ativo = ativo ?? this.Ativo
            };
        }

        public void DisableEnable()
        {
            this.Ativo = !this.Ativo;
        }

        public void CreateObject(char? tipo = null)
        {
            var tipoTransacao = tipo ?? Tipo[0] ;
            if (tipoTransacao.ToString().IsEqualTo("R"))
            {
                CorTag = "green";
                Tipo = "Receita";
            }
            else if (tipoTransacao.ToString().IsEqualTo("D"))
            {
                CorTag = "volcano";
                Tipo = "Despesa";
            }
        }
    }
}
