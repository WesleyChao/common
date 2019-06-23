using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace testEF.Models
{
    public partial class efCoreMVCContext : DbContext
    {
        public efCoreMVCContext()
        {
        }

        public efCoreMVCContext(DbContextOptions<efCoreMVCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Blogs> Blogs { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<CourseType> CourseTypes { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CourseInstance> CourseInstance { get; set; }
        public virtual DbSet<ClassRoom> ClassRoom { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;User ID=sa;Password=123456;Initial Catalog=efCoreMVC;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<CourseType>(u =>
            {
                u.HasKey(e => e.CourseTypeID);
            });

            modelBuilder.Entity<Course>(u =>
            {
                /*hasone 会在生成数据库的时候,建立外键关系 */
                u.HasKey(e => e.CourseID);

                u.HasOne(e => e.CourseType)
                .WithMany(e => e.Course)
                .HasForeignKey(e => e.CourseTypeID).OnDelete(DeleteBehavior.Restrict);// 
            });

            modelBuilder.Entity<CourseInstance>(u =>
            {
                u.HasKey(e => e.InstanceID);
                u.HasOne(e => e.Course).WithMany(e => e.CourseInstance);
            });













            modelBuilder.Entity<Blogs>(entity =>
            {
                entity.HasKey(e => e.BlogId);
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.HasKey(e => e.PostId);

                entity.HasIndex(e => e.BlogId);

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.BlogId);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.No);
            });


            modelBuilder.Entity<ClassRoom>(u =>
            {
                u.HasKey(t => t.RoomID);
                //   u.HasMany(t => t.Student);
            });
        }
    }
}
