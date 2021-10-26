using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPM_ProjectTests.Extensions
{
    public static class AuthorizeAttributeExtensions
    {

        public static  bool CheckRoleAccess (this AuthorizeAttribute attribute , string role)
        {
            var result = attribute.Roles.Contains(role);


            return result; 
        }




    }
}
