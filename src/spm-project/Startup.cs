using CustomExceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SPM_Project.Data;
using SPM_Project.DTOs.RRModels;
using SPM_Project.EntityModels;
using SPM_Project.Extensions;
using SPM_Project.Repositories;
using SPM_Project.Repositories.Interfaces;
using SPM_Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace SPM_Project
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //https://www.benday.com/2017/12/20/ef-core-asp-net-core-read-connections-strings-from-environment-variables/

            //add db context 
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    //Configuration.GetConnectionString("DefaultConnection")
                    Environment.GetEnvironmentVariable("SPM_DB_STRING")
                    ));




            services.AddDatabaseDeveloperPageExceptionFilter();


            //add indentity with roles
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();



            //add controllers that allows for views & suppress auto 400 reponses when model biding is invalid 
            //services.AddControllersWithViews().ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });

            services.AddControllersWithViews();


            //AUTO 400 BAD REQUEST FOR MODEL STATE ERRORS
            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionContext =>
                    new BadRequestObjectResult(

                        Newtonsoft.Json.JsonConvert.SerializeObject(
                            new Response<object>((int)HttpStatusCode.BadRequest, actionContext.ModelState.AllErrors(),"Invalid Request")
                    )


                    );
            });

            //seed users
            //services.AddScoped<SeedUsers>();


            //unit of work 
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            //servicemanager
            //services.AddScoped<IServiceManager, ServiceManager>();

            //allow access to httpcontext 
            services.AddHttpContextAccessor();





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //db context is auto injected here from the DI container
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ApplicationDbContext dbContext)
        {


            //need to blcok the thread as we are making an async call in a sync method (bad practice but ok for now )
            //seedUsers.SeedTestUsers().GetAwaiter().GetResult(); 




            if (env.IsDevelopment())
            {


                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();


            //handles exceptions thrown and returns an error reponse based on the type of exception thrown 
            //    app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();


            //Add database seeding code here 
            //SeedDatabase.Initialize(dbContext); 



            //handles exceptions thrown and returns an error reponse based on the type of exception thrown 
            app.UseMiddleware<ErrorHandlerMiddleware>();


            app.UseEndpoints(endpoints =>
            {

                //Attribute routing
                endpoints.MapControllers();

                //MVC Routing 
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
