using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SPM_ProjectTests.Extensions
{
    public static class ControllerTestExtensions
    {

        public static T WithIdentity<T>(this T controller, string role) where T : Controller
        {
            controller.EnsureHttpContext();

            string id = ""; 

            //based on role assgin AspNetDatabase id
            switch (role)
            {
                case "Learner":
                    // code block
                    id = "126f2e3c-810d-44b6-9375-2377c4011ba6"; 
                    break;
                case "Trainer":
                    // code block
                    id = "69d11941-ad79-40da-a63b-428e14a95c65";
                    break;
                case "Administrator":
                    // code block
                    id = "0da3267c-b388-4087-b811-87bccb272f87";
                    break;
            }



            var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                            {
                                new Claim(ClaimTypes.Role, role),
                                new Claim(ClaimTypes.NameIdentifier,id)
                                // other required and custom claims
                            }, "TestAuthentication"));

            controller.ControllerContext.HttpContext.User = principal;

            return controller;
        }



        //no roles , anyone can access 
        public static T WithAnonymousIdentity<T>(this T controller) where T : Controller
        {
            controller.EnsureHttpContext();

            var principal = new ClaimsPrincipal(new ClaimsIdentity());

            controller.ControllerContext.HttpContext.User = principal;

            return controller;
        }


        //create an accessible httpcontect to be accessed 
        private static T EnsureHttpContext<T>(this T controller) where T : Controller
        {
            if (controller.ControllerContext == null)
            {
                controller.ControllerContext = new ControllerContext();
            }

            if (controller.ControllerContext.HttpContext == null)
            {
                controller.ControllerContext.HttpContext = new DefaultHttpContext();
            }

            return controller;
        }



        //check authorise attribute 
        public static AuthorizeAttribute GetAuthoriseAttribute<T>(this T controller , string method) where T : Controller
        {
                return typeof(T).GetMethod(method).GetCustomAttributes(typeof(AuthorizeAttribute), true)
                    .Cast<AuthorizeAttribute>()
                        .FirstOrDefault();
        }















    }
}
