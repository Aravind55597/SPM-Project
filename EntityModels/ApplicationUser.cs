using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{

    public class ApplicationUser:IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public DateTime CreationTimestamp { get; set; }

        public DateTime UpdateTimestamp { get; set; }


    }
    

}
