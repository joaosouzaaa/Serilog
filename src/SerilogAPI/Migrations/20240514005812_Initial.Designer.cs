﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SerilogAPI.Data.DatabaseContexts;

#nullable disable

namespace SerilogAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240514005812_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SerilogAPI.Entities.House", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("address");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<int>("NumberOfRooms")
                        .HasColumnType("integer")
                        .HasColumnName("number_of_rooms");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("price");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("House");
                });

            modelBuilder.Entity("SerilogAPI.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer")
                        .HasColumnName("age");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("SerilogAPI.Entities.RentalDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("end_time");

                    b.Property<int>("HouseId")
                        .HasColumnType("integer");

                    b.Property<int>("RenterId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("start_time");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.HasIndex("RenterId");

                    b.ToTable("RentalDate");
                });

            modelBuilder.Entity("SerilogAPI.Entities.House", b =>
                {
                    b.HasOne("SerilogAPI.Entities.Person", "Owner")
                        .WithMany("Houses")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Owner_House");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SerilogAPI.Entities.RentalDate", b =>
                {
                    b.HasOne("SerilogAPI.Entities.House", "House")
                        .WithMany("RentalDates")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_RentalDate_House");

                    b.HasOne("SerilogAPI.Entities.Person", "Renter")
                        .WithMany()
                        .HasForeignKey("RenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("House");

                    b.Navigation("Renter");
                });

            modelBuilder.Entity("SerilogAPI.Entities.House", b =>
                {
                    b.Navigation("RentalDates");
                });

            modelBuilder.Entity("SerilogAPI.Entities.Person", b =>
                {
                    b.Navigation("Houses");
                });
#pragma warning restore 612, 618
        }
    }
}
