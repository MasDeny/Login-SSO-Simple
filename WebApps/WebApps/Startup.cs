using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApps.Data.Context;
using WebApps.Data.Repositories;
using WebApps.Domain.Repositories;
using WebApps.Domain.Security.Hashing;
using WebApps.Domain.Services;
using WebApps.Mapping;
using WebApps.Security.Hashing;
using WebApps.Services;
//using VisMan.Api.Helpers;
//using VisMan.Api.Mapping;
//using VisMan.Api.Security.Hashing;
//using VisMan.Api.Security.Token;
//using VisMan.Api.Services;
//using VisMan.Data.Context;
//using VisMan.Data.Repositories;
//using VisMan.Domain.Repositories;
//using VisMan.Domain.Security.Hashing;
//using VisMan.Domain.Security.Token;
//using VisMan.Domain.Services;

namespace WebApps
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
            //services.AddControllers();
            services.AddLogging(config =>
            {
                // clear out default configuration
                config.ClearProviders();

                config.AddConfiguration(Configuration.GetSection("Logging"));
                //config.AddDebug();
                //config.AddEventSourceLogger();

                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == EnvironmentName.Development)
                {
                    config.AddConsole()
                        .AddFilter(DbLoggerCategory.Database.Command.Name,
                            LogLevel.Information);
                }
            });

            //JWT AppSetting
            //var appSettingsSection = Configuration.GetSection("AppSettings");
            //services.Configure<AppSettings>(appSettingsSection);

            // Database Connection
           services.AddDbContext <WebAppDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("WebAppConnection")));

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            //services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddJsonOptions(options => options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore);
            services.AddAutoMapper(typeof(ModelToResourceProfile));        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseHttpsRedirection();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
