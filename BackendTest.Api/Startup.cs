using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BackendTest.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Allow local Angular requests
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy-LocalAngularRequest",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            var databaseConnection = Configuration.GetConnectionString("DatabaseConnection");
            services.AddSingleton(databaseConnection);

            //external projects for MediatR to find "IRequest" and "IRequestHandler"
            string[] projectReferences = new[] { "BackendTest.Command", "BackendTest.Query" };

            var assembliesForMediatRInjection = new List<Assembly>
            {
                typeof(Startup).Assembly
            };

            foreach (var projectReference in projectReferences)
            {
                var projectReferenceAssembly = Assembly
                    .Load(projectReference)
                    .GetTypes()
                    .Where(type => type.Namespace != null && type.Namespace.StartsWith(projectReference))
                    .FirstOrDefault();

                if (projectReferenceAssembly != null)
                    assembliesForMediatRInjection.Add(projectReferenceAssembly.Assembly);
            }

            services.AddMediatR(assembliesForMediatRInjection.ToArray());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("CorsPolicy-LocalAngularRequest");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
