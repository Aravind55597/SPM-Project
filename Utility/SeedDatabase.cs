using SPM_Project.Data;
using SPM_Project.PracticeDatatableModelAndController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Utility
{
    public class SeedDatabase
    {

        public static void Initialize(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            if (!dbContext.Users.Any())
            {
                // Write you necessary to code here to insert the User to database and save the the information to file.
                Customer customer; 
                for (int i = 0; i < 1500; i++)
                {

                    customer = new Customer()
                    {
                        FirstName = Faker.Name.First(),
                        LastName = Faker.Name.Last(),
                        Contact = Faker.Phone.Number(),
                        Email=Faker.Internet.Email(),
                        DateOfBirth=Faker.Identification.DateOfBirth()

                    };

                    dbContext.Add(customer);
                }
                dbContext.SaveChanges(); 
            }

        }





    }
}
