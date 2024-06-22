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

        public PlanoConta CastModalToDto()
        {
            return new()
            {
                Id = this.Id,
                Descricao = this.Descricao,
                Tipo = this.Tipo[0],
                Ativo = this.Ativo
            };
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
        private void CreateObject(char tipo)
        {
            if (tipo.ToString().IsEqualTo("R"))
            {
                CorTag = "volcano";
                Tipo = "Receita";
            }
            else if (tipo.ToString().IsEqualTo("D"))
            {
                CorTag = "green";
                Tipo = "Despesa";
            }
        }
    }
}
