﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using weGOPAY.weGOPAY.Data;

namespace weGOPAY.Migrations
{
    [DbContext(typeof(weGOPAYDbContext))]
    [Migration("20190122233915_WalletNameChange")]
    partial class WalletNameChange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("weGOPAY.weGOPAY.Core.Models.Requests.TransactionRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("CreditRequestDate");

                    b.Property<decimal>("CurrentBalance");

                    b.Property<int>("ReqCurrency");

                    b.Property<int>("RequestStatus");

                    b.Property<int>("ResCurrency");

                    b.Property<int>("TransactionType");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("TransactionRequest");
                });

            modelBuilder.Entity("weGOPAY.weGOPAY.Core.Models.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryOfOrigin");

                    b.Property<string>("CountryOfResidence");

                    b.Property<DateTime>("DateRegistered");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("Fullname");

                    b.Property<string>("Gender");

                    b.Property<bool>("IsUpdated");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Status");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("weGOPAY.weGOPAY.Core.Models.Wallets.Wallet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Currency");

                    b.Property<decimal>("EuroBalance");

                    b.Property<decimal>("NairaBalance");

                    b.Property<decimal>("PoundBalance");

                    b.Property<string>("Status");

                    b.Property<decimal>("USDBalance");

                    b.Property<string>("UserId");

                    b.Property<DateTime>("WalletCreationDate");

                    b.Property<decimal>("YenBalance");

                    b.HasKey("Id");

                    b.ToTable("Wallet");
                });
#pragma warning restore 612, 618
        }
    }
}
