using Hashicorp.Vault.API;
using Hashicorp.Vault.API.Extensions;
using Hashicorp.Vault.API.Options;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using Serilog;
using static Hashicorp.Vault.Package.Extensions.SecretManagerExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(builder =>
{
    builder.AddServerHeader = false;
});

builder.Host
    .UseDefaultServiceProvider((context, options) =>
    {
        options.ValidateOnBuild = true;
    })
    .UseSerilog((context, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration);
    });

builder
    .AddConfiguration()
    .Services.AddServices(builder.Configuration)
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddOpenApi();

var certPassword =
    builder.Configuration["SecretOptions:KestrelCertificatesPassword"];
builder.Configuration["Kestrel:Certificates:Default:Password"] = certPassword;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Serves JSON at /openapi/v1.json
    app.MapOpenApi();

    // Serves interactive UI at /scalar/v1
    app.MapScalarApiReference();
}

app.UseHttpsRedirection()
   .UseSerilogRequestLogging()
   .UseExceptionHandler();

MapOptions<SecretOptions>(app.Services).Wait();
var secretOptions = app.Services.GetRequiredService<IOptions<SecretOptions>>().Value;

app.MapGet("/secrets", async (IServiceProvider serviceProvider) =>
{
    var options = serviceProvider.GetRequiredService<IOptions<SecretOptions>>().Value;
    return TypedResults.Ok(options);
})
.WithName("Secrets");

app.Run();
