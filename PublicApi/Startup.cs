using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using DataAccess;
using PublicApi.DiExtensions;
using PublicApi.Middlewares;
using DataAccess.Initialization;

namespace PublicApi
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
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // setting Pascal casing explicitly instead of default Camel
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.WriteIndented = true;
                })
                .AddXmlSerializerFormatters();

            var connectionString = Configuration.GetConnectionString("PostgreSqlConnection");
            services.AddDbContext<ApplicationDbContext>(
                options =>
                    options.UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.EnableRetryOnFailure())
                    .EnableSensitiveDataLogging()
            );

            services.AddRepos();

            services.AddServices();

            services.AddValidators();

            services.AddSwaggerGen(
                c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "PublicApi", Version = "v1" })
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PublicApi v1"));

                SampleDataInitializer.DropAndMigrateAndSeedDatabase(context, loggerFactory);
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseMiddleware<ServiceErrorHandlerMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
