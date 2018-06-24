// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SampleMessagingApp.Core.Database.Context;
using SampleMessagingApp.Core.Model.Identity;
using SampleMessagingApp.Core.Services.Email;
using SampleMessagingApp.Core.Services.Jwt;
using SampleMessagingApp.Core.Utils;
using SampleMessagingApp.Messaging.Database.Map;
using SampleMessagingApp.Messaging.Fcm.Database.Map;

namespace SampleMessagingApp.Web
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; set; }

        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            Environment = env;

            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddUserSecrets<Startup>()
                .AddEnvironmentVariables()
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register JWT Token Authorization:
            var jwtService = CreateJwtService(Configuration);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Configure the Validation Options:
            .AddJwtBearer(options =>
            {
                options.ClaimsIssuer = jwtService.ClaimsIssuer;
                options.Audience = jwtService.Audience;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = jwtService.SigningKey,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Register Database Entity Maps:
            services.AddSingleton<TopicMap>();
            services.AddSingleton<UserTopicMap>();
            services.AddSingleton<UserRegistrationMap>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    // Enable for 2-FA:
                    //options.SignIn.RequireConfirmedPhoneNumber = true;
                })
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

            // Add MVC Config:
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            });


            // Register Services:
            services.AddSingleton(jwtService);

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailConfirmationService, EmailConfirmationService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }

        private IJwtService CreateJwtService(IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var audience = configuration["SampleMessagingApp:Jwt:Audience"];
            var issuer = configuration["SampleMessagingApp:Jwt:Issuer"];
            var secretKey = configuration["SampleMessagingApp:Jwt:SecretKey"];
            var secretAlgorithm = configuration["SampleMessagingApp:Jwt:SecurityAlgorithm"];

            return new JwtService(
                claimsIssuer: issuer,
                audience: audience,
                signingKey: JwtUtils.GetSymmetricSecurityKey(secretKey),
                signingCredentials: JwtUtils.GetSigningCredentials(secretKey, secretAlgorithm)
               );
        }

        
    }
}
