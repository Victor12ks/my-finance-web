using Microsoft.EntityFrameworkCore;
using MyFinanceWeb.Domain.Dtos;

namespace MyFinanceWeb.Infra.Contexts
{
    public class MyFinanceDbContext : DbContext
    {
        public MyFinanceDbContext(DbContextOptions<MyFinanceDbContext> options) : base(options) { }

        public DbSet<PlanoConta> PlanoConta { get; set; }
        public DbSet<Transacao> Transacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transacao>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Transacao>()
                .HasOne(t => t.PlanoConta)
                .WithMany()
                .HasForeignKey(t => t.PlanoContaId);

            modelBuilder.Entity<PlanoConta>()
                .HasKey(pc => pc.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
