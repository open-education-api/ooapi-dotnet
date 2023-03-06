using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using ooapi.v5.core.Repositories;
using ooapi.v5.Security;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.XPath;

namespace ooapi.v5
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnv;

        private IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _hostingEnv = env;
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Allows EF bundle to work when applying migrations in the pipeline
            var connectionString = Configuration.GetConnectionString("ooapiDB") ?? "";

            services.AddDbContext<CoreDBContext>(options =>
                options.UseSqlServer(connectionString,
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "ooapiv5"))
            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            );

            // Add framework services.
            services
                .AddMvc(options =>
                {
                    options.InputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>();
                    options.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter>();
                })
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opts.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
                    opts.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                })
                .AddXmlSerializerFormatters();


            services.AddAuthentication(BearerAuthenticationHandler.SchemeName)
                .AddScheme<AuthenticationSchemeOptions, BearerAuthenticationHandler>(BearerAuthenticationHandler.SchemeName, null);


            services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("ooapiv5.0", new OpenApiInfo
                    {
                        Title = "OoApi V5.0",
                        Version = "Open Onderwijs Api V5.0",
                        Description = "Open Education API"
                        //Contact = new OpenApiContact()
                        //{
                        //    Name = "Open Education API",
                        //    Url = new Uri("https://open-education-api.github.io/specification/v5/docs.html"),
                        //    Email = ""
                        //}//, TermsOfService = new Uri("")
                    });
                    options.UseOneOfForPolymorphism();

                    options.SelectDiscriminatorNameUsing((baseType) => "TypeName");
                    options.SelectDiscriminatorValueUsing((subType) => subType.Name);

                    List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                    xmlFiles.ForEach(xmlFile =>
                        {
                            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                            options.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

                            var comments = new XPathDocument(xmlPath);
                            options.SchemaFilter<XmlCommentsSchemaFilter>(comments);
                            options.IncludeXmlComments(xmlPath);
                        }
                    );

                });
            services.AddSwaggerGenNewtonsoftSupport();

            services.ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.Name);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseRouting();

            //TODO: Uncomment this if you need wwwroot folder
            // app.UseStaticFiles();

            app.UseAuthorization();

            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = $"https://{httpReq.Host}" } };
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/ooapiv5.0/swagger.json", "Open Onderwijs Api V5.0");
            });

            //TODO: Use Https Redirection
            // app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //TODO: Enable production exception handling (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling)
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }
        }
    }
}
