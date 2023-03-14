namespace WebAPI
{
    using System.Reflection;

    using Serilog;
    using WebAPI.Infrastructure.Extension;
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
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddDatabase(this.configuration, this.currentEnvironment);

            services.AddAuth(this.configuration);

            services.AddSwaggerOpenAPI();

            services.AddVersion();

            services.AddSingleton(this.configuration);
            Services.BusinessLogic.DependencyInjection.AddServices(services, this.configuration);
            Services.Data.DependencyInjection.AddServices(services);

            services.AddHealthCheck(this.configuration);
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory log)
        {
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
