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

        public DbSet<Klijent> Klijenti {get; set;}

        public DbSet<LekUReceptu> LekoviUReceptu { get ; set;}
        public ApotekaContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Apoteka>().HasMany(apoteka => apoteka.Lekovi).WithOne(lek => lek.Apoteka).OnDelete(DeleteBehavior.Restrict);
            // modelBuilder.Entity<Lek>().HasOne(lek => lek.Apoteka).WithMany(apoteka => apoteka.Lekovi);            
            
            // modelBuilder.Entity<Recept>().HasMany(recept => recept.Lekovi).WithOne(lek => lek.Recept).OnDelete(DeleteBehavior.Cascade);
            // modelBuilder.Entity<Lek>().HasOne(lek => lek.Recept).WithMany(recept => recept.Lekovi);

            // modelBuilder.Entity<Klijent>().HasMany(klijent => klijent.Recepti).WithOne(recept => recept.Klijent).OnDelete(DeleteBehavior.Cascade);            
            // modelBuilder.Entity<Recept>().HasOne(recept => recept.Klijent).WithMany(klijent => klijent.Recepti);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LekUReceptu>()
            .HasOne(recept => recept.Recept)
            .WithMany(recept => recept.Lekovi)
            .HasForeignKey(lek => lek.ReceptID);
        
            modelBuilder.Entity<LekUReceptu>()
            .HasOne(lek => lek.Lek)
            .WithMany(lek => lek.Recepti)
            .HasForeignKey(recept => recept.LekID);
        }       
    }
}
