﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using authApi.Models;

namespace authApi.Migrations
{
    [DbContext(typeof(AuthDbContext))]
    [Migration("20190116123329_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("authApi.Models.AppUser", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("UserId");

                    b.ToTable("Users","sec");
                });

            modelBuilder.Entity("authApi.Models.AppUserClaim", b =>
                {
                    b.Property<Guid>("ClaimId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType")
                        .IsRequired();

                    b.Property<string>("ClaimValue")
                        .IsRequired();

                    b.Property<Guid>("UserId");

                    b.HasKey("ClaimId");

                    b.ToTable("UserClaims","sec");
                });
#pragma warning restore 612, 618
        }
    }
}
