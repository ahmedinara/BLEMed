﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NFC.Api.Entities;

namespace NFC.Core.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220103150942_LogTable")]
    partial class LogTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BSMediator.Core.Entities.ApplicationGroupSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationGroupSettings");
                });

            modelBuilder.Entity("BSMediator.Core.Entities.ApplicationSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("applicationGroupSettingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("applicationGroupSettingId");

                    b.ToTable("ApplicationSettings");
                });

            modelBuilder.Entity("BSMediator.Core.Entities.ApplicationSetting", b =>
                {
                    b.HasOne("BSMediator.Core.Entities.ApplicationGroupSetting", "applicationGroupSetting")
                        .WithMany()
                        .HasForeignKey("applicationGroupSettingId");

                    b.Navigation("applicationGroupSetting");
                });
#pragma warning restore 612, 618
        }
    }
}
