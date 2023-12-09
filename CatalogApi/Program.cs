using CatalogApi;
using CatalogApi.Api;
using CatalogApi.Extensions;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddApplicationServices();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resourceBuilder =>
    {
        resourceBuilder.AddService(TelemetryConstants.ServiceName);
    })
    .WithMetrics(metricsBuilder => metricsBuilder
        .AddAspNetCoreInstrumentation()
        .AddMeter(TelemetryConstants.Meter.Name)
        .AddMeter("Microsoft.AspNetCore.Hosting")
        .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
        .AddPrometheusExporter()
    )
    .WithTracing(tracingBuilder => tracingBuilder
        .AddAspNetCoreInstrumentation()
        .AddEntityFrameworkCoreInstrumentation()
        .AddOtlpExporter(options => options.Endpoint = new Uri("http://jaeger:4317"))
        .AddConsoleExporter());

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapPrometheusScrapingEndpoint();

app.MapGroup("/api/v1/catalog")
    .WithTags("Catalog API")
    .MapCatalogApi();

app.Run();