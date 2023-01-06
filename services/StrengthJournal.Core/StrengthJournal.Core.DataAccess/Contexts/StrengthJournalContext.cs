using Microsoft.EntityFrameworkCore;
using StrengthJournal.DataAccess.Model;
using StrengthJournal.DataAccess.Model.Virtual;
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
        public DbSet<WeightUnit> WeightUnits { get; set; }
        public DbSet<ExerciseHistoryLine> ExerciseHistory { get; set; }
        public DbSet<AppError> AppErrors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<WeeklyVolumeReportLine> WeeklyVolumeReportLines { get; set; }
        public DbSet<WeeklySBDTonnageReportLine> WeeklySBDTonnageReportLines { get; set; }

        public StrengthJournalContext(DbContextOptions options) : base(options)
        { }

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

            modelBuilder.Entity<WeightUnit>()
                .HasData(
                    new WeightUnit()
                    {
                        Id = Guid.Parse("BF8DF35B-2F45-4A79-A49C-D3ACA4A12CD6"),
                        FullName = "Pounds",
                        Abbreviation = "lbs",
                        RatioToKg = 2.2M
                    },
                    new WeightUnit()
                    {
                        Id = Guid.Parse("4BC96550-F274-4A90-978B-92A398F8C49D"),
                        FullName = "Kilograms",
                        Abbreviation = "kg",
                        RatioToKg = 1.0M
                    }
                );

            modelBuilder.Entity<ExerciseHistoryLine>()
                .ToView("vwExerciseHistory")
                .HasNoKey();

            modelBuilder.Entity<WeeklyVolumeReportLine>()
                .ToTable(nameof(WeeklyVolumeReportLine), t => t.ExcludeFromMigrations())
                .HasNoKey();

            modelBuilder.Entity<WeeklySBDTonnageReportLine>()
                .ToTable(nameof(WeeklySBDTonnageReportLine), t => t.ExcludeFromMigrations())
                .HasNoKey();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
