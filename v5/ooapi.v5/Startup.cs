using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using ooapi.v5.core.Repositories;
using ooapi.v5.Security;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
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
            services.AddDbContext<CoreDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ooapiDB"))
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
                    //opts.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
                    opts.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                })
                .AddXmlSerializerFormatters();

            //services.AddTransient<IValidator>

            services.AddAuthentication(BearerAuthenticationHandler.SchemeName)
                .AddScheme<AuthenticationSchemeOptions, BearerAuthenticationHandler>(BearerAuthenticationHandler.SchemeName, null);


            services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("ooapiv5.0", new OpenApiInfo
                    {
                        Title = "OoApi V5.0",
                        Version = "Open Onderwijs Api V5.0",
                        Description = "Open Education API",
                        Contact = new OpenApiContact()
                        {
                            Name = "Open Education API",
                            Url = new Uri("https://open-education-api.github.io/specification/v5/docs.html"),
                            Email = ""
                        }//, TermsOfService = new Uri("")
                    });
                    options.CustomSchemaIds(type => type.FullName);
                    options.UseOneOfForPolymorphism();

                    options.SelectDiscriminatorNameUsing((baseType) => "TypeName");
                    options.SelectDiscriminatorValueUsing((subType) => subType.Name);

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

                    var comments = new XPathDocument(xmlPath);
                    options.SchemaFilter<XmlCommentsSchemaFilter>(comments);
                    options.IncludeXmlComments(xmlPath);
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
                    var scheme = httpReq.Host.Host.StartsWith("localhost", StringComparison.OrdinalIgnoreCase) ? "http" : "https";
                    swagger.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = $"{scheme}://{httpReq.Host}" } };
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
