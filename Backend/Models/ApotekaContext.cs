using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class ApotekaContext : DbContext
    {

        public DbSet<Apoteka> Apoteke {get; set;}
        public DbSet<Recept> Recepti {get; set;}
        public DbSet<Lek> Lekovi {get; set;}

        public ApotekaContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apoteka>().HasMany(apoteka => apoteka.Lekovi).WithOne(lek => lek.Apoteka).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Lek>().HasOne(lek => lek.Apoteka).WithMany(apoteka => apoteka.Lekovi);

            modelBuilder.Entity<Apoteka>().HasMany(apoteka => apoteka.Recepti).WithOne(recept => recept.Apoteka).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Recept>().HasOne(recept => recept.Apoteka).WithMany(apoteka => apoteka.Recepti);
            
            
            modelBuilder.Entity<Recept>().HasMany(recept => recept.Lekovi).WithOne(lek => lek.Recept).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Lek>().HasOne(lek => lek.Recept).WithMany(recept => recept.Lekovi);
            

        }       
    }
}
