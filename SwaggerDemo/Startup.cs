using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using StructureMap;
using SwaggerDemo.Model;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;

namespace SwaggerDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(options =>
            {
                options.UseInMemoryDatabase("BookLists");
            });

            services.AddOData();

            services.AddMvc(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new Info
               {
                   Version = "v1",
                   Title = "Test API",
                   Description = "ASP NET CORE Web Api"
               });
           });

            var container = new Container(new ServiceRegistry());
            container.Populate(services);
            return container.GetInstance<IServiceProvider>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseHsts();
            //}
            loggerFactory.AddApplicationInsights(app.ApplicationServices,LogLevel.Trace);
            //loggerFactory.AddDebug(LogLevel.Debug);
            var logger = loggerFactory.CreateLogger<Startup>();
            logger.LogDebug("Debug Hello World!!!");
            logger.LogInformation("Trace Hello World!!!");
            logger.LogWarning("Warning Hello World!!!");


            app.UseMiddleware<ExceptionShieldingMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc(b =>
            {
                b.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                b.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
           });
        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Book>("Books");
            builder.EntitySet<Press>("Presses");
            return builder.GetEdmModel();
        }
    }
}
