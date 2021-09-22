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

        public SeedUsers(ApplicationDbContext context)
        {
            _context = context;
        }

        public async void SeedMainHRUser()
        {
            //LMS user 100
            var user1 = new ApplicationUser
            {
                UserName = "admin@email.com",
                NormalizedUserName = "ADMIN@EMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
                Department = Departments.Human_Resource,
                LMSUser=_context.LMSUser.FirstOrDefault(l=>l.Id==100),
                LockoutEnabled = false,
                Name = "JoanAdministrator"
               
            };

            var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "ADMINISTRATOR"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "ADMINISTRATOR", NormalizedName = "ADMINISTRATOR" });
            }


            if (!_context.Users.Any(u => u.UserName == user1.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user1, "password");
                user1.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(_context);
                await userStore.CreateAsync(user1);
                await userStore.AddToRoleAsync(user1, "ADMINISTRATOR");
            }


            //lms user 1
            var user2 = new ApplicationUser
            {
                UserName = "trainer@email.com",
                NormalizedUserName = "TRAINER@EMAIL.COM",
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                Department = Departments.Engineering,
                LMSUser = _context.LMSUser.FirstOrDefault(l => l.Id == 1),
                Name = "JohnTrainer"
            };

            //var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "TRAINER"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "TRAINER", NormalizedName = "TRAINER" });
            }

            if (!_context.Users.Any(u => u.UserName == user2.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user2, "password");
                user2.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(_context);
                await userStore.CreateAsync(user2);
                await userStore.AddToRoleAsync(user2, "TRAINER");
            }


            //lms user 15
            var user = new ApplicationUser
            {
                UserName = "learner@email.com",
                NormalizedUserName = "LEARNER@EMAIL.COM",
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                Department = Departments.Engineering,
                LMSUser = _context.LMSUser.FirstOrDefault(l => l.Id == 15),
                Name = "JimLearner"
            };

            //var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "LEARNER"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "LEARNER", NormalizedName = "LEARNER" });
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "password");
                user.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(_context);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "LEARNER");
            }




            await _context.SaveChangesAsync();


        }

        public async void SeedMainTrainerUser()
        {
            //lms user 1
            var user = new ApplicationUser
            {
                UserName = "trainer@email.com",
                NormalizedUserName = "TRAINER@EMAIL.COM",
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                Department = Departments.Engineering,
                LMSUser = _context.LMSUser.FirstOrDefault(l => l.Id == 1),
                Name = "JohnTrainer"
            };

            var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "TRAINER"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "TRAINER", NormalizedName = "TRAINER" });
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "password");
                user.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(_context);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "TRAINER");
            }

            await _context.SaveChangesAsync();


        }

        public async void SeedMainLearnerUser()
        {
            //lms user 15
            var user = new ApplicationUser
            {
                UserName = "learner@email.com",
                NormalizedUserName = "LEARNER@EMAIL.COM",
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                Department = Departments.Engineering,
                LMSUser = _context.LMSUser.FirstOrDefault(l => l.Id == 15),
                Name="JimLearner"
            };

            var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "LEARNER"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "LEARNER", NormalizedName = "LEARNER" });
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "password");
                user.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(_context);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "LEARNER");
            }

            await _context.SaveChangesAsync();


        }


    }



}
