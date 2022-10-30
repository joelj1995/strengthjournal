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
        public DbSet<WorkoutLogEntry> WorkoutLogEntries { get; set; }
        public DbSet<WorkoutLogEntrySet> WorkoutLogEntrySets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>()
                .HasData(
                    new Exercise()
                    {
                        Id = Guid.Parse("BD99B901-26EB-4F12-90CA-91D6A7CC673D"),
                        Name = "Squat"
                    },
                    new Exercise()
                    {
                        Id = Guid.Parse("F06FDF7F-76C0-4C67-8C3A-994009E75D5A"),
                        Name = "Bench Press"
                    },
                    new Exercise()
                    {
                        Id = Guid.Parse("84E827A0-CA29-42F8-9B82-C0A79886FC30"),
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
