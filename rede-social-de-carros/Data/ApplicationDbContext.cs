using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using rede_social_de_carros.Models;

namespace rede_social_de_carros.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship to set null on delete
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Endereco)
                .WithMany()
                .HasForeignKey(u => u.EnderecoId)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Automovel>? Automoveis { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<Endereco>? Endereco { get; set; }
    }
}