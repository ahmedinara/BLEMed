using AutoMapper;
using BSMediator.Core.Repositories.Opreation;
using BSMediator.Core.Repository;
using BSMediator.Core.ResourceFiles;
using BSMediator.Core.Services.Opreation;
using BSMediator.Core.Shared;
using BSMediator.Data.Repositories.Opreation;
using BSMediator.Service.Services.Opreation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NFC.Api.Entities;
using NFC.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRFID.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static string SecretKey = "401b09eab3c013d4ca54922bb802bec8fd5318192b0aAMK167d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";

        readonly string MyAllowSpecificOrigins = "CorsPolicy";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Configure DataBase Connection String
            services.AddDbContext<AppDbContext>(options =>
             options.UseSqlServer(
                 Configuration.GetConnectionString("NFC")));
            #endregion

            services.AddControllers();

            services.AddAutoMapper();

            #region Add Injection Scops
            services.AddScoped<BSMediator.Core.Repositories.Opreations.IUserRepository, UserRepository>();

            services.AddScoped<BSMediator.Core.Repositories.Users.IUserRepository, UserApiRepository>();
            services.AddScoped<IAppSettingRepository, AppSettingRepository>();
            services.AddScoped<IAppSettingService, AppSettingService>();
            //services.AddScoped<BSMediator.Core.Repositories.Opreation.IUserRepository, BSMediator.Service.Services.Opreation.>();
            services.AddScoped<BSMediator.Core.Services.User.IAuthApiService, BSMediator.Service.Services.User.AuthApiService>();
            services.AddScoped<BSMediator.Core.Services.Opreation.IAuthAdminService, BSMediator.Service.Services.Opreation.AuthAdminService>();
            services.AddScoped<HttpClientService>();
            services.AddScoped<IEmailService, EmailService>();
            #endregion
            #region Configure Bearer Token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Messages.SecretKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                    option.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = async (context) =>
                        {
                            context.Response.StatusCode = 401;
                            await context.Response.WriteAsync("UnAuthrized");
                        }
                    };
                });
            #endregion

            #region Configure Cors
            services.AddCors(o =>
            {
                o.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                       .AllowAnyHeader().AllowAnyMethod();
                    });
            });
            #endregion

            #region Configure Swagger UI
            services.AddSwaggerGen(c =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NRFID_Api", Version = "v1" });
            });
            #endregion
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {

            }
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NRFID_Api v1"));
            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();            // Added For Authentication
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
         
        }
    }
}
