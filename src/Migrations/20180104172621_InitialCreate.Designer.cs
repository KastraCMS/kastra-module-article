﻿// <auto-generated />
using Kastra.Module.Article.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Kastra.Module.Article.Migrations
{
    [DbContext(typeof(ArticleContext))]
    [Migration("20180104172621_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Kastra.Module.Article.DAL.KastraArticles", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ArticleID");

                    b.Property<string>("ArticleContent");

                    b.Property<int>("ArticleOrder");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("CreatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(1000);

                    b.Property<int>("ModuleId")
                        .HasColumnName("ModuleID");

                    b.Property<string>("Title")
                        .HasMaxLength(250);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("UpdatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("UserID")
                        .HasMaxLength(450);

                    b.HasKey("ArticleId")
                        .HasName("PK_Kastra_Articles");

                    b.ToTable("Kastra_Articles");
                });
#pragma warning restore 612, 618
        }
    }
}
