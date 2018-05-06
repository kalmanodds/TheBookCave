﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TheBookCave.Data;

namespace TheBookCave.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180506220051_ConstructorCartWishlist")]
    partial class ConstructorCartWishlist
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TheBookCave.Data.EntityModels.BookEntityModel.BookEntityModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<int?>("DatePublishedID");

                    b.Property<string>("Description");

                    b.Property<string>("Genre");

                    b.Property<string>("ISBN10");

                    b.Property<string>("ISBN13");

                    b.Property<string>("Image");

                    b.Property<int>("InStock");

                    b.Property<int>("NumberOfCopiesSold");

                    b.Property<int>("NumberOfPages");

                    b.Property<int>("NumberOfRatings");

                    b.Property<double>("Price");

                    b.Property<string>("Publisher");

                    b.Property<double>("Rating");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.HasIndex("DatePublishedID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("TheBookCave.Data.EntityModels.OrderBookConnectionEntityModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookID");

                    b.Property<int>("OrderID");

                    b.HasKey("ID");

                    b.ToTable("OrderBookConnections");
                });

            modelBuilder.Entity("TheBookCave.Data.EntityModels.OrderEntityModel.OrderEntityModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DateOrderID");

                    b.Property<bool>("IsReady");

                    b.Property<bool>("IsReceived");

                    b.Property<bool>("IsShipped");

                    b.Property<int?>("ShippingAddressID");

                    b.Property<double>("TotalPrice");

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("DateOrderID");

                    b.HasIndex("ShippingAddressID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TheBookCave.Data.EntityModels.RatingEntityModel.RatingEntityModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookID");

                    b.Property<string>("Comment");

                    b.Property<int>("Score");

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("TheBookCave.Data.EntityModels.UserBookCartConnectionEntityModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookID");

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.ToTable("UserBookCartConnections");
                });

            modelBuilder.Entity("TheBookCave.Data.EntityModels.UserBookWishlistConnectionEntityModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookID");

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.ToTable("UserBookWishlistConnections");
                });

            modelBuilder.Entity("TheBookCave.Data.EntityModels.UserEntityModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AddressID");

                    b.Property<string>("Email");

                    b.Property<int?>("FavoriteBookID");

                    b.Property<string>("FirstName");

                    b.Property<string>("Image");

                    b.Property<bool>("IsPremium");

                    b.Property<string>("LastName");

                    b.Property<string>("userID");

                    b.HasKey("ID");

                    b.HasIndex("AddressID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TheBookCave.Models.AddressModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<int>("HouseNumber");

                    b.Property<string>("StreetName");

                    b.Property<int>("Zip");

                    b.HasKey("ID");

                    b.ToTable("AddressModel");
                });

            modelBuilder.Entity("TheBookCave.Models.DateModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Day");

                    b.Property<int>("Month");

                    b.Property<int>("Year");

                    b.HasKey("ID");

                    b.ToTable("DateModel");
                });

            modelBuilder.Entity("TheBookCave.Data.EntityModels.BookEntityModel.BookEntityModel", b =>
                {
                    b.HasOne("TheBookCave.Models.DateModel", "DatePublished")
                        .WithMany()
                        .HasForeignKey("DatePublishedID");
                });

            modelBuilder.Entity("TheBookCave.Data.EntityModels.OrderEntityModel.OrderEntityModel", b =>
                {
                    b.HasOne("TheBookCave.Models.DateModel", "DateOrder")
                        .WithMany()
                        .HasForeignKey("DateOrderID");

                    b.HasOne("TheBookCave.Models.AddressModel", "ShippingAddress")
                        .WithMany()
                        .HasForeignKey("ShippingAddressID");
                });

            modelBuilder.Entity("TheBookCave.Data.EntityModels.UserEntityModel", b =>
                {
                    b.HasOne("TheBookCave.Models.AddressModel", "Address")
                        .WithMany()
                        .HasForeignKey("AddressID");
                });
#pragma warning restore 612, 618
        }
    }
}
