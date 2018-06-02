using System;
using System.Runtime.ExceptionServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleMessagingApp.Core.Database;
using SampleMessagingApp.Core.Model.Identity;

namespace SampleMessagingApp.Web
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; set; }

        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            Environment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    // You also need to update /wwwroot/app/scripts/app.js
                    o.Authority = Configuration["oidc:authority"];
                    o.Audience = Configuration["oidc:clientid"];
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            // [Authorize] would usually handle this
            app.Use(async (context, next) =>
            {
                // Use this if there are multiple authentication schemes
                var authResult = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
                if (authResult.Succeeded && authResult.Principal.Identity.IsAuthenticated)
                {
                    await next();
                }
                else if (authResult.Failure != null)
                {
                    // Rethrow, let the exception page handle it.
                    ExceptionDispatchInfo.Capture(authResult.Failure).Throw();
                }
                else
                {
                    await context.ChallengeAsync();
                }
            });
        }
    }
}
