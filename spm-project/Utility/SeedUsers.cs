using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SPM_Project.Data;
using SPM_Project.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Utility
{
    public class SeedUsers
    {


        //lms users 1 to 14 are senior engineers!!!!!!!!!!!!!!!!!
        //lms 15 onwards are learners !!!!!!!!!!!!!!!!

        private ApplicationDbContext _context;

   

        private UserManager<ApplicationUser> _userManager;

        public SeedUsers(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;

            _userManager = userManager;
        }

        public async Task SeedTestUsers()
        {

            // LMSuser->  1 Trainer trainer@email.com password    other id-> 2 to 14
            // LMSuser -> 15 Learner learner@email.com password   other id -> 16 to 89
            // LMSuser -> 100 Aministrator admin@email.com password other id -> 90 to 100


            //for (int i = 90; i < 100; i++)
            //{
            //    var email = Faker.Internet.Email();

            //    var user = new ApplicationUser
            //    {
            //        UserName = email,
            //        NormalizedUserName = email.ToUpper(),
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //        Department = Departments.Human_Resource,
            //        LMSUser = _context.LMSUser.FirstOrDefault(l => l.Id ==i),
            //        LockoutEnabled = false,
            //        Name = Faker.Name.First()+"Administrator",
            //        EmailConfirmed = true,
            //    };

            //    var password = "password";
            //    var result = await _userManager.CreateAsync(user, password);

            //    if (result.Succeeded)
            //    {
            //        await _userManager.AddToRoleAsync(user, "ADMINISTRATOR");
            //    }


            //}



            for (int i = 4; i < 15; i++)
            {
                var email = Faker.Internet.Email();

                var user = new ApplicationUser
                {
                    UserName = email,
                    NormalizedUserName = email.ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Department = Departments.Engineering,
                    LMSUser = _context.LMSUser.FirstOrDefault(l => l.Id == i),
                    LockoutEnabled = false,
                    Name = Faker.Name.First() + "Trainer",
                    EmailConfirmed = true,
                };

                var password = "Password@123";
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "TRAINER");
                }


            }


            for (int i = 16; i < 90; i++)
            {
                var email = Faker.Internet.Email();

                var user = new ApplicationUser
                {
                    UserName = email,
                    NormalizedUserName = email.ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Department = Departments.Engineering,
                    LMSUser = _context.LMSUser.FirstOrDefault(l => l.Id == i),
                    LockoutEnabled = false,
                    Name = Faker.Name.First() + "Learner",
                    EmailConfirmed = true,
                };

                var password = "Password@123";
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "LEARNER");
                }
            }


            //LMS user 100
            //var user1 = new ApplicationUser
            //{
            //    UserName = "admin@email.com",
            //    NormalizedUserName = "ADMIN@EMAIL.COM",
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    Department = Departments.Human_Resource,
            //    LMSUser=_context.LMSUser.FirstOrDefault(l=>l.Id==100),
            //    LockoutEnabled = false,
            //    Name = "JoanAdministrator",
            //    EmailConfirmed = true,

            //};

            //var roleStore = new RoleStore<IdentityRole>(_context);

            //if (!_context.Roles.Any(r => r.Name == "ADMINISTRATOR"))
            //{
            //    await roleStore.CreateAsync(new IdentityRole { Name = "ADMINISTRATOR", NormalizedName = "ADMINISTRATOR" });
            //}


            //if (!_context.Users.Any(u => u.UserName == user1.UserName))
            //{
            //    var password = new PasswordHasher<ApplicationUser>();
            //    var hashed = password.HashPassword(user1, "password");
            //    user1.PasswordHash = hashed;
            //    var userStore = new UserStore<ApplicationUser>(_context);
            //    await userStore.CreateAsync(user1);
            //    await userStore.AddToRoleAsync(user1, "ADMINISTRATOR");
            //}



            //lms user 1
            //var user2 = new ApplicationUser
            //{
            //    UserName = "trainer@email.com",
            //    NormalizedUserName = "TRAINER@EMAIL.COM",
            //    LockoutEnabled = false,
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    Department = Departments.Engineering,
            //    LMSUser = _context.LMSUser.FirstOrDefault(l => l.Id == 1),
            //    Name = "JohnTrainer",
            //    EmailConfirmed = true,
            //};

            ////var roleStore = new RoleStore<IdentityRole>(_context);

            //if (!_context.Roles.Any(r => r.Name == "TRAINER"))
            //{
            //    await roleStore.CreateAsync(new IdentityRole { Name = "TRAINER", NormalizedName = "TRAINER" });
            //}

            //if (!_context.Users.Any(u => u.UserName == user2.UserName))
            //{
            //    var password = new PasswordHasher<ApplicationUser>();
            //    var hashed = password.HashPassword(user2, "password");
            //    user2.PasswordHash = hashed;
            //    var userStore = new UserStore<ApplicationUser>(_context);
            //    await userStore.CreateAsync(user2);
            //    await userStore.AddToRoleAsync(user2, "TRAINER");
            //}


            //lms user 15
            //var user = new ApplicationUser
            //{
            //    UserName = "learner@email.com",
            //    NormalizedUserName = "LEARNER@EMAIL.COM",
            //    LockoutEnabled = false,
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    Department = Departments.Engineering,
            //    LMSUser = _context.LMSUser.FirstOrDefault(l => l.Id == 15),
            //    Name = "JimLearner",
            //    EmailConfirmed = true,
            //};

            ////var roleStore = new RoleStore<IdentityRole>(_context);

            //if (!_context.Roles.Any(r => r.Name == "LEARNER"))
            //{
            //    await roleStore.CreateAsync(new IdentityRole { Name = "LEARNER", NormalizedName = "LEARNER" });
            //}

            //if (!_context.Users.Any(u => u.UserName == user.UserName))
            //{
            //    var password = new PasswordHasher<ApplicationUser>();
            //    var hashed = password.HashPassword(user, "password");
            //    user.PasswordHash = hashed;
            //    var userStore = new UserStore<ApplicationUser>(_context);
            //    await userStore.CreateAsync(user);
            //    await userStore.AddToRoleAsync(user, "LEARNER");
            //}




            //    await _context.SaveChangesAsync();


            //}

            //public async void SeedMainTrainerUser()
            //{
            //    //lms user 1
            //    var user = new ApplicationUser
            //    {
            //        UserName = "trainer@email.com",
            //        NormalizedUserName = "TRAINER@EMAIL.COM",
            //        LockoutEnabled = false,
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //        Department = Departments.Engineering,
            //        LMSUser = _context.LMSUser.FirstOrDefault(l => l.Id == 1),
            //        Name = "JohnTrainer"
            //    };

            //    var roleStore = new RoleStore<IdentityRole>(_context);

            //    if (!_context.Roles.Any(r => r.Name == "TRAINER"))
            //    {
            //        await roleStore.CreateAsync(new IdentityRole { Name = "TRAINER", NormalizedName = "TRAINER" });
            //    }

            //    if (!_context.Users.Any(u => u.UserName == user.UserName))
            //    {
            //        var password = new PasswordHasher<ApplicationUser>();
            //        var hashed = password.HashPassword(user, "password");
            //        user.PasswordHash = hashed;
            //        var userStore = new UserStore<ApplicationUser>(_context);
            //        await userStore.CreateAsync(user);
            //        await userStore.AddToRoleAsync(user, "TRAINER");
            //    }

            //    await _context.SaveChangesAsync();


            //}

            //public async void SeedMainLearnerUser()
            //{
            //    //lms user 15
            //    var user = new ApplicationUser
            //    {
            //        UserName = "learner@email.com",
            //        NormalizedUserName = "LEARNER@EMAIL.COM",
            //        LockoutEnabled = false,
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //        Department = Departments.Engineering,
            //        LMSUser = _context.LMSUser.FirstOrDefault(l => l.Id == 15),
            //        Name="JimLearner"
            //    };

            //    var roleStore = new RoleStore<IdentityRole>(_context);

            //    if (!_context.Roles.Any(r => r.Name == "LEARNER"))
            //    {
            //        await roleStore.CreateAsync(new IdentityRole { Name = "LEARNER", NormalizedName = "LEARNER" });
            //    }

            //    if (!_context.Users.Any(u => u.UserName == user.UserName))
            //    {
            //        var password = new PasswordHasher<ApplicationUser>();
            //        var hashed = password.HashPassword(user, "password");
            //        user.PasswordHash = hashed;
            //        var userStore = new UserStore<ApplicationUser>(_context);
            //        await userStore.CreateAsync(user);
            //        await userStore.AddToRoleAsync(user, "LEARNER");
            //    }

            //    await _context.SaveChangesAsync();


            //}


        }



    }
}
