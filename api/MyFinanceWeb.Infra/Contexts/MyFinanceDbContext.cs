using Microsoft.EntityFrameworkCore;
using MyFinanceWeb.Domain.Dtos;

namespace MyFinanceWeb.Infra.Contexts
{
    public class MyFinanceDbContext : DbContext
    {
        public MyFinanceDbContext(DbContextOptions<MyFinanceDbContext> options) : base(options) { }


        public DbSet<PlanoContaDto> PlanoConta { get; set; }
        public DbSet<TransacaoDto> Transacao { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options) : base(options) { }
        //{
        //}
    }
}
