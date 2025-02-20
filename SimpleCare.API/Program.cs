using SimpleCare.BedWards.Application;
using SimpleCare.BedWards.Boundary;
using SimpleCare.EmergencyWards.Application;
using SimpleCare.EmergencyWards.Boundary;
using SimpleCare.EmergencyWards.Domain;
using SimpleCare.EmergencyWards.Interfaces;
using SimpleCare.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEntityFrameworkInfrastructure();

builder.Services.AddMediatR(cfg => cfg
    .RegisterServicesFromAssemblyContaining<BedWardsApplication>()
    .RegisterServicesFromAssemblyContaining<BedWardsBoundary>()
    .RegisterServicesFromAssemblyContaining<EmergencyWard>()
    .RegisterServicesFromAssemblyContaining<EmergencyWardsBoundary>()
);

builder.Services.AddScoped<IEmergencyWard, EmergencyWardRoot>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
