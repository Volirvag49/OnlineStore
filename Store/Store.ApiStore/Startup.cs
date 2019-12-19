using System.Threading;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Store.ApiStore.Infrastructure.Automapper;
using Store.ApiStore.Infrastructure.Middleware.Extensions;
using Store.ApiStore.Services;
using Store.ApiStore.Services.Base;
using Store.Database.EF;
using Store.Database.Repositories;
using Store.Database.Repositories.Base;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using System;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace Store.ApiStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //private static readonly ILoggerFactory _loggerFactory
        //        = LoggerFactory.Create(builder =>
        //        {
        //            builder
        //            .AddFilter((category, level) =>
        //            category == DbLoggerCategory.Database.Command.Name
        //            && level == LogLevel.Information)
        //            .AddDebug();
        //        });

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DefaultContext>(options =>
               options//.UseLoggerFactory(_loggerFactory)
                .UseSqlServer(
                        @"Data Source=(LocalDB)\MSSQLLocalDB;Database=Store;Integrated Security=True"));


            services.AddControllers();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddScoped(a =>
            {
                var accessor = a.GetService<IHttpContextAccessor>();
                return accessor?.HttpContext == null || accessor.HttpContext.Request?.Method != "GET"
                    ? new CancellationTokenSource()
                    : CancellationTokenSource.CreateLinkedTokenSource(accessor.HttpContext.RequestAborted);
            });

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            // ===== Add Providers ========
            services.AddScoped<IReadOnlyRepository, ReadOnlyRepository<DefaultContext>>();
            services.AddScoped<IWriteOnlyRepository, WriteOnlyRepository<DefaultContext>>();

            // ===== Add Services ========
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IFileService, FileService>();
            

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Store", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DefaultContext context)
        {
            app.EnableGlobalExceptions();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Images")),
                RequestPath = "/Images"
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors(options =>
                options.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                DbInitializer.Initialize(context);
            }

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
