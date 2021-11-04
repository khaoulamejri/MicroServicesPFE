
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.API.Consumers;
using UserApp.Data;
using UserApp.Domain.Entities;
using UserApp.Services.IServices;
using UserApp.Services.Services;
using GreenPipes;

namespace UserApp.API
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
            var key = Encoding.ASCII.GetBytes(Configuration["Token:Key"]);
            // MassTransit-RabbitMQ Configuration

            services.AddMassTransit(config =>
            {
                //   config.addconsumers(assembly.getentryassembly());




                // config.setkebabcaseendpointnameformatter();
            config.AddConsumer<DeviseCreatedConsumer>();
              config.AddConsumer<EmployeeUserCreatedconsumer>();
                config.AddConsumer<PayCreatedConsumer>();
                config.AddConsumer<EmployeeUpdatedConsumer>();


                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Configuration["EventBusSettings:HostAddress"]);
                    cfg.UseHealthCheck(ctx);
                  //cfg.configureendpoints(ctx);
                    cfg.ReceiveEndpoint("devise-queue", c =>
                    {
                        c.ConfigureConsumer<DeviseCreatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint("empuser-queue", c =>
                    {
                        c.ConfigureConsumer<EmployeeUserCreatedconsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint("pays-queue", c =>
                    {
                        c.ConfigureConsumer<PayCreatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint("empuserup-queue", c =>
                    {
                        c.ConfigureConsumer<EmployeeUpdatedConsumer>(ctx);
                    });

                });
            });
            services.AddMassTransitHostedService();
           
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.Stores.MaxLengthForKeys = 128)
                       .AddEntityFrameworkStores<ApplicationDbContext>()
                       .AddDefaultTokenProviders();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
           

            services.AddDistributedMemoryCache();
            services.AddSession();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Configuration["Token:Issuer"],

                ValidateAudience = true,
                ValidAudience = Configuration["Token:Audience"],

                ValidateIssuerSigningKey = true,/*Configuration["JwtSecurityToken:Key"])*/
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),

                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = Configuration["Token:Issuer"];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
                configureOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<IComposantService, ComposantService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserRolesServices, UserRolesServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IJwtKeysServices, JwtKeysServices>();
            services.AddScoped<IUserRoleCompaniesServices, UserRoleCompaniesServices>();
            services.AddScoped<IRolesPrivilegesServices, RolesPrivilegesServices>();
            services.AddScoped<IProfileServices, ProfileServices>();
            services.AddScoped<ICompanyServices, CompanyServices>();
            services.AddScoped<IDeviseServices, DeviseServices>();
            services.AddScoped<IEmployeeServices, EmployeeServices>();
            services.AddScoped<IPaysServices, PaysServices>();
            services.AddScoped<IProfileComposantService, ProfileComposantService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
   
            //  services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserApp.API", Version = "v1" });
            });
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserApp.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
        app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
