using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartMeterLibServices;
using SmartMeterLibServices.Configurations;
using SmartMeterLibServices.ExternalServices;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;
using SmartMeterLibServices.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SmartMeterWebApp.SmartMeterApi.services;
using SmartMeterWebApp.SmartMeterApi.Data;

namespace SmartMeterWebApp
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
            services.AddRazorPages();
            services.AddScoped<ISubscriberRepository, SubscriberRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IBillingRepository, BillingRepository>();
            services.AddScoped<IMobileBillingRepository, MobileBillingRepository>();
            services.AddScoped<IStackHolderRepository, StackHolderRepository>();
            services.AddSingleton<IEmailSender, EmailSender>();
            //services.AddScoped<IMainViewModel, MainViewModel>();
            services.AddHttpClient();

            //services.AddTransient<EmailSender, EmailSender>();


            services.AddHostedService<SetMeterUsageNotificationBackgroundService>();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "SmartMeterCookie";
                config.LoginPath = "/Identity/Account/Login";
                config.AccessDeniedPath = "/Identity/Account/AccessDenied";

            });


            string dbChoice = Configuration.GetValue<string>("DatabaseChoice").ToLower();
            if (dbChoice == "defaultconnection")
            {               

                services.AddDbContextPool<AppDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection")));
                

            }
            else if (dbChoice == "smartmeterdb")
            {         

                services.AddDbContextPool<AppDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("smartmeterdb")));
                
                services.AddControllersWithViews();
            }
            else if (dbChoice == "covidDBAzure")
            {

                services.AddDbContextPool<AppDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("covidDBAzure")));
                

                services.AddControllersWithViews();
            }
            else
            {
                //services.AddTransient<ISqlData, SqlData>();
            }

            //services.Configure<RouteOptions>(options => {
            //    options.LowercaseUrls = true;
            //    options.LowercaseQueryStrings = true;
            //    options.AppendTrailingSlash = true;
            //options.ConstraintMap.Add("even", typeof(EvenConstraint));      });

            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddRazorPages()
                    .AddRazorPagesOptions(options =>
                    {

                        options.Conventions.AuthorizePage("/stackholders/dashboard");
                        options.Conventions.AuthorizePage("/stackholders/subscribers");
                        options.Conventions.AuthorizePage("/stackholders/deviceslist");
                        options.Conventions.AuthorizePage("/stackholders/billinglist");
                        options.Conventions.AuthorizePage("/stackholders/BillingTr");
                        options.Conventions.AuthorizePage("/stackholders/BillTry");
                        options.Conventions.AuthorizePage("/stackholders/EditDevice");

                        options.Conventions.AuthorizePage("/subscribers/Dashboard");
                        options.Conventions.AuthorizePage("/subscribers/Meterusage");
                        options.Conventions.AuthorizePage("/subscribers/Checkbilling");
                        options.Conventions.AuthorizePage("/subscribers/Detailmeter");
                    });

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = false;
            });

            //var proxy = new HttpClientHandler
            //{
            //    UseProxy = true,
            //    Proxy = null, // use system proxy
            //    DefaultProxyCredentials = CredentialCache.DefaultNetworkCredentials
            //};
            //var jwtSettings = new JwtSettings();
            //Configuration.Bind(nameof(jwtSettings), jwtSettings);
            //services.AddSingleton(jwtSettings);

            //services.AddScoped<IIdentityService, IdentityService>();

            //var tokenValidationParameters = new TokenValidationParameters
            //{
            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
            //    ValidateIssuer = false,
            //    ValidateAudience = false,
            //    RequireExpirationTime = false,
            //    ValidateLifetime = true
            //};

            //services.AddHttpContextAccessor();

            //services.AddSingleton(tokenValidationParameters);

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //    .AddJwtBearer(x =>
            //    {
            //        x.SaveToken = true;
            //        x.TokenValidationParameters = tokenValidationParameters;
            //    });

            //services.AddAuthorization();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
          UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });


            ApplicationUsersDbInitializer.SeedData(userManager, roleManager, context).GetAwaiter().GetResult();


        }
    }
}
