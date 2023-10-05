﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NauHelper.Infrastructure.Database.EF;

#nullable disable

namespace NauHelper.Infrastructure.Database.EF.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231004185149_Add foreign keys for specialty and group")]
    partial class Addforeignkeysforspecialtyandgroup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NauHelper.Core.Entities.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("NauHelper.Core.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpecialtyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("NauHelper.Core.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Student"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Administrator"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Owner"
                        });
                });

            modelBuilder.Entity("NauHelper.Core.Entities.RoleSettingKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DefaultValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoleSettingKeys");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DefaultValue = "ua",
                            Key = "Language",
                            Type = "System.String"
                        },
                        new
                        {
                            Id = 2,
                            DefaultValue = "",
                            Key = "Faculty",
                            RoleId = 1,
                            Type = "System.Int32"
                        },
                        new
                        {
                            Id = 3,
                            DefaultValue = "",
                            Key = "Group",
                            RoleId = 1,
                            Type = "System.Int32"
                        },
                        new
                        {
                            Id = 4,
                            DefaultValue = "",
                            Key = "Specialty",
                            RoleId = 1,
                            Type = "System.Int32"
                        });
                });

            modelBuilder.Entity("NauHelper.Core.Entities.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleSettingKeyId")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("NauHelper.Core.Entities.Specialty", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Specialties");
                });

            modelBuilder.Entity("NauHelper.Core.Entities.User", b =>
                {
                    b.Property<long>("TelegramId")
                        .HasColumnType("bigint");

                    b.HasKey("TelegramId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NauHelper.Core.Entities.UserRole", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = 502351239L,
                            RoleId = 3
                        });
                });

            modelBuilder.Entity("NauHelper.Core.Entities.Group", b =>
                {
                    b.HasOne("NauHelper.Core.Entities.Specialty", null)
                        .WithMany()
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NauHelper.Core.Entities.Specialty", b =>
                {
                    b.HasOne("NauHelper.Core.Entities.Faculty", null)
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}