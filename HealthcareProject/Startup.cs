using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using HealthcareProject.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HealthcareProject.Models;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using Hangfire;
using Hangfire.MemoryStorage;
using Rotativa.AspNetCore;
using Stripe;
using Microsoft.AspNetCore.Identity.UI.Services;
using HealthcareProject.Services;

namespace HealthcareProject
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
            // needed to load configuration from appsettings.json
            services.AddOptions();

            // needed to store rate limit counters and ip rules
            services.AddMemoryCache();

            //load general configuration from appsettings.json
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));

            //load ip rules from appsettings.json
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));

            // inject counter and rules stores
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();


            //Hangfire for recurring jobs
            services.AddHangfire(config =>
              config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
              .UseSimpleAssemblyNameTypeSerializer()
              .UseDefaultTypeSerializer()
              .UseMemoryStorage());

            services.AddHangfireServer();




            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<healthcarev1Context>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DataConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                 .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            // requires
            // using Microsoft.AspNetCore.Identity.UI.Services;

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.Configure<DataProtectionTokenProviderOptions>(o =>
       o.TokenLifespan = TimeSpan.FromMinutes(5));
            // https://github.com/aspnet/Hosting/issues/793
            // the IHttpContextAccessor service is not registered by default.
            // the clientId/clientIp resolvers use it.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // configuration (resolvers, counter key builders)
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
        }
        //Create default accounts at startup if not present
        //Create admin role
        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();


            //Adding Admin Role and default admin user
            var roleCheck = await RoleManager.RoleExistsAsync("CEO");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                await RoleManager.CreateAsync(new IdentityRole("CEO"));
            }
            var admin = new IdentityUser
            {
                UserName = "ceo@myhealthcare.com",
                Email = "ceo@myhealthcare.com",
                EmailConfirmed = true,


            };

            var user = await UserManager.FindByEmailAsync("ceo@myhealthcare.com");
            if (user == null)
            {
                var createpoweruser = await UserManager.CreateAsync(admin, "Superuser1!");
                if (createpoweruser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(admin, "CEO");
                }

            }
            //Staff Role and generic staff email
            var staffCheck = await RoleManager.RoleExistsAsync("Staff");
            if (!staffCheck)
            {
                //create the roles and seed them to the database
                await RoleManager.CreateAsync(new IdentityRole("Staff"));
            }
            var staff = new IdentityUser
            {
                UserName = "staff@myhealthcare.com",
                Email = "staff@myhealthcare.com",
                EmailConfirmed = true,


            };

            var staffuser = await UserManager.FindByEmailAsync("staff@myhealthcare.com");
            if (staffuser == null)
            {
                var createpoweruser = await UserManager.CreateAsync(staff, "Superuser1!");
                if (createpoweruser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(staff, "Staff");
                }

            }

            //Doctor role and generic staff addition
            var doctorCheck = await RoleManager.RoleExistsAsync("Doctor");
            if (!doctorCheck)
            {
                //create the roles and seed them to the database
                await RoleManager.CreateAsync(new IdentityRole("Doctor"));
            }
            var doctor = new IdentityUser
            {
                UserName = "mydoctor@myhealthcare.com",
                Email = "mydoctor@myhealthcare.com",
                EmailConfirmed = true,


            };

            var doctoruser = await UserManager.FindByEmailAsync("mydoctor@myhealthcare.com");
            if (doctoruser == null)
            {
                var createpoweruser = await UserManager.CreateAsync(doctor, "Superuser1!");
                if (createpoweruser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(doctor, "Doctor");
                }

            }
            //Nurse

            var nurseCheck = await RoleManager.RoleExistsAsync("Nurse");
            if (!nurseCheck)
            {
                //create the roles and seed them to the database
                await RoleManager.CreateAsync(new IdentityRole("Nurse"));
            }
            var nurse = new IdentityUser
            {
                UserName = "nurse@myhealthcare.com",
                Email = "nurse@myhealthcare.com",
                EmailConfirmed = true,


            };

            var nurseuser = await UserManager.FindByEmailAsync("nurse@myhealthcare.com");
            if (nurseuser == null)
            {
                var createpoweruser = await UserManager.CreateAsync(nurse, "Superuser1!");
                if (createpoweruser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(nurse, "Nurse");
                }

            }
            // Patient, create only patient role
            var patientroleCheck = await RoleManager.RoleExistsAsync("Patient");
            if (!patientroleCheck)
            {
                //create the roles and seed them to the database
                await RoleManager.CreateAsync(new IdentityRole("Patient"));
            }

        }
        [Obsolete]
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            //Stripe Payment
            StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["SecretKey"]);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //Rate limit to prevet overloading of the system
            app.UseIpRateLimiting();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            //Create user roles
            CreateUserRoles(serviceProvider).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            RotativaConfiguration.Setup(env.ContentRootPath, "wwwroot/Rotativa");
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });
            app.UseHangfireDashboard();
            var options = new BackgroundJobServerOptions { WorkerCount = 1 };
            app.UseHangfireServer(options);
            //Clear Appointment at 8 Pm
            recurringJobManager.AddOrUpdate<JobScheduling>("Clear Appointment", x => x.ClearAppointment(), Cron.Daily(14, 00));
            //Generate report at 9 PM
            int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            recurringJobManager.AddOrUpdate<JobScheduling>("Generate daily report", x => x.GenerateDailyReport(), Cron.Daily(15, 00));
            recurringJobManager.AddOrUpdate<JobScheduling>("Generate Monthly report", x => x.GenerateMonthlyReport(), Cron.Monthly(days, 15));
        }

        /* backgroundJobClient.Enqueue<JobScheduling>(x => x.ClearAppointment());
         backgroundJobClient.Enqueue<JobScheduling>(x => x.GenerateDailyReport());*//*
        //Clear Appointment at 8 Pm
        recurringJobManager.AddOrUpdate<JobScheduling>("Clear Appointment", x => x.ClearAppointment(), Cron.Daily(14, 00));
            //Generate report at 9 PM
            recurringJobManager.AddOrUpdate<JobScheduling>("Generate daily report", x => x.GenerateDailyReport(), Cron.Daily(15, 00));
 */
        }
    }

