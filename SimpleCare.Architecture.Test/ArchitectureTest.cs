using SimpleCare.API.Controllers;
using System.Reflection;

namespace SimpleCare.Architecture.Test;

public class ArchitectureTest
{
    [Fact]
    public void DomainLibrariesShouldNotReferenceAnyOtherSolutionLibraries()
    {
        var apiAssembly = typeof(EmergencyWardController).Assembly;

        var assemblyNames = apiAssembly.GetReferencedAssemblies();

        var domainAssemblies = assemblyNames
            .Where(assemblyName => assemblyName.Name?.EndsWith(".Domain") ?? false)
            .Select(assemblyName => Assembly.Load(assemblyName));

        foreach (var domainAssembly in domainAssemblies)
        {
            var allReferenced = domainAssembly.GetReferencedAssemblies();

            var infrastructureReference = allReferenced.FirstOrDefault(assemblyName => assemblyName.Name?.EndsWith(".Infrastructure.Interfaces") ?? false);

            Assert.True(infrastructureReference is null, $"{domainAssembly.GetName().Name} has a reference to infrastructure interface library");

            var referenced = allReferenced
                .Where(assemblyName =>
                    ((assemblyName.Name?.EndsWith(".Domain") ?? false) && (assemblyName.Name != domainAssembly.GetName().Name))||
                    (assemblyName.Name?.EndsWith(".Application") ?? false) ||
                    (assemblyName.Name?.EndsWith(".Boundary") ?? false));

            Assert.False(referenced.Any(), $"{domainAssembly.GetName().Name} has references to other module class libraries");
        }
    }

    [Fact]
    public void ApplicationLibrariesShouldNotReferenceInfrastructureOrAnyOtherDomainLibrary()
    {
        var apiAssembly = typeof(EmergencyWardController).Assembly;

        var assemblyNames = apiAssembly.GetReferencedAssemblies();

        var applicationAssemblies = assemblyNames
            .Where(assemblyName => assemblyName.Name?.EndsWith(".Application") ?? false)
            .Select(assemblyName => Assembly.Load(assemblyName));

        var domainAssemblyNames = assemblyNames
            .Where(assemblyName => assemblyName.Name?.EndsWith(".Domain") ?? false);

        foreach (var applicationAssembly in applicationAssemblies)
        {
            var allReferenced = applicationAssembly.GetReferencedAssemblies();

            var infrastructureReference = allReferenced.FirstOrDefault(assemblyName => assemblyName.Name?.EndsWith(".Infrastructure") ?? false);

            Assert.True(infrastructureReference is null, $"{applicationAssembly.GetName().Name} has a reference to infrastructure library");

            var domainName = applicationAssembly.GetName().Name?.Replace(".Application", ".Domain") ?? string.Empty;
            var otherDomainReferences = allReferenced
                .Where(assemblyName => (assemblyName.Name?.EndsWith(".Domain") ?? false) && (assemblyName.Name != domainName));

            Assert.False(otherDomainReferences.Any(), $"{applicationAssembly.GetName().Name} has references to other modules domain library");
        }
    }

    [Fact]
    public void BoundaryLibrariesShouldNotReferenceAnyOtherSolutionLibraries()
    {
        var apiAssembly = typeof(EmergencyWardController).Assembly;

        var assemblyNames = apiAssembly.GetReferencedAssemblies();

        var domainAssemblies = assemblyNames
            .Where(assemblyName => assemblyName.Name?.EndsWith(".Boundary") ?? false)
            .Select(assemblyName => Assembly.Load(assemblyName));

        foreach (var boundaryAssembly in domainAssemblies)
        {
            var allReferenced = boundaryAssembly.GetReferencedAssemblies();

            var infrastructureReference = allReferenced.FirstOrDefault(assemblyName =>
                (assemblyName.Name?.EndsWith(".Infrastructure") ?? false) ||
                (assemblyName.Name?.EndsWith(".Infrastructure.Interfaces") ?? false));

            Assert.True(infrastructureReference is null, $"{boundaryAssembly.GetName().Name} has a reference to infrastructure (interface) library");

            var referenced = allReferenced
                .Where(assemblyName =>
                    (assemblyName.Name?.EndsWith(".Domain") ?? false) ||
                    (assemblyName.Name?.EndsWith(".Application") ?? false) ||
                    ((assemblyName.Name?.EndsWith(".Boundary") ?? false) && (assemblyName.Name != boundaryAssembly.GetName().Name)));

            Assert.False(referenced.Any(), $"{boundaryAssembly.GetName().Name} has references to other module class libraries");
        }
    }
}
