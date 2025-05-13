using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace SimpleCare.API;

public static class OpenTelemetryExtension
{
    public static WebApplicationBuilder AddOpenTelemetry(this WebApplicationBuilder builder)
    {
        // Setup logging to be exported via OpenTelemetry
        //builder.Logging.AddOpenTelemetry(logging =>
        //{
        //    logging.IncludeFormattedMessage = true;
        //    logging.IncludeScopes = true;
        //});

        var otel = builder.Services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("SimpleCare"))
            .WithTracing(tracing => tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddSource("SimpleCare"))
            .UseOtlpExporter();

        // Add Metrics for ASP.NET Core and our custom metrics and export via OTLP
        //otel.WithMetrics(metrics =>
        //{
        //    // Metrics provider from OpenTelemetry
        //    metrics.AddAspNetCoreInstrumentation();

        //    // Metrics provides by ASP.NET Core in .NET 8
        //    metrics.AddMeter("Microsoft.AspNetCore.Hosting");
        //    metrics.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
        //});

        // Export OpenTelemetry data via OTLP, using env vars for the configuration
        //var OtlpEndpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];
        //if (OtlpEndpoint != null)
        //{
        //    otel.UseOtlpExporter();
        //}

        return builder;
    }
}
