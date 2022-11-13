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
    [Migration("20221113223326_UpdateTypes")]
    partial class UpdateTypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("StrengthJournal.DataAccess.Model.AppError", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ApiErrorTraceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppErrors");
                });

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

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ExternalId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StrengthJournal.DataAccess.Model.Virtual.ExerciseHistoryLine", b =>
                {
                    b.Property<decimal?>("BodyWeightKg")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("BodyWeightLbs")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("EntryDateUTC")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("RPE")
                        .HasColumnType("bigint");

                    b.Property<long?>("Reps")
                        .HasColumnType("bigint");

                    b.Property<long?>("TargetReps")
                        .HasColumnType("bigint");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("WeightKg")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("WeightLbs")
                        .HasColumnType("decimal(18,2)");

                    b.ToView("vwExerciseHistory");
                });

            modelBuilder.Entity("StrengthJournal.DataAccess.Model.WeightUnit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("RatioToKg")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("WeightUnits");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bf8df35b-2f45-4a79-a49c-d3aca4a12cd6"),
                            Abbreviation = "lbs",
                            FullName = "Pounds",
                            RatioToKg = 2.2m
                        },
                        new
                        {
                            Id = new Guid("4bc96550-f274-4a90-978b-92a398f8c49d"),
                            Abbreviation = "kg",
                            FullName = "Kilograms",
                            RatioToKg = 1.0m
                        });
                });

            modelBuilder.Entity("StrengthJournal.DataAccess.Model.WorkoutLogEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("BodyWeightPIT")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("BodyWeightPITUnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EntryDateUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BodyWeightPITUnitId");

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

                    b.Property<int?>("RPE")
                        .HasColumnType("int");

                    b.Property<int?>("Reps")
                        .HasColumnType("int");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.Property<int?>("TargetReps")
                        .HasColumnType("int");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("WeightUnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkoutLogEntryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("WeightUnitId");

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
                    b.HasOne("StrengthJournal.DataAccess.Model.WeightUnit", "BodyWeightPITUnit")
                        .WithMany()
                        .HasForeignKey("BodyWeightPITUnitId");

                    b.HasOne("StrengthJournal.DataAccess.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BodyWeightPITUnit");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StrengthJournal.DataAccess.Model.WorkoutLogEntrySet", b =>
                {
                    b.HasOne("StrengthJournal.DataAccess.Model.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StrengthJournal.DataAccess.Model.WeightUnit", "WeightUnit")
                        .WithMany()
                        .HasForeignKey("WeightUnitId");

                    b.HasOne("StrengthJournal.DataAccess.Model.WorkoutLogEntry", "WorkoutLogEntry")
                        .WithMany("Sets")
                        .HasForeignKey("WorkoutLogEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("WeightUnit");

                    b.Navigation("WorkoutLogEntry");
                });

            modelBuilder.Entity("StrengthJournal.DataAccess.Model.WorkoutLogEntry", b =>
                {
                    b.Navigation("Sets");
                });
#pragma warning restore 612, 618
        }
    }
}
