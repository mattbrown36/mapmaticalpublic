using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using mlWebsite.Services;
using mlShared.Data;
using mlShared.Models;
using mlShared.Services;
using mlShared;

namespace mlWebsite
{
    public class Startup
    {
        const string corsDebugPolicy = "CorsDebugPolicy";

        public Startup(IHostingEnvironment env)
        {
            HostingEnvironment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy(corsDebugPolicy, 
                    builder => builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials());
            });

            // Add framework services.
            services.AddMvc(options => {
                options.Filters.Add(typeof(LogExceptionFilter));
            });

            if (HostingEnvironment.IsDevelopment()) {
                ApplicationDbContext.DefaultConnectionString = Util.GetSetting("dev-db-connection");
            }
            else {
                ApplicationDbContext.DefaultConnectionString = Configuration.GetConnectionString("ProductionConnection");
            }

            //Not sure there's a better place to put this?
            ApplicationDbContext.TryToMigrate(ApplicationDbContext.DefaultConnectionString);

            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseMySql(ApplicationDbContext.DefaultConnectionString);
            });

            services.AddIdentity<MlUser, IdentityRole>(config => {
                config.SignIn.RequireConfirmedEmail = false; //We still send out the link though.
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
                app.UseCors(corsDebugPolicy);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseGoogleAuthentication(new GoogleOptions()
            {
                ClientId = Util.GetSetting("google-id"),
                ClientSecret = Util.GetSetting("google-secret"),
            });
            app.UseFacebookAuthentication(new FacebookOptions()
            {
                AppId = Util.GetSetting("facebook-id"),
                AppSecret = Util.GetSetting("facebook-secret"),
            });

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
