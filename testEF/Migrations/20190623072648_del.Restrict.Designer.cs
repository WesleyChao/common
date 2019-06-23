﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using testEF.Models;

namespace testEF.Migrations
{
    [DbContext(typeof(efCoreMVCContext))]
    [Migration("20190623072648_del.Restrict")]
    partial class delRestrict
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("testEF.Models.Blogs", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Url");

                    b.HasKey("BlogId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("testEF.Models.ClassRoom", b =>
                {
                    b.Property<string>("RoomID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RoomName");

                    b.Property<int?>("StudentNo");

                    b.HasKey("RoomID");

                    b.HasIndex("StudentNo");

                    b.ToTable("ClassRoom");
                });

            modelBuilder.Entity("testEF.Models.Course", b =>
                {
                    b.Property<string>("CourseID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CourseName");

                    b.Property<string>("CourseTypeID");

                    b.HasKey("CourseID");

                    b.HasIndex("CourseTypeID");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("testEF.Models.CourseInstance", b =>
                {
                    b.Property<string>("InstanceID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CourseID");

                    b.Property<string>("InstanceName");

                    b.HasKey("InstanceID");

                    b.HasIndex("CourseID");

                    b.ToTable("CourseInstance");
                });

            modelBuilder.Entity("testEF.Models.CourseType", b =>
                {
                    b.Property<string>("CourseTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CourseTypeName");

                    b.HasKey("CourseTypeID");

                    b.ToTable("CourseTypes");
                });

            modelBuilder.Entity("testEF.Models.Posts", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlogId");

                    b.Property<string>("Content");

                    b.Property<string>("Title");

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("testEF.Models.Student", b =>
                {
                    b.Property<int>("No")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("Name");

                    b.HasKey("No");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("testEF.Models.ClassRoom", b =>
                {
                    b.HasOne("testEF.Models.Student")
                        .WithMany("ClassRoom")
                        .HasForeignKey("StudentNo");
                });

            modelBuilder.Entity("testEF.Models.Course", b =>
                {
                    b.HasOne("testEF.Models.CourseType", "CourseType")
                        .WithMany("Course")
                        .HasForeignKey("CourseTypeID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("testEF.Models.CourseInstance", b =>
                {
                    b.HasOne("testEF.Models.Course", "Course")
                        .WithMany("CourseInstance")
                        .HasForeignKey("CourseID");
                });

            modelBuilder.Entity("testEF.Models.Posts", b =>
                {
                    b.HasOne("testEF.Models.Blogs", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}