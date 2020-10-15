using System;
using System.Collections.Generic;
using System.Text;
using Cinema_Database.Model;
using Microsoft.EntityFrameworkCore;
namespace Cinema_Database.DbCinemaContext
{
    public class DbCimenaContext:DbContext
    {
        public DbSet<Customers> Customers { get; set; }

        public DbSet<Halls> Halls { get; set; }

        public DbSet<Movies> Movies { get; set; }

        public DbSet<Projections> Projections { get; set; }

        public DbSet<Seats> Seats { get; set; }

        public DbSet<Tickets> Tickets { get; set; }

        protected  override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.Connection);    
        }
        
        protected  override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>()
               .HasMany(c => c.Tickets)
               .WithOne(t => t.Customer)
               .HasForeignKey(t => t.CustomerId);


            modelBuilder.Entity<Halls>()
                .HasMany(h => h.Projections)
                .WithOne(s => s.Halls)
                .HasForeignKey(s => s.HallId);

            
            modelBuilder.Entity<Halls>()
                .HasMany(h => h.Seats)
                .WithOne(s => s.Halls)
                .HasForeignKey(s => s.HallId);

            
            modelBuilder.Entity<Movies>()
                .HasMany(m => m.Projections)
                .WithOne(p => p.Movie)
                .HasForeignKey(p => p.MovieId);

            
            modelBuilder.Entity<Seats>()
                .HasOne(s => s.Halls)
                .WithMany(h => h.Seats)
                .HasForeignKey(s=>s.HallId);
           
            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.Projection)
                .WithMany(p => p.Tickets)
                .HasForeignKey(p => p.ProjectionsId);
        }

    }
}
