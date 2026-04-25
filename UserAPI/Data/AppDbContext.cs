using Microsoft.EntityFrameworkCore;
using UserAPI.Models;

namespace UserAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Produit> Produits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Contrainte d'unicité sur l'email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // On précise comment le stocker en SQL Server
            modelBuilder.Entity<Produit>()
                .Property(p => p.Prix)  // 10 = nombre total de chiffres
                .HasPrecision(10, 2);   // 2 = chiffres après la virgule
        }
    }
}