using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleCare.EmergencyWards.Interfaces;
using SimpleCare.Infrastructure.EmergencyWards;
using SimpleCare.Infrastructure.Interfaces.UnitOfWork;
using SimpleCare.Infrastructure.UnitOfWork;

namespace SimpleCare.Infrastructure;

public static class EntityFrameworkInfrastructure
{
    private static IServiceCollection AddEmergencyWardInfrastructure(this IServiceCollection services)
    {
        services
            .AddScoped<IEmergencyPatientRepository, EmergencyPatientRepository>()
            .AddScoped<IEmergencyEncounterRepository, EmergencyEncounterRepository>();

        return services;
    }

    private static IServiceCollection AddBedWardInfrastructure(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddEntityFrameworkInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<SimpleCareDbContext>();

        services
            .AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>()
            .AddEmergencyWardInfrastructure()
            .AddBedWardInfrastructure();

        return services;
    }
}
