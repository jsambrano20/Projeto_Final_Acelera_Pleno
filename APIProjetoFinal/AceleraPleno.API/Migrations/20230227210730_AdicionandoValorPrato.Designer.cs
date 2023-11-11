﻿// <auto-generated />
using System;
using AceleraPleno.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AceleraPleno.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230227210730_AdicionandoValorPrato")]
    partial class AdicionandoValorPrato
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AceleraPleno.API.Models.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("AceleraPleno.API.Models.Log", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Acao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dados")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Funcionario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdTabela")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Tabela")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("AceleraPleno.API.Models.Mesa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ambiente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Lugares")
                        .HasColumnType("int");

                    b.Property<bool>("Ocupada")
                        .HasColumnType("bit");

                    b.Property<int>("StatusMesa")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Mesas");
                });

            modelBuilder.Entity("AceleraPleno.API.Models.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DtRecebimento")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MesaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PratoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<int>("StatusPedido")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("MesaId");

                    b.HasIndex("PratoId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("AceleraPleno.API.Models.Prato", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Foto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Pratos");
                });

            modelBuilder.Entity("AceleraPleno.API.Models.Mesa", b =>
                {
                    b.HasOne("AceleraPleno.API.Models.Cliente", "Clientes")
                        .WithMany()
                        .HasForeignKey("ClienteId");

                    b.Navigation("Clientes");
                });

            modelBuilder.Entity("AceleraPleno.API.Models.Pedido", b =>
                {
                    b.HasOne("AceleraPleno.API.Models.Mesa", "Mesas")
                        .WithMany()
                        .HasForeignKey("MesaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AceleraPleno.API.Models.Prato", "Pratos")
                        .WithMany()
                        .HasForeignKey("PratoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mesas");

                    b.Navigation("Pratos");
                });
#pragma warning restore 612, 618
        }
    }
}