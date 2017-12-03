using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Autofac;
using Aurora.API.Backend.Database;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using System.Runtime.Loader;
using Aurora.API.Backend.Responses;
using Microsoft.Extensions.Options;
using AspNetCore.Identity.MongoDB;
using Microsoft.AspNetCore.Identity;
using Aurora.API.Backend.Database.Collections;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Aurora.API.Helpers;
using System.Diagnostics;

namespace Aurora.API
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options => {
            //        options.TokenValidationParameters =
            //             new TokenValidationParameters
            //             {
            //                 ValidateIssuer = true,
            //                 ValidateAudience = true,
            //                 ValidateLifetime = true,
            //                 ValidateIssuerSigningKey = true,
            //                 ValidIssuer = "Aurora.Security.Bearer",
            //                 ValidAudience = "Aurora.Security.Bearer",
            //                 IssuerSigningKey = JwtSecurityKey.Create("aurora.api super secret key")
            //             };

            //        options.Events = new JwtBearerEvents
            //        {
            //            OnAuthenticationFailed = context =>
            //            {
            //                Debug.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
            //                return Task.CompletedTask;
            //            },
            //            OnTokenValidated = context =>
            //            {
            //                Debug.WriteLine("OnTokenValidated: " + context.SecurityToken);
            //                return Task.CompletedTask;
            //            }
            //        };

            //        options.IncludeErrorDetails = true;
            //    });



            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDb"));
            services.AddSingleton<IUserStore<User>>(provider =>
            {
                var options = provider.GetService<IOptions<MongoDbSettings>>();
                var client = new MongoClient(options.Value.ConnectionString);
                var database = client.GetDatabase("auroradb");
                //var database = Configuration.Get<IMongoDatabase>();

                return new MongoUserStore<User>(database);
            });

            services.AddIdentity<User>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {

            });

            services.AddMvc().AddJsonOptions(j => {
                
            });

            services.AddMediatR(typeof(Startup).Assembly, typeof(Response<>).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
               // app.UseExceptionHandler();
            }

            app.UseAuthentication();
            app.UseMvc();

            //app.Run(async (context) =>
            //{
                
            //});
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MongoModule(null));
        }
    }
}
