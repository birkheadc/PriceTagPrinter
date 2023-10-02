﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PriceTagPrinter.Contexts;

#nullable disable

namespace PriceTagPrinter.Migrations
{
    [DbContext(typeof(PriceTagContext))]
    partial class PriceTagContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.22");

            modelBuilder.Entity("PriceTagPrinter.Models.PriceTag", b =>
                {
                    b.Property<string>("GoodsCode")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("GoodsName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("GoodsPrice")
                        .HasColumnType("INTEGER");

                    b.HasKey("GoodsCode");

                    b.ToTable("PriceTags");
                });
#pragma warning restore 612, 618
        }
    }
}
