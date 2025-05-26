using System.Text.Json.Serialization;

using SimpleCare.API;
using SimpleCare.API.Middleware;
using SimpleCare.BedWards.Application;
using SimpleCare.BedWards.Boundary;
using SimpleCare.BedWards.Domain;
using SimpleCare.BedWards.Domain.Interfaces;
using SimpleCare.EmergencyWards.Application;
using SimpleCare.EmergencyWards.Boundary;
using SimpleCare.EmergencyWards.Domain;
using SimpleCare.EmergencyWards.Interfaces;
using SimpleCare.Infrastructure;
using SimpleCare.Infrastructure.UnitOfWork;
using SimpleCare.Orderlies.Application;
using SimpleCare.Orderlies.Boundary;
using SimpleCare.Orderlies.Domain;
using SimpleCare.Orderlies.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.AddOpenTelemetry();

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

const string corsPolicy = "SimpleCareCorsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, builder =>
    {
        builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()  // This allows the traceparent and tracestate headers
            .WithExposedHeaders("traceparent", "tracestate")  // Explicitly expose the trace headers
            .AllowAnyMethod();
    });
});


builder.Services.Configure<SqlServerSettings>(builder.Configuration.GetSection("SqlServerSettings"));
builder.Services.AddEntityFrameworkInfrastructure();

builder.Services.AddMediatR(cfg => cfg
    .RegisterServicesFromAssemblyContaining<BedWardsApplication>()
    .RegisterServicesFromAssemblyContaining<BedWardsBoundary>()
    .RegisterServicesFromAssemblyContaining<EmergencyWard>()
    .RegisterServicesFromAssemblyContaining<EmergencyWardsBoundary>()
    .RegisterServicesFromAssemblyContaining<OrderliesApplication>()
    .RegisterServicesFromAssemblyContaining<OrderliesBoundary>()
);

builder.Services.AddScoped<IEmergencyWard, EmergencyWardRoot>();
builder.Services.AddScoped<IBedWard, BedWardRoot>();
builder.Services.AddScoped<IOrderly, OrderlyRoot>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors(corsPolicy);

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
