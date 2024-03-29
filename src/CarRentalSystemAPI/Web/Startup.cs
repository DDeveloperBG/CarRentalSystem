﻿namespace WebAPI
{
    using System.Reflection;

    using Serilog;
    using WebAPI.Infrastructure.Extension;
    using WebAPI.Infrastructure.Filter;
    using WebAPI.Models;
    using WebAPI.Services.Mapping;

    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment currentEnvironment;

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment currentEnvironment)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            this.configuration = configuration;

            this.currentEnvironment = currentEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabase(this.configuration, this.currentEnvironment);

            services.AddSwaggerOpenAPI();
            services.AddVersion();
            services.AddHealthCheck(this.configuration);

            services.AddSingleton(this.configuration);
            Services.BusinessLogic.DependencyInjection.AddServices(services, this.configuration);
            Services.Data.DependencyInjection.AddServices(services);

            services.AddAuth(this.configuration);

            services.AddMvc().AddMvcOptions(options =>
            {
                options.Filters.Add<InputValidatingFilter>();
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory log)
        {
            // This allows request body to be rereadable.
            app.Use((context, next) =>
            {
                context.Request.EnableBuffering();
                return next();
            });

            AutoMapperConfig.RegisterMappings(typeof(RequestResultDTO<>).GetTypeInfo().Assembly);

            app.ConfigureDbContext();

            if (this.currentEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.ConfigureSwagger();
            }
            else
            {
                app.UseHsts();
            }

            app.ConfigureCors(this.configuration);

            app.ConfigureCustomExceptionMiddleware();

            log.AddSerilog();

            app.UseRouting();

            app.UseHealthCheck();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
