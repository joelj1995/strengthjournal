﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StrengthJournal.DataAccess.Contexts;

#nullable disable

namespace StrengthJournal.DataAccess.Migrations
{
    [DbContext(typeof(StrengthJournalContext))]
    [Migration("20221030010504_CreateWorkoutLogEntryEntity")]
    partial class CreateWorkoutLogEntryEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("StrengthJournal.DataAccess.Model.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("ParentExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.ToTable("Exercises");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bd99b901-26eb-4f12-90ca-91d6a7cc673d"),
                            Name = "Squat"
                        },
                        new
                        {
                            Id = new Guid("f06fdf7f-76c0-4c67-8c3a-994009e75d5a"),
                            Name = "Bench Press"
                        },
                        new
                        {
                            Id = new Guid("84e827a0-ca29-42f8-9b82-c0a79886fc30"),
                            Name = "Deadlift"
                        });
                });

            modelBuilder.Entity("StrengthJournal.DataAccess.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Handle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Handle");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StrengthJournal.DataAccess.Model.WorkoutLogEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EntryDateUTC")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("WorkoutLogEntries");
                });

            modelBuilder.Entity("StrengthJournal.DataAccess.Model.WorkoutLogEntrySet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("RPE")
                        .HasColumnType("bigint");

                    b.Property<long?>("Reps")
                        .HasColumnType("bigint");

                    b.Property<long?>("TargetReps")
                        .HasColumnType("bigint");

                    b.Property<decimal?>("WeightKg")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("WeightLbs")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("WorkoutLogEntryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("WorkoutLogEntryId");

                    b.ToTable("WorkoutLogEntrySets");
                });

            modelBuilder.Entity("StrengthJournal.DataAccess.Model.Exercise", b =>
                {
                    b.HasOne("StrengthJournal.DataAccess.Model.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("StrengthJournal.DataAccess.Model.WorkoutLogEntry", b =>
                {
                    b.HasOne("StrengthJournal.DataAccess.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StrengthJournal.DataAccess.Model.WorkoutLogEntrySet", b =>
                {
                    b.HasOne("StrengthJournal.DataAccess.Model.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StrengthJournal.DataAccess.Model.WorkoutLogEntry", "WorkoutLogEntry")
                        .WithMany()
                        .HasForeignKey("WorkoutLogEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("WorkoutLogEntry");
                });
#pragma warning restore 612, 618
        }
    }
}
