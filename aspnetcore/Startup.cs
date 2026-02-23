using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnetcore.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using aspnetcore.Services;
using aspnetcore.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace aspnetcore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            // Setup database connection string
            ProcedureHelper.ConnectionString = Configuration["App:ConnectionString"];
            // Initialize resulthandler helper
            ResultHandler.Initialize();
            // Get secret key for user service
            AccountsService.AuthKey = Configuration["App:AuthKey"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // Remove error model response in Swagger
            services.AddMvc().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
            });
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Web store ASP.NET Web APIs",
                    Version = "v1",
                });
                // Add Authorization in Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            // Add Cors origin for React Client
            services.AddCors(options =>
            {
                options.AddPolicy("OnlyOwnClientOrigin", builder =>
                {
                    builder
                        .WithOrigins(Configuration["App:CorsOrigins"].Split(
                            ",", StringSplitOptions.RemoveEmptyEntries))
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
            // JWT authentication
            var key = Encoding.ASCII.GetBytes(Configuration["App:AuthKey"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            // Dependency Injection
            services.AddScoped<IAccountsService, AccountsService>();
            services.AddScoped<IAdminDivsService, AdminDivsService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IOrderStatusesService, OrderStatusesService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<ICartsService, CartsService>();
            services.AddScoped<ICartDetailsService, CartDetailsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();


            app.UseSwagger();
            app.UseSwaggerUI(options => { ... });

            if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

            app.UseRouting();

            app.UseCors("OnlyOwnClientOrigin"); 

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
           });
        }
    }
}
