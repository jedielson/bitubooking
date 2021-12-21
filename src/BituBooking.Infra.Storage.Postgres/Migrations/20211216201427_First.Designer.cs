﻿// <auto-generated />
using System;
using BituBooking.Infra.Storage.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BituBooking.Infra.Storage.Postgres.Migrations
{
    [DbContext(typeof(BookingContext))]
    [Migration("20211216201427_First")]
    partial class First
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BituBooking.Domain.Management.Hotel", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Code")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("StarsOfCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<int>("StarsOfRating")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.HasKey("Identifier");

                    b.ToTable("Hotel", "public");
                });

            modelBuilder.Entity("BituBooking.Domain.Management.Room", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AvailableQuantity")
                        .HasColumnType("integer");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<Guid>("Code")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<Guid>("HotelId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<decimal>("PricePerNight")
                        .HasColumnType("numeric");

                    b.HasKey("Identifier");

                    b.HasIndex("HotelId");

                    b.ToTable("Room", "public");
                });

            modelBuilder.Entity("BituBooking.Domain.Management.Hotel", b =>
                {
                    b.OwnsOne("BituBooking.Domain.Management.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("HotelIdentifier")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<int>("ZipCode")
                                .HasColumnType("integer");

                            b1.HasKey("HotelIdentifier");

                            b1.ToTable("Hotel", "public");

                            b1.WithOwner()
                                .HasForeignKey("HotelIdentifier");
                        });

                    b.OwnsOne("BituBooking.Domain.Management.Contacts", "Contacts", b1 =>
                        {
                            b1.Property<Guid>("HotelIdentifier")
                                .HasColumnType("uuid");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("Mobile")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("Phone")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.HasKey("HotelIdentifier");

                            b1.ToTable("Hotel", "public");

                            b1.WithOwner()
                                .HasForeignKey("HotelIdentifier");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Contacts")
                        .IsRequired();
                });

            modelBuilder.Entity("BituBooking.Domain.Management.Room", b =>
                {
                    b.HasOne("BituBooking.Domain.Management.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("BituBooking.Domain.Management.Hotel", b =>
                {
                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}