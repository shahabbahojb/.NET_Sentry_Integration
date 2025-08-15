using Microsoft.EntityFrameworkCore;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SampleApp_Review;
using SampleApp_Review.Utility;
using Sentry.OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new() { Title = "SampleApp_Review API", Version = "v1" }); });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(DbUtility.GetConnectionString()));

builder.WebHost.UseSentry(options =>
{
    // A Sentry Data Source Name (DSN) is required.
    // See https://docs.sentry.io/concepts/key-terms/dsn-explainer/
    // You can set it in the SENTRY_DSN environment variable, or you can set it in code here.
    options.Dsn = Environment.GetEnvironmentVariable("SENTRY_DSN");
    // When debug is enabled, the Sentry client will emit detailed debugging information to the console.
    // This might be helpful, or might interfere with the normal operation of your application.
    // We enable it here for demonstration purposes when first trying Sentry.
    // You shouldn't do this in your applications unless you're troubleshooting issues with Sentry.
    options.Debug = true;
    // Adds request URL and headers, IP and name for users, etc.
    options.SendDefaultPii = true;
    // This option is recommended. It enables Sentry's "Release Health" feature.
    options.AutoSessionTracking = true;
    // Enabling this option is recommended for client applications only. It ensures all threads use the same global scope.
    options.IsGlobalModeEnabled = false;
    // Example sample rate for your transactions: captures 10% of transactions
    options.TracesSampleRate = 1.0;
    options.ProfilesSampleRate = 1.0; // Enables profiling
    options.AddEntityFramework();
    options.UseOpenTelemetry();
});

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("weather.forecast"))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddSentry())
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation())
    .UseOtlpExporter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Enable Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleApp_Review API v1"));
}

app.UseHttpsRedirection();
app.UseSentryTracing();

// Add health check endpoint
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

app.MapControllers();
app.Run();