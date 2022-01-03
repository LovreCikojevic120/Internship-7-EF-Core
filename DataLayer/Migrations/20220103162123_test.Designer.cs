﻿// <auto-generated />
using System;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(StackInternshipDbContext))]
    [Migration("20220103162123_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataLayer.Entities.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.Property<string>("CommentContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CommentOwnerId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfDislikes")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfLikes")
                        .HasColumnType("int");

                    b.Property<int?>("ParentCommentId")
                        .HasColumnType("int");

                    b.Property<int?>("ResourceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeOfPosting")
                        .HasColumnType("datetime2");

                    b.HasKey("CommentId");

                    b.HasIndex("CommentOwnerId");

                    b.HasIndex("ResourceId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            CommentId = 3,
                            CommentContent = "Fritule su bezveze",
                            CommentOwnerId = 1,
                            NumberOfDislikes = 4,
                            NumberOfLikes = 4,
                            ResourceId = 1,
                            TimeOfPosting = new DateTime(2021, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("DataLayer.Entities.Models.Resource", b =>
                {
                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.Property<string>("NameTag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfDislikes")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfLikes")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfReplys")
                        .HasColumnType("int");

                    b.Property<string>("ResourceContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResourceOwnerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeOfPosting")
                        .HasColumnType("datetime2");

                    b.HasKey("ResourceId");

                    b.HasIndex("ResourceOwnerId");

                    b.ToTable("Resources");

                    b.HasData(
                        new
                        {
                            ResourceId = 1,
                            NameTag = "Dev",
                            NumberOfDislikes = 4,
                            NumberOfLikes = 4,
                            NumberOfReplys = 7,
                            ResourceContent = "Fritule su najbolje slatko",
                            ResourceOwnerId = 1,
                            TimeOfPosting = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ResourceId = 2,
                            NameTag = "Generalno",
                            NumberOfDislikes = 4,
                            NumberOfLikes = 4,
                            NumberOfReplys = 0,
                            ResourceContent = "Krokanti su najbolje slatko",
                            ResourceOwnerId = 1,
                            TimeOfPosting = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("DataLayer.Entities.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<bool>("IsTrusted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RepPoints")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            IsTrusted = true,
                            Password = "12345",
                            RepPoints = 1,
                            Role = "Admin",
                            UserName = "Ivan Bakotin"
                        });
                });

            modelBuilder.Entity("DataLayer.Entities.Models.UserComment", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCommented")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVoted")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "CommentId");

                    b.HasIndex("CommentId");

                    b.ToTable("UserComments");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            CommentId = 3,
                            IsCommented = false,
                            IsVoted = false
                        });
                });

            modelBuilder.Entity("DataLayer.Entities.Models.UserResource", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCommented")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVoted")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "ResourceId");

                    b.HasIndex("ResourceId");

                    b.ToTable("UserResources");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            ResourceId = 1,
                            IsCommented = false,
                            IsVoted = false
                        });
                });

            modelBuilder.Entity("DataLayer.Entities.Models.Comment", b =>
                {
                    b.HasOne("DataLayer.Entities.Models.User", "CommentOwner")
                        .WithMany("Comments")
                        .HasForeignKey("CommentOwnerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataLayer.Entities.Models.Resource", "Resource")
                        .WithMany("Comments")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("CommentOwner");

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("DataLayer.Entities.Models.Resource", b =>
                {
                    b.HasOne("DataLayer.Entities.Models.User", "ResourceOwner")
                        .WithMany("Resources")
                        .HasForeignKey("ResourceOwnerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ResourceOwner");
                });

            modelBuilder.Entity("DataLayer.Entities.Models.UserComment", b =>
                {
                    b.HasOne("DataLayer.Entities.Models.Comment", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataLayer.Entities.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Entities.Models.UserResource", b =>
                {
                    b.HasOne("DataLayer.Entities.Models.Resource", "Resource")
                        .WithMany()
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataLayer.Entities.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Resource");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Entities.Models.Resource", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("DataLayer.Entities.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Resources");
                });
#pragma warning restore 612, 618
        }
    }
}