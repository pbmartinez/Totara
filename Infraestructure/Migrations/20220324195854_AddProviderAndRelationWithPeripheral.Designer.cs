﻿// <auto-generated />
using System;
using Infrastructure.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(UnitOfWorkContainer))]
    [Migration("20220324195854_AddProviderAndRelationWithPeripheral")]
    partial class AddProviderAndRelationWithPeripheral
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Brand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GatewayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sponsor")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GatewayId")
                        .IsUnique();

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("Domain.Entities.Gateway", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ipv4Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Gateway");
                });

            modelBuilder.Entity("Domain.Entities.Peripheral", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GatewayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Vendor")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GatewayId");

                    b.ToTable("Peripheral");
                });

            modelBuilder.Entity("Domain.Entities.Provider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PeripheralId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PeripheralId")
                        .IsUnique();

                    b.ToTable("Provider");
                });

            modelBuilder.Entity("Domain.Entities.Brand", b =>
                {
                    b.HasOne("Domain.Entities.Gateway", "Gateway")
                        .WithOne("Brand")
                        .HasForeignKey("Domain.Entities.Brand", "GatewayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gateway");
                });

            modelBuilder.Entity("Domain.Entities.Peripheral", b =>
                {
                    b.HasOne("Domain.Entities.Gateway", "Gateway")
                        .WithMany("Peripherals")
                        .HasForeignKey("GatewayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gateway");
                });

            modelBuilder.Entity("Domain.Entities.Provider", b =>
                {
                    b.HasOne("Domain.Entities.Peripheral", "Peripheral")
                        .WithOne("Provider")
                        .HasForeignKey("Domain.Entities.Provider", "PeripheralId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Peripheral");
                });

            modelBuilder.Entity("Domain.Entities.Gateway", b =>
                {
                    b.Navigation("Brand");

                    b.Navigation("Peripherals");
                });

            modelBuilder.Entity("Domain.Entities.Peripheral", b =>
                {
                    b.Navigation("Provider");
                });
#pragma warning restore 612, 618
        }
    }
}
