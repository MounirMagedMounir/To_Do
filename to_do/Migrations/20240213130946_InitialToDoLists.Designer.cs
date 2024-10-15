﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using to_do.Data;

#nullable disable

namespace to_do.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240213130946_InitialToDoLists")]
    partial class InitialToDoLists
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("to_do.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<DateTime>("Duo")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int?>("ToDoListId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ToDoListId");

                    b.ToTable("Items");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Item");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("to_do.Models.ToDoList", b =>
                {
                    b.HasBaseType("to_do.Models.Item");

                    b.Property<int?>("ToDoListsId")
                        .HasColumnType("int");

                    b.HasIndex("ToDoListsId");

                    b.HasDiscriminator().HasValue("ToDoList");
                });

            modelBuilder.Entity("to_do.Models.ToDoLists", b =>
                {
                    b.HasBaseType("to_do.Models.ToDoList");

                    b.HasDiscriminator().HasValue("ToDoLists");
                });

            modelBuilder.Entity("to_do.Models.Item", b =>
                {
                    b.HasOne("to_do.Models.ToDoList", null)
                        .WithMany("Items")
                        .HasForeignKey("ToDoListId");
                });

            modelBuilder.Entity("to_do.Models.ToDoList", b =>
                {
                    b.HasOne("to_do.Models.ToDoLists", null)
                        .WithMany("ToDoList")
                        .HasForeignKey("ToDoListsId");
                });

            modelBuilder.Entity("to_do.Models.ToDoList", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("to_do.Models.ToDoLists", b =>
                {
                    b.Navigation("ToDoList");
                });
#pragma warning restore 612, 618
        }
    }
}
