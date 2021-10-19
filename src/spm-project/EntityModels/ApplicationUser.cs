using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{

    public class ApplicationUser:IdentityUser
    {
    
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationTimestamp { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateTimestamp { get; set; }

        public LMSUser LMSUser { get; set; }


        [Key, ForeignKey("LMSUser")]
        public int? LMSUserId { get; set; }
    }




}
