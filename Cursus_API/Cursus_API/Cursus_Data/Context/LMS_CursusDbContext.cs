
using Cursus_Data.Data.Configuration;
using Cursus_Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Context
{
    public class LMS_CursusDbContext : DbContext
    {
        //public LMS_CursusDbContext(DbContextOptions opts) : base(opts) { }
        public LMS_CursusDbContext(DbContextOptions<LMS_CursusDbContext> options)
        : base(options)
        {
        }

        #region DBSet
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseVersion> CourseVersions { get; set; }
        public DbSet<EnrollCourse> EnrollCourses { get; set; }
        public DbSet<CourseContent> CourseContents { get; set; }
        public DbSet<CourseVersionDetail> CourseVersionDetails { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<SystemTransaction> SystemTransactions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<UserEmail> UserEmails { get; set; }
        public DbSet<CourseVersionEmail> CourseVersionEmails { get; set; }
        public DbSet<CourseComment> CourseComments { get; set; }
        public DbSet<CourseRating> CourseRatings { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Payment> Payments { get; set; }
        //public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserBehavior> UserBehaviors { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<UserProcess> UserProcesses { get; set; }

        public DbSet<FinancialTransactions> FinancialTransactions { get; set; }


        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }



        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserDetailConfiguration());
            modelBuilder.ApplyConfiguration(new WalletConfiguration());
            modelBuilder.ApplyConfiguration(new InstructorConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new CourseVersionConfiguration());
            modelBuilder.ApplyConfiguration(new CourseVersionDetailConfiguration());
            modelBuilder.ApplyConfiguration(new CourseContentConfiguration());
            modelBuilder.ApplyConfiguration(new EnrollCourseConfiguration());
            modelBuilder.ApplyConfiguration(new UserProcessConfiguration());
            modelBuilder.ApplyConfiguration(new EmailTemplateConfiguration());

            //1
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserDetail>().ToTable("UserDetail");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Wallet>().ToTable("Wallet");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<FinancialTransactions>().ToTable("FinancialTransactions");

            //2
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Course>()
                .Property(c => c.CourseRating)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<CourseVersion>().ToTable("CourseVersion");
            modelBuilder.Entity<CourseVersion>()
                .Property(cv => cv.Version)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<EnrollCourse>()
                .ToTable("EnrollCourse")
                .HasKey(ec => ec.EnrollCourseId);

            modelBuilder.Entity<EnrollCourse>()
                .HasOne(ec => ec.User)
                .WithMany(u => u.EnrollCourses)
                .HasForeignKey(ec => ec.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict if needed

            modelBuilder.Entity<EnrollCourse>()
                .HasOne(ec => ec.Course)
                .WithMany(c => c.EnrollCourses)
                .HasForeignKey(ec => ec.CourseId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict if needed

            //UserEmail
            modelBuilder.Entity<UserEmail>().ToTable("UserEmail");
            modelBuilder.Entity<UserEmail>()
                .HasKey(ue => new { ue.UserID, ue.EmailTemplateId });

            modelBuilder.Entity<UserEmail>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.UserEmails)
                .HasForeignKey(ue => ue.UserID);

            modelBuilder.Entity<UserEmail>()
                .HasOne(ue => ue.EmailTemplate)
                .WithMany(c => c.UserEmails)
                .HasForeignKey(ue => ue.EmailTemplateId);

            //CourseVersionEmail
            modelBuilder.Entity<CourseVersionEmail>().ToTable("CourseVersionEmail");
            modelBuilder.Entity<CourseVersionEmail>()
                .HasKey(cve => new { cve.CourseVersionId, cve.EmailTemplateId });

            modelBuilder.Entity<CourseVersionEmail>()
                .HasOne(cve => cve.CourseVersion)
                .WithMany(u => u.CourseVersionEmails)
                .HasForeignKey(cve => cve.CourseVersionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseVersionEmail>()
                .HasOne(cve => cve.EmailTemplate)
                .WithMany(c => c.CourseVersionEmails)
                .HasForeignKey(cve => cve.EmailTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            //3
            modelBuilder.Entity<CourseContent>().ToTable("CourseContent");
            modelBuilder.Entity<Image>().ToTable("Image");

            modelBuilder.Entity<CourseVersionDetail>().ToTable("CourseVersionDetail");
            modelBuilder.Entity<CourseVersionDetail>()
                .Property(cvd => cvd.Rating)
                .HasColumnType("decimal(5,2)");

            //system
            modelBuilder.Entity<Bank>().ToTable("Bank");
            modelBuilder.Entity<SystemTransaction>().ToTable("SystemTransaction");
            modelBuilder.Entity<RefreshToken>().ToTable("RefreshToken");
            modelBuilder.Entity<Category>().ToTable("Category")
                .HasMany(c => c.Children)
                .WithOne(c => c.Parent)
                .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

            // CourseComment
            modelBuilder.Entity<CourseComment>().ToTable("CourseComment");
            modelBuilder.Entity<CourseComment>()
                .HasOne(cc => cc.User)
                .WithMany(u => u.CourseComments)
                .HasForeignKey(cc => cc.FromUserId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict if needed

            modelBuilder.Entity<CourseComment>()
                .HasOne(cc => cc.CourseVersion)
                .WithMany(cv => cv.CourseComments)
                .HasForeignKey(cc => cc.ToCourseVersionId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict if needed

            // CourseRating
            modelBuilder.Entity<CourseRating>().ToTable("CourseRating");
            modelBuilder.Entity<CourseRating>()
                .Property(cr => cr.Rating)
                .HasColumnType("decimal(5,2)");
            modelBuilder.Entity<CourseRating>()
                .HasOne(cr => cr.User)
                .WithMany(u => u.CourseRatings)
                .HasForeignKey(cr => cr.FromUserId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict if needed

            modelBuilder.Entity<CourseRating>()
                .HasOne(cr => cr.CourseVersion)
                .WithMany(cv => cv.CourseRatings)
                .HasForeignKey(cr => cr.ToCourseVersionId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict if needed



            // Bookmark
            modelBuilder.Entity<Bookmark>().ToTable("Bookmark");
            modelBuilder.Entity<Bookmark>()
                .HasKey(b => new { b.UserId, b.CourseId });

            modelBuilder.Entity<Bookmark>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookmarks)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bookmark>()
                .HasOne(b => b.Course)
                .WithMany(c => c.Bookmarks)
                .HasForeignKey(b => b.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            //// Payment
            //modelBuilder.Entity<Payment>().ToTable("Payment");
            //modelBuilder.Entity<Payment>()
            //    .HasOne(p => p.User)
            //    .WithMany(u => u.Payments)
            //    .HasForeignKey(p => p.UserId);

            //// Transaction
            //modelBuilder.Entity<Transaction>().ToTable("Transaction");
            //modelBuilder.Entity<Transaction>()
            //    .HasOne(t => t.User)
            //    .WithMany(u => u.Transactions)
            //    .HasForeignKey(t => t.UserId);

            // UserBehavior
            modelBuilder.Entity<UserBehavior>().ToTable("UserBehavior");
            modelBuilder.Entity<UserBehavior>()
                .HasKey(ub => ub.UserBehaviorId);

            modelBuilder.Entity<UserBehavior>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserBehaviors)
                .HasForeignKey(ub => ub.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserComment>().ToTable("UserComment");

            // FromUserId foreign key with cascade delete
            modelBuilder.Entity<UserComment>()
                .HasOne(uc => uc.FromUser)
                .WithMany(u => u.UserComments)
                .HasForeignKey(uc => uc.FromUserId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict to avoid multiple cascade paths

            // ToUserId foreign key with no cascade delete
            modelBuilder.Entity<UserComment>()
                .HasOne(uc => uc.ToUser)
                .WithMany()
                .HasForeignKey(uc => uc.ToUserId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict for consistency

            // UserProcess configuration
            modelBuilder.Entity<UserProcess>()
                .ToTable("UserProcess")
                .HasKey(up => up.UserProcessId);

            modelBuilder.Entity<UserProcess>()
                .HasOne(up => up.EnrollCourse)
                .WithMany(ec => ec.UserProcesses)
                .HasForeignKey(up => up.EnrollCourseId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict if needed

            modelBuilder.Entity<UserProcess>()
                .HasOne(up => up.CourseContent)
                .WithMany(cc => cc.UserProcesses)
                .HasForeignKey(up => up.CourseContentId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict if needed

            // OrderItem configuration
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Course)
                .WithMany(c => c.OrderItems)
                .HasForeignKey(oi => oi.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cart configuration
            modelBuilder.Entity<Cart>()
                .HasKey(c => c.CartId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading delete here

            /// CartItem configuration
            modelBuilder.Entity<CartItem>()
                .HasKey(ci => ci.CartItemId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading delete here

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Course)
                .WithMany()
                .HasForeignKey(ci => ci.CourseId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading delete here

            // Payment configuration
            modelBuilder.Entity<Payment>()
                .HasKey(p => p.PaymentId);

            //modelBuilder.Entity<Payment>()
            //    .HasOne(p => p.Order)
            //    .WithMany(o => o.Payments) // Assuming Order has a collection of Payments
            //    .HasForeignKey(p => p.OrderId);

            // Transaction configuration
            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.TrId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Payment)
                .WithOne(p => p.Transaction)
                .HasForeignKey<Transaction>(t => t.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany() // Assuming User does not have a collection of Transactions
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);






            //Add-Migration init -Context LMS_CursusDbContext -Project Cursus_Data -StartupProject Cursus_API -OutputDir Context/Migrations
            base.OnModelCreating(modelBuilder);
        }
    }
}
