﻿// <auto-generated />
using System;
using EmlakYonetim.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmlakYonetim.Migrations
{
    [DbContext(typeof(VeriTabani))]
    partial class VeriTabaniModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmlakYonetim.Models.Admin", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Eposta")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Sifre")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("ID");

                    b.ToTable("Adminler");
                });

            modelBuilder.Entity("EmlakYonetim.Models.Danisman", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("Ad")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Soyad")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Telefon")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("ePosta")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Danismanlar");
                });

            modelBuilder.Entity("EmlakYonetim.Models.Haber", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("Baslik")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FotoUrl")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Ozet")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("YayinTarihi")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Haberler");
                });

            modelBuilder.Entity("EmlakYonetim.Models.IlanPaylasma", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("Aciklama")
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Adres")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("BinaYasi")
                        .HasColumnType("int");

                    b.Property<string>("EmlakTipi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Fiyat")
                        .HasColumnType("real");

                    b.Property<string>("Foto")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Il")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("IlanTarihi")
                        .HasColumnType("date");

                    b.Property<string>("Ilce")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Kategori")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("MetreKare")
                        .HasColumnType("int");

                    b.Property<string>("OdaSayisi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Ilanlar");
                });

            modelBuilder.Entity("EmlakYonetim.Models.Kullanici", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("Ad")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sifre")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Soyad")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Telefon")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("ePosta")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Kullanicilar");
                });

            modelBuilder.Entity("EmlakYonetim.Models.Odeme", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("Cvv")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("EmlakTuru")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KartNumarasi")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("KiralamaSuresi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("SonKulTar")
                        .HasColumnType("date");

                    b.Property<string>("adSoyad")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("emlakci")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ilanNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Odemeler");
                });

            modelBuilder.Entity("EmlakYonetim.Models.OneCikanIlanlar", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<long>("IlanID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("IlanID");

                    b.ToTable("OneCikanIlanlar");
                });

            modelBuilder.Entity("EmlakYonetim.Models.Randevu", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("adSoyad")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ePosta")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("emlakci")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ilanNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeOnly>("randevuSaati")
                        .HasColumnType("time");

                    b.Property<DateOnly>("randevuTarihi")
                        .HasColumnType("date");

                    b.Property<string>("tel")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("ID");

                    b.ToTable("Randevular");
                });

            modelBuilder.Entity("EmlakYonetim.Models.yorum", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Derecelendirme")
                        .HasColumnType("int");

                    b.Property<string>("YorumMetni")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Yorumlar");
                });

            modelBuilder.Entity("EmlakYonetim.Models.OneCikanIlanlar", b =>
                {
                    b.HasOne("EmlakYonetim.Models.IlanPaylasma", "Ilan")
                        .WithMany()
                        .HasForeignKey("IlanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ilan");
                });
#pragma warning restore 612, 618
        }
    }
}
