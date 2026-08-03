using KlinikGigiV2.Web.Configurations;
using FastEndpoints.Security;


var builder = WebApplication.CreateBuilder(args);

builder.AddLoggerConfigs();

using var loggerFactory = LoggerFactory.Create(config => config.AddConsole());
var startupLogger = loggerFactory.CreateLogger<Program>();

startupLogger.LogInformation("Starting web host");

builder.Services.AddOptionConfigs(builder.Configuration, startupLogger, builder);
builder.Services.AddServiceConfigs(startupLogger, builder);

// JWT Authentication
builder.Services.AddAuthenticationJwtBearer(o =>
{
  o.SigningKey = builder.Configuration["Jwt:Key"];
});
builder.Services.AddAuthorization();

builder.Services.AddFastEndpoints()
                .SwaggerDocument(o =>
                {
                  o.DocumentSettings = s =>
                  {
                    s.Title = "KlinikGigiV2 API";
                    s.Version = "v1";
                    s.Description = "API untuk sistem manajemen klinik gigi.";
                  };
                  o.ShortSchemaNames = true;
                });

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowFrontend", policy =>
  {
    policy.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
  });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

await app.UseAppMiddlewareAndSeedDatabase();

app.Run();

public partial class Program { }