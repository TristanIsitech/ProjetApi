using Microsoft.EntityFrameworkCore;
using ProjetApi.Entities;

namespace ProjetApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {
            
        }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Element> Elements { get; set; }

        public DbSet<Faiblesse> Faiblesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Créer la clée étrangère unique vers unique de Carte vers Carte
            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.evolution_)
                .WithOne();

            // Créer la clée étrangère unique vers plusieur de Carte vers Element
            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.element)
                .WithMany(e => e.pokemon)
                .HasForeignKey(p => p.element_name);

            // Créer la clée primaire multiple de la table faiblesse
            modelBuilder.Entity<Faiblesse>()
                .HasKey(f => new { f.element_faiblesse, f.element_resistance });
            // Créer la clée étrangère table faiblesse 
            modelBuilder.Entity<Faiblesse>()
                .HasOne(f => f.resistance)
                .WithMany(e => e.resistance)
                .HasForeignKey(f => f.element_resistance);
            // Créer la clée primaire multiple de la table faiblesse
            modelBuilder.Entity<Faiblesse>()
                .HasOne(f => f.faiblesse)
                .WithMany(e => e.faiblesse)
                .HasForeignKey(f => f.element_faiblesse);
        }
    }
}