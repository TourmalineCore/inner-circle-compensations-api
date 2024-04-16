﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(CompensationsDbContext))]
    partial class CompensationsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Compensation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCompensation")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateCreateCompensation")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("boolean");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Compensations");
                });
#pragma warning restore 612, 618
        }
    }
}
