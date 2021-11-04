using Conge.API.Consumers;
using Conge.Data;
using Conge.Services.Iservices;
using Conge.Services.services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

namespace Conge.API
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

            services.AddMassTransit(config => {
                config.AddConsumer<CompanyGetIDConsumer>();
                config.AddConsumer<EmployeeCongeCreatedConsumer>();
                config.AddConsumer<AffectationEmployeeCreatedConsumer>();
                config.AddConsumer<EmployeeCongeUpdatedConsumer>();
                config.AddConsumer<UniteConsumedConsumer>();
                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(Configuration["EventBusSettings:HostAddress"]);
                    cfg.UseHealthCheck(ctx);
                    cfg.ReceiveEndpoint("companygetid-queue", c =>
                    {
                        c.ConfigureConsumer<CompanyGetIDConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint("empconge-queue", c =>
                    {
                        c.ConfigureConsumer<EmployeeCongeCreatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint("affectconge-queue", c =>
                    {
                        c.ConfigureConsumer<AffectationEmployeeCreatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint("empcongeup-queue", c =>
                    {
                        c.ConfigureConsumer<EmployeeCongeUpdatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint("unitconge-queue", c =>
                    {
                        c.ConfigureConsumer<UniteConsumedConsumer>(ctx);
                    });
                });
            });
            services.AddMassTransitHostedService();
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));
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
            //services.AddAuthorization(auth =>
            //{
            //    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            //        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            //        .RequireAuthenticatedUser().Build());
            //});

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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAncienteServices, AncienteServices>();
            services.AddScoped<IDelegationService, DelegationService>();
          //  services.AddScoped<IDemandeCongeServices, DemandeCongeServices>();
            services.AddScoped<IDetailsDroitCongeServices, DetailsDroitCongeServices>();
            services.AddScoped<IDroitCongeServices, DroitCongeServices>();
            services.AddScoped<IEmployeeServices, EmployeeServices>();
            services.AddScoped<IJoursFeriesServices, JoursFeriesServices>();
            services.AddScoped<IMvtCongeServices, MvtCongeServices>();
            services.AddScoped<IPlanDroitCongeServices, PlanDroitCongeServices>();
            services.AddScoped<ISeuilServices, SeuilServices>();
            services.AddScoped<ISoldeCongeServices, SoldeCongeServices>();
            services.AddScoped<ITitreCongeServices, TitreCongeServices>();
            services.AddScoped<ITypeCongeServices, TypeCongeServices>();
            services.AddScoped<IUniteService, UniteService>();
            services.AddScoped<IAffectationEmployeeServices, AffectationEmployeeServices>();




            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Conge.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Conge.API v1"));
            }

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
