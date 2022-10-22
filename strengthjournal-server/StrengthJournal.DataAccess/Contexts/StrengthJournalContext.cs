using Microsoft.EntityFrameworkCore;
using StrengthJournal.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthJournal.DataAccess.Contexts
{
    public class StrengthJournalContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>()
                .HasData(
                    new Exercise()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Squat"
                    },
                    new Exercise()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Bench Press"
                    },
                    new Exercise()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Deadlift"
                    }
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost;Database=StrengthJournal;Trusted_Connection=True");
        }
    }
}
