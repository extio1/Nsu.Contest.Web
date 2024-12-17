﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Nsu.Contest.Web.HRManager.Model.Data;

#nullable disable

namespace Nsu.Contest.Web.HRManager.Migrations
{
    [DbContext(typeof(HRManagerDbContext))]
    [Migration("20241216140150_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ContestJunior", b =>
                {
                    b.Property<Guid>("ContestId")
                        .HasColumnType("uuid");

                    b.Property<int>("JuniorsId")
                        .HasColumnType("integer");

                    b.HasKey("ContestId", "JuniorsId");

                    b.HasIndex("JuniorsId");

                    b.ToTable("ContestJunior");
                });

            modelBuilder.Entity("ContestTeamlead", b =>
                {
                    b.Property<Guid>("ContestId")
                        .HasColumnType("uuid");

                    b.Property<int>("TeamleadsId")
                        .HasColumnType("integer");

                    b.HasKey("ContestId", "TeamleadsId");

                    b.HasIndex("TeamleadsId");

                    b.ToTable("ContestTeamlead");
                });

            modelBuilder.Entity("Nsu.Contest.Web.Common.Entity.Contest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<double>("Score")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("double precision")
                        .HasDefaultValue(0.0)
                        .HasColumnName("Points");

                    b.HasKey("Id");

                    b.ToTable("Contest", (string)null);
                });

            modelBuilder.Entity("Nsu.Contest.Web.Common.Entity.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("EmployeeType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Employees", (string)null);

                    b.HasDiscriminator<string>("EmployeeType").IsComplete(true).HasValue("Employee");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Nsu.Contest.Web.Common.Entity.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid?>("ContestId")
                        .HasColumnType("uuid");

                    b.Property<int>("JuniorId")
                        .HasColumnType("integer");

                    b.Property<int>("TeamleadId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ContestId");

                    b.HasIndex("JuniorId");

                    b.HasIndex("TeamleadId");

                    b.ToTable("Team", (string)null);
                });

            modelBuilder.Entity("Nsu.Contest.Web.Common.Entity.Wishlist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.PrimitiveCollection<int[]>("DesiredEmployees")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<int>("ForEmployeeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ForEmployeeId");

                    b.ToTable("Wishlist", (string)null);
                });

            modelBuilder.Entity("Nsu.Contest.Web.Common.Entity.Junior", b =>
                {
                    b.HasBaseType("Nsu.Contest.Web.Common.Entity.Employee");

                    b.HasDiscriminator().HasValue("Junior");
                });

            modelBuilder.Entity("Nsu.Contest.Web.Common.Entity.Teamlead", b =>
                {
                    b.HasBaseType("Nsu.Contest.Web.Common.Entity.Employee");

                    b.HasDiscriminator().HasValue("Teamlead");
                });

            modelBuilder.Entity("ContestJunior", b =>
                {
                    b.HasOne("Nsu.Contest.Web.Common.Entity.Contest", null)
                        .WithMany()
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nsu.Contest.Web.Common.Entity.Junior", null)
                        .WithMany()
                        .HasForeignKey("JuniorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContestTeamlead", b =>
                {
                    b.HasOne("Nsu.Contest.Web.Common.Entity.Contest", null)
                        .WithMany()
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nsu.Contest.Web.Common.Entity.Teamlead", null)
                        .WithMany()
                        .HasForeignKey("TeamleadsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nsu.Contest.Web.Common.Entity.Team", b =>
                {
                    b.HasOne("Nsu.Contest.Web.Common.Entity.Contest", null)
                        .WithMany("Teams")
                        .HasForeignKey("ContestId");

                    b.HasOne("Nsu.Contest.Web.Common.Entity.Junior", "Junior")
                        .WithMany()
                        .HasForeignKey("JuniorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nsu.Contest.Web.Common.Entity.Teamlead", "Teamlead")
                        .WithMany()
                        .HasForeignKey("TeamleadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Junior");

                    b.Navigation("Teamlead");
                });

            modelBuilder.Entity("Nsu.Contest.Web.Common.Entity.Wishlist", b =>
                {
                    b.HasOne("Nsu.Contest.Web.Common.Entity.Employee", "ForEmployee")
                        .WithMany()
                        .HasForeignKey("ForEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ForEmployee");
                });

            modelBuilder.Entity("Nsu.Contest.Web.Common.Entity.Contest", b =>
                {
                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
