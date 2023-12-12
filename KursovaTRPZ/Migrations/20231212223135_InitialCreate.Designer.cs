﻿// <auto-generated />
using System;
using KursovaTRPZ.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KursovaTRPZ.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20231212223135_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventLog", b =>
                {
                    b.Property<int>("Event_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Event_ID"));

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EventTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("Sensor_ID")
                        .HasColumnType("int");

                    b.HasKey("Event_ID");

                    b.HasIndex("Id");

                    b.HasIndex("Sensor_ID");

                    b.ToTable("EventLogs");
                });

            modelBuilder.Entity("KursovaTRPZ.Models.Auth", b =>
                {
                    b.Property<int>("Auth_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Auth_Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Auth_Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Auth");
                });

            modelBuilder.Entity("KursovaTRPZ.Models.Sensor", b =>
                {
                    b.Property<int>("Sensor_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Sensor_Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<int>("EngineerUserId")
                        .HasColumnType("int");

                    b.Property<string>("Sensor_Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Sensor_Id");

                    b.HasIndex("EngineerUserId");

                    b.ToTable("Sensors");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Sensor");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("KursovaTRPZ.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("KursovaTRPZ.Models.MotionSensor", b =>
                {
                    b.HasBaseType("KursovaTRPZ.Models.Sensor");

                    b.Property<bool>("MotionSensor_Value")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("MotionSensor");
                });

            modelBuilder.Entity("KursovaTRPZ.Models.RadiationSensor", b =>
                {
                    b.HasBaseType("KursovaTRPZ.Models.Sensor");

                    b.Property<float>("Radiation_Value")
                        .HasColumnType("real");

                    b.HasDiscriminator().HasValue("RadiationSensor");
                });

            modelBuilder.Entity("KursovaTRPZ.Models.SoilSensor", b =>
                {
                    b.HasBaseType("KursovaTRPZ.Models.Sensor");

                    b.Property<float>("Humidity_Value")
                        .HasColumnType("real");

                    b.Property<float>("Ph_Value")
                        .HasColumnType("real");

                    b.HasDiscriminator().HasValue("SoilSensor");
                });

            modelBuilder.Entity("KursovaTRPZ.Models.WaterSensor", b =>
                {
                    b.HasBaseType("KursovaTRPZ.Models.Sensor");

                    b.Property<float>("Ph_Value")
                        .HasColumnType("real");

                    b.ToTable("Sensors", t =>
                        {
                            t.Property("Ph_Value")
                                .HasColumnName("WaterSensor_Ph_Value");
                        });

                    b.HasDiscriminator().HasValue("WaterSensor");
                });

            modelBuilder.Entity("KursovaTRPZ.Models.Administrator", b =>
                {
                    b.HasBaseType("KursovaTRPZ.Models.User");

                    b.HasDiscriminator().HasValue("Administrator");
                });

            modelBuilder.Entity("KursovaTRPZ.Models.Engineer", b =>
                {
                    b.HasBaseType("KursovaTRPZ.Models.User");

                    b.HasDiscriminator().HasValue("Engineer");
                });

            modelBuilder.Entity("EventLog", b =>
                {
                    b.HasOne("KursovaTRPZ.Models.Administrator", "AdminNavigation")
                        .WithMany("EventLogs")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KursovaTRPZ.Models.Sensor", "Sensor")
                        .WithMany()
                        .HasForeignKey("Sensor_ID");

                    b.Navigation("AdminNavigation");

                    b.Navigation("Sensor");
                });

            modelBuilder.Entity("KursovaTRPZ.Models.Auth", b =>
                {
                    b.HasOne("KursovaTRPZ.Models.User", "User")
                        .WithOne("Auth")
                        .HasForeignKey("KursovaTRPZ.Models.Auth", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("KursovaTRPZ.Models.Sensor", b =>
                {
                    b.HasOne("KursovaTRPZ.Models.Engineer", "Engineer")
                        .WithMany("Sensors")
                        .HasForeignKey("EngineerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Engineer");
                });

            modelBuilder.Entity("KursovaTRPZ.Models.User", b =>
                {
                    b.Navigation("Auth")
                        .IsRequired();
                });

            modelBuilder.Entity("KursovaTRPZ.Models.Administrator", b =>
                {
                    b.Navigation("EventLogs");
                });

            modelBuilder.Entity("KursovaTRPZ.Models.Engineer", b =>
                {
                    b.Navigation("Sensors");
                });
#pragma warning restore 612, 618
        }
    }
}
