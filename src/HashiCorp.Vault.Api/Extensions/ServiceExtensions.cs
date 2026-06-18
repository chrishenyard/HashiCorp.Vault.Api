using FluentValidation;
using Hashicorp.Vault.API.Options;
using Hashicorp.Vault.Package.Extensions;
using Hashicorp.Vault.Package.Options;

namespace Hashicorp.Vault.API.Extensions;

public static class ServiceExtensions
{
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddUserSecrets<Program>(optional: true)
            .AddEnvironmentVariables()
            .Build();

        builder.Configuration.AddConfiguration(configuration);

        return builder;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddScoped<IValidator<HashiCorpVaultOptions>, HashiCorpVaultOptionsValidator>()
            .AddSecretManager(config);

        services.AddOptions<HashiCorpVaultOptions>()
            .Bind(config.GetSection("HashiCorpVaultOptions"))
            .Validate(options =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var validator = serviceProvider.GetRequiredService<IValidator<HashiCorpVaultOptions>>();
                var result = validator.Validate(options);
                if (!result.IsValid)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
                    throw new InvalidOperationException($"Invalid HashiCorpVaultOptions: {errors}");
                }
                return true;
            });

        services.AddOptions<SecretOptions>()
            .Bind(config.GetSection("SecretOptions"));

        return services;
    }
}
