﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProcessPayment.Data;

namespace ProcessPayment.Data.Migrations
{
    [DbContext(typeof(PaymentContext))]
    [Migration("20210219123712_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("ProcessPayment.Domain.Entities.Payment", b =>
                {
                    b.Property<string>("PaymentId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CardHolder")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreditCardNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PaymentStateId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecurityCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("TEXT")
                        .IsFixedLength(true);

                    b.HasKey("PaymentId");

                    b.HasIndex("PaymentStateId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("ProcessPayment.Domain.Entities.PaymentState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PaymentState");
                });

            modelBuilder.Entity("ProcessPayment.Domain.Entities.Payment", b =>
                {
                    b.HasOne("ProcessPayment.Domain.Entities.PaymentState", "PaymentState")
                        .WithMany()
                        .HasForeignKey("PaymentStateId");

                    b.Navigation("PaymentState");
                });
#pragma warning restore 612, 618
        }
    }
}
