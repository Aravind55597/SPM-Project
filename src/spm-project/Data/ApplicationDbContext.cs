using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPM_Project.EntityModels;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SPM_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //for practice (Remove once everyone is ok with datatables)

        public DbSet<Chapter> Chapter { get; set; }

        public DbSet<ClassEnrollmentRecord> ClassEnrollmentRecord { get; set; }
        public DbSet<Course> Course { get; set; }

        public DbSet<CourseClass> CourseClass { get; set; }
        public DbSet<LMSUser> LMSUser { get; set; }
        public DbSet<ProgressTracker> ProgressTracker { get; set; }
        public DbSet<Quiz> Quiz { get; set; }

        public DbSet<QuizQuestion> QuizQuestion { get; set; }

        public DbSet<McqQuestion> McqQuestion { get; set; }

        public DbSet<TFQuestion> TFQuestion { get; set; }

        public override Task<int> SaveChangesAsync(
                bool acceptAllChangesOnSuccess,
                CancellationToken token = default)
        {
            foreach (var entity in ChangeTracker
                .Entries()
                .Where(x => x.Entity is IEntityWithId && x.State == EntityState.Modified)
                .Select(x => x.Entity)
                .Cast<IEntityWithId>())
            {
                entity.UpdateTimestamp = DateTime.Now;
            }

            foreach (var entity in ChangeTracker
                .Entries()
                .Where(x => x.Entity is IEntityWithId && x.State == EntityState.Added)
                .Select(x => x.Entity)
                .Cast<IEntityWithId>())
            {
                entity.CreationTimestamp = DateTime.Now;
            }


            return base.SaveChangesAsync(acceptAllChangesOnSuccess, token);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //run the onmodelcreating for for the base class for IndentityDBcontext so that indentity table will be created
            base.OnModelCreating(modelBuilder);
            //https://stackoverflow.com/questions/4525953/can-i-access-the-discriminator-value-in-tph-mapping-with-entity-framework-4-ctp5

            //QUIZQUESTION----------------------------------------------------------------------------------------------------------------------------------

            //create discrimninator for the questions clearly
            //https://docs.microsoft.com/de-de/ef/core/modeling/inheritance

            //explicity define that the column has a discriminator
            modelBuilder.Entity<QuizQuestion>()
              .HasDiscriminator(b => b.QuestionType);

            //Application user ------------------------------------------------------------------------------------------------------------------------------------------

            //quiz question and course class have a 1 to 1 relationship , hence need to indicate which is the principle entity
            //https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#one-to-one

            modelBuilder.Entity<ApplicationUser>().Property(m => m.LMSUserId).IsRequired(false);

            //https://stackoverflow.com/questions/47382680/entity-framework-core-property-setter-is-never-called-violation-of-encapsulat
            modelBuilder.Entity<QuizQuestion>()
             .Property(b => b.Marks)
             .HasField("_Marks")
             .UsePropertyAccessMode(PropertyAccessMode.Property);

            ////set up updeated & created
            //modelBuilder.Entity<Chapter>()
            //    .Property(s => s.CreationTimeStamp)
            //    .HasDefaultValueSql("GETDATE()");

            //modelBuilder.Entity<Chapter>()
            //        .Property(s => s.UpdateTimeStamp)
            //        .HasDefaultValueSql("GETDATE()");

            ////set up updeated & created
            //modelBuilder.Entity<Quiz>()
            //    .Property(s => s.CreationTimeStamp)
            //    .HasDefaultValueSql("GETDATE()");

            //modelBuilder.Entity<Quiz>()
            //        .Property(s => s.UpdateTimeStamp)
            //        .HasDefaultValueSql("GETDATE()");

            ////set up updeated & created
            //modelBuilder.Entity<Resource>()
            //    .Property(s => s.CreationTimestamp)
            //    .HasDefaultValueSql("GETDATE()");

            //modelBuilder.Entity<Resource>()
            //        .Property(s => s.UpdateTimestamp)
            //        .HasDefaultValueSql("GETDATE()");

            ////set up updeated & created
            //modelBuilder.Entity<ProgressTracker>()
            //    .Property(s => s.CreationTimestamp)
            //    .HasDefaultValueSql("GETDATE()");

            //modelBuilder.Entity<ProgressTracker>()
            //        .Property(s => s.UpdateTimestamp)
            //        .HasDefaultValueSql("GETDATE()");

            ////set up updeated & created
            //modelBuilder.Entity<UserAnswer>()
            //    .Property(s => s.CreationTimestamp)
            //    .HasDefaultValueSql("GETDATE()");

            //modelBuilder.Entity<UserAnswer>()
            //        .Property(s => s.UpdateTimestamp)
            //        .HasDefaultValueSql("GETDATE()");

            ////set up updeated & created
            //modelBuilder.Entity<Course>()
            //    .Property(s => s.CreationTimestamp)
            //    .HasDefaultValueSql("GETDATE()");

            //modelBuilder.Entity<Course>()
            //        .Property(s => s.UpdateTimestamp)
            //        .HasDefaultValueSql("GETDATE()");

            ////set up updeated & created
            //modelBuilder.Entity<CourseClass>()
            //    .Property(s => s.CreationTimeStamp)
            //    .HasDefaultValueSql("GETDATE()");

            //modelBuilder.Entity<CourseClass>()
            //        .Property(s => s.UpdateTimeStamp)
            //        .HasDefaultValueSql("GETDATE()");

            ////set up updeated & created
            //modelBuilder.Entity<ClassEnrollmentRecord>()
            //    .Property(s => s.CreationTimestamp)
            //    .HasDefaultValueSql("GETDATE()");

            //modelBuilder.Entity<ClassEnrollmentRecord>()
            //        .Property(s => s.UpdateTimestamp)
            //        .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<QuizQuestion>()
               .HasOne(b => b.Quiz)
               .WithMany(a => a.Questions)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseClass>()
                  .HasOne(a => a.GradedQuiz)
                  .WithOne(b => b.CourseClass)
                  .HasForeignKey<CourseClass>(b => b.GradedQuizId);

            modelBuilder.Entity<CourseClass>()
               .HasOne(b => b.GradedQuiz)
               .WithOne(a => a.CourseClass)
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}