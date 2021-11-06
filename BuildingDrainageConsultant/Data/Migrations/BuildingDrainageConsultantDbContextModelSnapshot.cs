﻿// <auto-generated />
using System;
using BuildingDrainageConsultant.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingDrainageConsultant.Data.Migrations
{
    [DbContext(typeof(BuildingDrainageConsultantDbContext))]
    partial class BuildingDrainageConsultantDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AccessoryDrain", b =>
                {
                    b.Property<int>("AccessoriesId")
                        .HasColumnType("int");

                    b.Property<int>("DrainsId")
                        .HasColumnType("int");

                    b.HasKey("AccessoriesId", "DrainsId");

                    b.HasIndex("DrainsId");

                    b.ToTable("AccessoryDrain");
                });

            modelBuilder.Entity("AtticaDrainAtticaPart", b =>
                {
                    b.Property<int>("AtticaDrainsId")
                        .HasColumnType("int");

                    b.Property<int>("AtticaPartsId")
                        .HasColumnType("int");

                    b.HasKey("AtticaDrainsId", "AtticaPartsId");

                    b.HasIndex("AtticaPartsId");

                    b.ToTable("AtticaDrainAtticaPart");
                });

            modelBuilder.Entity("AtticaDrainUser", b =>
                {
                    b.Property<int>("AtticaDrainsId")
                        .HasColumnType("int");

                    b.Property<string>("UsersId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AtticaDrainsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("AtticaDrainUser");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.Accessory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Accessories");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(6000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.AtticaDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<int>("IsWalkable")
                        .HasColumnType("int");

                    b.Property<int>("RoofType")
                        .HasColumnType("int");

                    b.Property<int?>("ScreedWaterproofing")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("AtticaDetails");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.AtticaDrain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AtticaDetailId")
                        .HasColumnType("int");

                    b.Property<int>("ConcreteWaterproofing")
                        .HasColumnType("int");

                    b.Property<int>("Diameter")
                        .HasColumnType("int");

                    b.Property<int>("DrainageArea100mm")
                        .HasColumnType("int");

                    b.Property<int>("DrainageArea35mm")
                        .HasColumnType("int");

                    b.Property<double>("FlowRate100mm")
                        .HasColumnType("float");

                    b.Property<double>("FlowRate35mm")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ScreedWaterproofing")
                        .HasColumnType("int");

                    b.Property<int>("VisiblePart")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AtticaDetailId");

                    b.ToTable("AtticaDrains");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.AtticaPart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("AtticaParts");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.Drain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Depth")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Diameter")
                        .HasColumnType("int");

                    b.Property<int>("Direction")
                        .HasColumnType("int");

                    b.Property<int>("DrainageArea")
                        .HasColumnType("int");

                    b.Property<int>("FlapSeal")
                        .HasColumnType("int");

                    b.Property<double>("FlowRate")
                        .HasColumnType("float");

                    b.Property<int>("Heating")
                        .HasColumnType("int");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<int>("LoadClass")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("Renovation")
                        .HasColumnType("int");

                    b.Property<int>("VisiblePart")
                        .HasColumnType("int");

                    b.Property<int>("Waterproofing")
                        .HasColumnType("int");

                    b.Property<int?>("WaterproofingKitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("WaterproofingKitId");

                    b.ToTable("Drains");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.Extension", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Extensions");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.ImageHL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ImageCategory")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Path")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.Merchant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Merchants");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.SafeDrain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Depth")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Diameter")
                        .HasColumnType("int");

                    b.Property<int>("Direction")
                        .HasColumnType("int");

                    b.Property<int>("DrainageArea3mVertical")
                        .HasColumnType("int");

                    b.Property<int>("DrainageAreaFree")
                        .HasColumnType("int");

                    b.Property<double>("FlowRate3mVertical")
                        .HasColumnType("float");

                    b.Property<double>("FlowRateFree")
                        .HasColumnType("float");

                    b.Property<int>("Heating")
                        .HasColumnType("int");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("Waterproofing")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("SafeDrains");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.WaterproofingKit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("WaterproofingKits");
                });

            modelBuilder.Entity("DrainExtension", b =>
                {
                    b.Property<int>("DrainsId")
                        .HasColumnType("int");

                    b.Property<int>("ExtensionsId")
                        .HasColumnType("int");

                    b.HasKey("DrainsId", "ExtensionsId");

                    b.HasIndex("ExtensionsId");

                    b.ToTable("DrainExtension");
                });

            modelBuilder.Entity("DrainUser", b =>
                {
                    b.Property<int>("DrainsId")
                        .HasColumnType("int");

                    b.Property<string>("UsersId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DrainsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("DrainUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SafeDrainUser", b =>
                {
                    b.Property<int>("SafeDrainsId")
                        .HasColumnType("int");

                    b.Property<string>("UsersId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SafeDrainsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("SafeDrainUser");
                });

            modelBuilder.Entity("AccessoryDrain", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.Accessory", null)
                        .WithMany()
                        .HasForeignKey("AccessoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BuildingDrainageConsultant.Data.Models.Drain", null)
                        .WithMany()
                        .HasForeignKey("DrainsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AtticaDrainAtticaPart", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.AtticaDrain", null)
                        .WithMany()
                        .HasForeignKey("AtticaDrainsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BuildingDrainageConsultant.Data.Models.AtticaPart", null)
                        .WithMany()
                        .HasForeignKey("AtticaPartsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AtticaDrainUser", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.AtticaDrain", null)
                        .WithMany()
                        .HasForeignKey("AtticaDrainsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BuildingDrainageConsultant.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.Accessory", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.ImageHL", "Image")
                        .WithMany("Accessories")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Image");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.Article", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.ImageHL", "Image")
                        .WithMany("Articles")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Image");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.AtticaDetail", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.ImageHL", "Image")
                        .WithMany("AtticaDetails")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Image");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.AtticaDrain", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.AtticaDetail", "AtticaDetail")
                        .WithMany("AtticaDrains")
                        .HasForeignKey("AtticaDetailId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("AtticaDetail");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.AtticaPart", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.ImageHL", "Image")
                        .WithMany("AtticaParts")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Image");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.Drain", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.ImageHL", "Image")
                        .WithMany("Drains")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("BuildingDrainageConsultant.Data.Models.WaterproofingKit", "WaterproofingKit")
                        .WithMany("Drains")
                        .HasForeignKey("WaterproofingKitId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Image");

                    b.Navigation("WaterproofingKit");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.Extension", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.ImageHL", "Image")
                        .WithMany("Extensions")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Image");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.SafeDrain", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.ImageHL", "Image")
                        .WithMany("SafeDrains")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Image");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.WaterproofingKit", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.ImageHL", "Image")
                        .WithMany("WaterproofingKits")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Image");
                });

            modelBuilder.Entity("DrainExtension", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.Drain", null)
                        .WithMany()
                        .HasForeignKey("DrainsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BuildingDrainageConsultant.Data.Models.Extension", null)
                        .WithMany()
                        .HasForeignKey("ExtensionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DrainUser", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.Drain", null)
                        .WithMany()
                        .HasForeignKey("DrainsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BuildingDrainageConsultant.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BuildingDrainageConsultant.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SafeDrainUser", b =>
                {
                    b.HasOne("BuildingDrainageConsultant.Data.Models.SafeDrain", null)
                        .WithMany()
                        .HasForeignKey("SafeDrainsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BuildingDrainageConsultant.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.AtticaDetail", b =>
                {
                    b.Navigation("AtticaDrains");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.ImageHL", b =>
                {
                    b.Navigation("Accessories");

                    b.Navigation("Articles");

                    b.Navigation("AtticaDetails");

                    b.Navigation("AtticaParts");

                    b.Navigation("Drains");

                    b.Navigation("Extensions");

                    b.Navigation("SafeDrains");

                    b.Navigation("WaterproofingKits");
                });

            modelBuilder.Entity("BuildingDrainageConsultant.Data.Models.WaterproofingKit", b =>
                {
                    b.Navigation("Drains");
                });
#pragma warning restore 612, 618
        }
    }
}
