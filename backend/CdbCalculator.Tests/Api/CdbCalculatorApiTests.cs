using System.Net;
using System.Net.Http.Json;
using CdbCalculator.Application.Investments.Contracts;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CdbCalculator.Tests.Api;

public sealed class CdbCalculatorApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CdbCalculatorApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Calculate_ShouldReturnOk_WithExpectedPayload()
    {
        var response = await _client.PostAsJsonAsync(
            "/api/v1/cdb-calculator/calculate",
            new CalculateCdbInvestmentRequest(1000m, 12));

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var payload = await response.Content.ReadFromJsonAsync<CdbInvestmentResponse>();
        payload.Should().BeEquivalentTo(new CdbInvestmentResponse(1123.08m, 1098.47m, 24.62m, 123.08m));
    }

    [Fact]
    public async Task Calculate_ShouldReturnBadRequest_WhenPayloadIsInvalid()
    {
        var response = await _client.PostAsJsonAsync(
            "/api/v1/cdb-calculator/calculate",
            new CalculateCdbInvestmentRequest(0m, 1));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
