using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace SwaggerDemo
{
    public class Startup
    {
        private const string SwaggerDocuVersion = "v1";
        private const string SwaggerDocuTitle = ".NET Core Swagger Demo";
        private const string SwaggerDocuDescription = "<p>Demo project to show how to use Swagger in .NET Core</p>";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            /* SWAGGER-HOWTO: register swagger generator */
            services.AddSwaggerGen(x =>
            {
                // set document info (multiple documents possible - i.e. for different versions)
                x.SwaggerDoc(SwaggerDocuVersion, new OpenApiInfo
                {
                    Title = SwaggerDocuTitle,
                    Version = SwaggerDocuVersion,
                    Description = SwaggerDocuDescription,
                    Contact = new OpenApiContact
                    {
                        Email = "add-contact-email",
                        Name = "sume"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/sume2000/SwaggerDemo/blob/master/LICENSE")
                    },
                    TermsOfService = new Uri("https://github.com/sume2000/SwaggerDemo/blob/master/README.md")
                });

                // set the XML comments path for swagger
                /* the following lines must be set in the project file (.csproj) to let VS generate the document:
                    <PropertyGroup>
                        <GenerateDocumentationFile>true</GenerateDocumentationFile>
                        <NoWarn>$(NoWarn);1591</NoWarn>
                    </PropertyGroup>
                */
                var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                x.IncludeXmlComments(xmlPath, true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            /* SWAGGER-HOWTO: add static files if custom CSS is added for swagger UI */
            app.UseStaticFiles();

            /* SWAGGER-HOWTO: serve generated swagger docu as json endpoint */
            app.UseSwagger();
            /* SWAGGER-HOWTO: serve generated swagger docu as swagger UI page */
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint($"/swagger/{SwaggerDocuVersion}/swagger.json", SwaggerDocuTitle);
                // optionally remove route prefix to show swagger UI as the app's root page
                x.RoutePrefix = "";
                // optionally add custom CSS styles for swagger UI (must be added as Embedded Resource and referenced as [projectname].[stylesheet])
                x.InjectStylesheet("/customswagger.css");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}