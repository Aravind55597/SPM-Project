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

        //for practice
        public DbSet<Customer> Customer { get; set; }

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

            

  
        }
    }
}