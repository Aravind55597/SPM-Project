using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{

    public class ApplicationUser:IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Departments Department { get; set; }

        public DateTime DOB { get; set; }

        public DateTime CreationTimestamp { get; set; }

        public DateTime UpdateTimestamp { get; set; }

        public LMSUser LMSUser { get; set; }





        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    userIdentity.AddClaim(new Claim("FullName", this.Ful));
        //    return userIdentity;
        //}

    }

    public enum Departments
    {
        Human_Resource,
        Engineering
    }


}
