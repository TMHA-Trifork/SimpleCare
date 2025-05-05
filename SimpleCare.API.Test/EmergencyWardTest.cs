using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Mvc.Testing;

using NSubstitute;

using SimpleCare.EmergencyWards.Application.Values;
using SimpleCare.EmergencyWards.Domain;
using SimpleCare.EmergencyWards.Domain.Interfaces;

namespace SimpleCare.API.Test;

public class EmergencyWardTest(SimpleCareWebApplicationFactory fixture) : IClassFixture<SimpleCareWebApplicationFactory>
{
    [Fact]
    public async Task GetEmergencyWards_ShouldReturnOk()
    {
        // Arrange
        var emergencyWardRoot = Substitute.For<IEmergencyWard>();
        emergencyWardRoot.GetPatients(
            Arg.Any<EmergencyPatientStatus[]>(),
            Arg.Any<CancellationToken>()).Returns([new Patient(Guid.NewGuid(), "1234567890", "Doe", "John", EmergencyPatientStatus.Registered)]);

        var client = fixture.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IEmergencyWard>(e => emergencyWardRoot);
            });
        }).CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Get, "/api/emergency-wards/patients");

        // Act
        var response = await client.SendAsync(request);
        var patients = await fixture.GetResult<EmergencyPatientListItem[]>(response);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("Doe", patients?.First()?.FamilyName);
    }
}

public class SimpleCareWebApplicationFactory : WebApplicationFactory<Program>
{
    internal async Task<TResult?> GetResult<TResult>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        options.Converters.Add(new JsonStringEnumConverter());

        return JsonSerializer.Deserialize<TResult>(content, options);
    }
}
