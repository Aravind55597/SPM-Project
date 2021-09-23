using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPM_Project.EntityModels;
using SPM_Project.PracticeDatatableModelAndController;

namespace SPM_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //for practice (Remove once everyone is ok with datatables)
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Chapter> Chapter { get; set; }

        public DbSet<ClassEnrollmentRecord> ClassEnrollmentRecord { get; set; }
        public DbSet<Course> Course { get; set; }

        public DbSet<CourseClass> CourseClass { get; set; }
        public DbSet<LMSUser>LMSUser { get; set; }
        public DbSet<ProgressTracker> ProgressTracker { get; set; }
        public DbSet<Quiz> Quiz { get; set; }

        public DbSet<QuizQuestion> QuizQuestion { get; set; }


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

            //QUIZ ------------------------------------------------------------------------------------------------------------------------------------------

            //quiz question and course class have a 1 to 1 relationship , hence need to indicate which is the principle entity 
            //https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#one-to-one





            //seed users and roles 

            //lms users 1 to 14 are senior engineers!!!!!!!!!!!!!!!!!
            //lms 15 onwards are learners !!!!!!!!!!!!!!!!

        }
    }
}