namespace WebAPI.Infrastructure.Extension
{
    using System.Text;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using WebAPI.Common;
    using WebAPI.Data;
    using WebAPI.Data.Common;
    using WebAPI.Data.Common.Repositories;
    using WebAPI.Data.Models;
    using WebAPI.Data.Repositories;

    public static class ConfigureServiceContainer
    {
        public static void AddDatabase(
            this IServiceCollection serviceCollection,
            IConfiguration configuration,
            IWebHostEnvironment currentEnvironment)
        {
            serviceCollection.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            serviceCollection.AddScoped<IDbQueryRunner, DbQueryRunner>();

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("CarRentalSystemDb"));
            }
            else
            {
                serviceCollection.AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlServer(configuration.GetDefaultConnectionString()));
            }

            serviceCollection.AddIdentity<ApplicationUser, ApplicationRole>(x => IdentityOptionsProvider.GetIdentityOptions(x, currentEnvironment.IsProduction()))
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddVersion(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        public static void AddAuth(
            this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(p =>
                {
                    var key = Encoding.UTF8.GetBytes(configuration[GlobalConstants.ConfigurationKeys.JWT.SecretKey]);

                    p.SaveToken = true;
                    p.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration[GlobalConstants.ConfigurationKeys.JWT.IssuerKey],
                        ValidAudience = configuration[GlobalConstants.ConfigurationKeys.JWT.AudienceKey],
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateLifetime = true,
                    };
                });

            serviceCollection.AddAuthorization();
        }

        public static void AddSwaggerOpenAPI(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(setupAction =>
            {
                setupAction.CustomSchemaIds(x => x.FullName);

                setupAction.SwaggerDoc(
                    "v1",
                    new OpenApiInfo()
                    {
                        Title = GlobalConstants.SystemName,
                        Version = "1",
                    });

                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = $"Input your Bearer token in this format - Bearer token to access this API",
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        }, new List<string>()
                    },
                });
            });
        }

        public static void AddHealthCheck(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>(name: "Application DB Context", failureStatus: HealthStatus.Degraded)
                .AddUrlGroup(new Uri(configuration[GlobalConstants.ConfigurationKeys.ApplicationUrlKey]), name: "Client", failureStatus: HealthStatus.Degraded)
                .AddSqlServer(configuration.GetDefaultConnectionString());

            serviceCollection.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.AddHealthCheckEndpoint("Basic Health Check", $"/healthz");
            }).AddInMemoryStorage();
        }
    }
}
