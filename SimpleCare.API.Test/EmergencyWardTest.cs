using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Options;

using NSubstitute;

using SimpleCare.EmergencyWards.Application.Values;
using SimpleCare.EmergencyWards.Domain;
using SimpleCare.EmergencyWards.Interfaces;

namespace SimpleCare.API.Test;

public class EmergencyWardTest(SimpleCareWebApplicationFactory fixture) : IClassFixture<SimpleCareWebApplicationFactory>
{
    [Fact(Skip="Cannot do this with negotiate authentication")]
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
                services.AddAuthentication("Test")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>("TestScheme", options => { });
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

public class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
        : base(options, logger, encoder) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[] { new Claim(ClaimTypes.Name, "TestUser") };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}