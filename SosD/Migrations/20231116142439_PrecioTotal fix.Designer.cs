﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SosD.Repos;

#nullable disable

namespace SosD.Migrations
{
    [DbContext(typeof(SosDContext))]
    [Migration("20231116142439_PrecioTotal fix")]
    partial class PrecioTotalfix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SosD.Models.Diseño", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.ToTable("Diseño");
                });

            modelBuilder.Entity("SosD.Models.Presupuesto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("DiseñoId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("ImagemPelicula")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PrecioUni")
                        .HasColumnType("int");

                    b.Property<int?>("TipoPrendaId")
                        .HasColumnType("int");

                    b.Property<int?>("TipoTelaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DiseñoId");

                    b.HasIndex("TipoPrendaId");

                    b.HasIndex("TipoTelaId");

                    b.ToTable("Presupuesto");
                });

            modelBuilder.Entity("SosD.Models.TipoPrenda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("TipoTelaId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoTelaId");

                    b.ToTable("Tipo Prenda");
                });

            modelBuilder.Entity("SosD.Models.TipoTela", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.ToTable("Tipo Tela");
                });

            modelBuilder.Entity("SosD.ViewModels.PresupuestoViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenPrenda")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PrecioUni")
                        .HasColumnType("int");

                    b.Property<int?>("TipoPrendaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoPrendaId");

                    b.ToTable("PresupuestoViewModel");
                });

            modelBuilder.Entity("SosD.Models.Presupuesto", b =>
                {
                    b.HasOne("SosD.Models.Diseño", "Diseño")
                        .WithMany()
                        .HasForeignKey("DiseñoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SosD.Models.TipoPrenda", "TipoPrenda")
                        .WithMany()
                        .HasForeignKey("TipoPrendaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SosD.Models.TipoTela", "TipoTela")
                        .WithMany()
                        .HasForeignKey("TipoTelaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Diseño");

                    b.Navigation("TipoPrenda");

                    b.Navigation("TipoTela");
                });

            modelBuilder.Entity("SosD.Models.TipoPrenda", b =>
                {
                    b.HasOne("SosD.Models.TipoTela", "TipoTela")
                        .WithMany()
                        .HasForeignKey("TipoTelaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoTela");
                });

            modelBuilder.Entity("SosD.ViewModels.PresupuestoViewModel", b =>
                {
                    b.HasOne("SosD.Models.TipoPrenda", "TipoPrenda")
                        .WithMany()
                        .HasForeignKey("TipoPrendaId");

                    b.Navigation("TipoPrenda");
                });
#pragma warning restore 612, 618
        }
    }
}
