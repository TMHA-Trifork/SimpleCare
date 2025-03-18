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
}
