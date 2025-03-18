using Microsoft.Extensions.DependencyInjection;

using SimpleCare.BedWards.Domain.Interfaces;
using SimpleCare.BedWards.Interfaces;
using SimpleCare.EmergencyWards.Interfaces;
using SimpleCare.Infrastructure.BedWards;
using SimpleCare.Infrastructure.EmergencyWards;
using SimpleCare.Infrastructure.Interfaces.UnitOfWork;
using SimpleCare.Infrastructure.Orderlies;
using SimpleCare.Infrastructure.UnitOfWork;
using SimpleCare.Orderlies.Domain.Interfaces;

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
        return services
            .AddScoped<IBedWardPatientRepository, BedWardPatientRepository>()
            .AddScoped<IBedWardIncomingPatientRepository, BedWardIncomingPatientRepository>()
            .AddScoped<IBedWardEncounterRepository, BedWardEncounterRepository>()
            .AddScoped<IBedWardRepository, BedWardRepository>();
    }

    private static IServiceCollection AddOrderlyInfrastructure(this IServiceCollection services)
    {
        return services
            .AddScoped<IOrderlyTaskRepository, OrderlyTaskRepository>();
    }

    public static IServiceCollection AddEntityFrameworkInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<SimpleCareDbContext>();

        services
            .AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>()
            .AddEmergencyWardInfrastructure()
            .AddBedWardInfrastructure()
            .AddOrderlyInfrastructure();

        return services;
    }
}
