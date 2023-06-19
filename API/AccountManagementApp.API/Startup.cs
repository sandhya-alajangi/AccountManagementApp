using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AccountManagementApp.Data.Context;
using AccountManagementApp.Domain;
using AccountManagementApp.Domain.Contracts;
using Swashbuckle.AspNetCore.Swagger;

namespace AccountManagementApp.API
{
    /// <summary>
    /// Startup Class provides the Initializes all services required for this App
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IMeterReaderService, MeterReaderService>();
            services.AddScoped<IFileReader, FileReader>();
            services.AddScoped<IFileProcessor, FileProcessor>();

            //Using In Memory Database
            services.AddDbContext<AccountContext>(options =>
                    options.UseInMemoryDatabase(databaseName: "AccountManagementDatabase"));

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "Account Management API", Version = "V1" });
            });
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
