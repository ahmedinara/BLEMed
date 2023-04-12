using AutoMapper;
using BSMediator.Core.BioTimeEntites;
using BSMediator.Core.Repositories.Opreater;
using BSMediator.Core.Repositories.Opreation;
using BSMediator.Core.Repositories.Users;
using BSMediator.Core.Repository;
using BSMediator.Core.ResourceFiles;
using BSMediator.Core.Services.Opreater;
using BSMediator.Core.Services.Opreation;
using BSMediator.Core.Services.User;
using BSMediator.Core.Shared;
using BSMediator.Data.Repositories.Opreater;
using BSMediator.Data.Repositories.Opreation;
using BSMediator.Data.Repositories.User;
using BSMediator.Service.Services.Opreater;
using BSMediator.Service.Services.Opreation;
using BSMediator.Service.Services.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using NFC.Api.Entities;
using NFC.Data.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NFC.MVC
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
            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

            #region Configure DataBase Connection String
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
            Configuration.GetConnectionString("NFC")));

            services.AddDbContext<BioTimeContext>(options =>
            options.UseSqlServer("Data Source=" + Config.DbServerName + ";Initial Catalog=" + Config.DataBaseName + ";User ID=" + Config.DbUserName + ";Password=" + Config.DbPassword + ""));
            #endregion

            //services.AddControllers(config =>
            //{
            //    config.Filters.Add(new LanguageFilter());
            //});
            #region Add Injection Scops
            services.AddScoped<ICommRoleRepository, CommRoleRepository>();
            services.AddScoped<BSMediator.Core.Repositories.Users.IUserRepository, UserApiRepository>();
            services.AddScoped<BSMediator.Core.Repositories.Users.IUserRepository, UserDbRepository>();
            services.AddScoped<BSMediator.Core.Repositories.Opreations.IUserRepository, UserRepository>();
            services.AddScoped<IAppSettingRepository, AppSettingRepository>();
            services.AddScoped<IActionRepository, ActionRepository>();
            services.AddScoped<BSMediator.Core.Services.User.IUserService, BSMediator.Service.Services.User.UserService>();
            services.AddScoped<ICommRoleService, CommRoleService>();
            services.AddScoped<IActionService,ActionService>();
            services.AddScoped<BSMediator.Core.Services.Opreater.IUserService, BSMediator.Service.Services.Opreater.UserService>();
            services.AddScoped<ICommencationHandlerService, CommencationHandlerService>();
            services.AddScoped<IAppSettingService, AppSettingService>();
            services.AddScoped<IAuthApiService, AuthApiService>();
            services.AddScoped<IAuthAdminService, AuthAdminService>();
            services.AddScoped<HttpClientService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            #endregion

            #region Session Configuration
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);//You can set Time (1) Hour   
            });
            #endregion

            services.AddControllers();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en-US");

                var cultures = new CultureInfo[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-SA"),
                };

                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });
            services.AddControllersWithViews();

            services.AddAutoMapper();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePages(async context =>
            {
                var request = context.HttpContext.Request;
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    response.Redirect("User/login");
                }
            });


            app.UseRequestLocalization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
