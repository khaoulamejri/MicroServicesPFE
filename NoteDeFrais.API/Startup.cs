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
using NoteDeFrais.API.Consumer;
using NoteDeFrais.Data;
using NoteDeFrais.Services.IServices;
using NoteDeFrais.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.API
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
            // MassTransit - RabbitMQ Configuration
            services.AddMassTransit(config => {
                config.AddConsumer<PaysCreatedConsumer>();
                config.AddConsumer<EmployeeNoteCreatedConsumer>();
                config.AddConsumer<EmployeeUserUpdatedConsumer>();
                config.AddConsumer<DeviseNoteConsumer>();
                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(Configuration["EventBusSettings:HostAddress"]);
                    cfg.UseHealthCheck(ctx);
                    cfg.ReceiveEndpoint("pays-queue", c =>
                    {
                        c.ConfigureConsumer<PaysCreatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint("empnote-queue", c =>
                    {
                        c.ConfigureConsumer<EmployeeNoteCreatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint("empupdatenote-queue", c =>
                    {
                        c.ConfigureConsumer<EmployeeUserUpdatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint("devisenote-queue", c =>
                    {
                        c.ConfigureConsumer<DeviseNoteConsumer>(ctx);
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
            services.AddScoped<ICompteComptableServices, CompteComptableServices>();
            services.AddScoped<IDepensesServices, DepensesServices>();
            services.AddScoped<IDeviseServices, DeviseServices>();
            services.AddScoped<IEmployeeGroupeServices, EmployeeGroupeServices>();
            services.AddScoped<IEmployeeServices, EmployeeServices>();
            services.AddScoped<IFileNServices, FileNServices>();
            services.AddScoped<IFraisKilometriquesServices, FraisKilometriquesServices>();
          services.AddScoped<IPaysServices, PaysServices>();
            services.AddScoped<IDeviseServices, DeviseServices>();
            services.AddScoped<IGroupeDepenseServices, GroupeDepenseServices>();
            services.AddScoped<IGroupeFraisServices, GroupeFraisServices>();
            services.AddScoped<IMoyenPaiementServices, MoyenPaiementServices>();
            services.AddScoped<INoteFraisServices, NoteFraisServices>();
            services.AddScoped<IOrdreMissionServices, OrdreMissionServices>();
            services.AddScoped<ITypeDepenseServices, TypeDepenseServices>();
            services.AddScoped<ITypeOrdreMissionServices, TypeOrdreMissionServices>();
          //  services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NoteDeFrais.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NoteDeFrais.API v1"));
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
