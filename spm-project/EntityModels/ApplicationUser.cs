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
        public string Name { get; set; }

        public Departments Department { get; set; }

        public DateTime DOB { get; set; }

        public DateTime CreationTimestamp { get; set; }

        public DateTime UpdateTimestamp { get; set; }

        public LMSUser LMSUser { get; set; }
    }

    public enum Departments
    {
        Human_Resource,
        Engineering
    }

    
}
