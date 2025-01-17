using System.Text;
using System.Text.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

[TestCaseOrderer("HomeEnergyApi.Tests.Extensions.PriorityOrderer", "HomeEnergyApi.Tests")]
public class Test
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private string testHome1 = JsonSerializer.Serialize(new Home(1, "Test", "123 Test St.", "Test City", 123));
    private string testHome2 = JsonSerializer.Serialize(new Home(2, "Testy", "456 Assert St.", "Unitville", 456));
    private string testHome3 = JsonSerializer.Serialize(new Home(3, "Tester", "789 Theory St.", "Integration Town", 789));

    public Test(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory, TestPriority(1)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsSuccessfulHTTPResponseCodeOnGETHomes(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        Assert.True(response.IsSuccessStatusCode, $"HomeEnergyApi did not return successful HTTP Response Code on GET request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");
    }

    [Theory, TestPriority(2)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturns201CreatedHTTPResponseOnAddingHomeThroughPOST(string url)
    {
        var client = _factory.CreateClient();

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Post, url);
        sendRequest.Content = new StringContent(testHome1,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);
        Assert.True((int)response.StatusCode == 201, $"HomeEnergyApi did not return \"201: Created\" HTTP Response Code on POST request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");
    }

    [Theory, TestPriority(3)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsHomeBeingCreatedOnPOSTRequest(string url)
    {
        var client = _factory.CreateClient();

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Post, url);
        sendRequest.Content = new StringContent(testHome2,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);
        string responseContent = await response.Content.ReadAsStringAsync();

        Assert.True(responseContent == testHome2, $"HomeEnergyApi did not return the home being added as a response from the POST request at {url}; \n Expected : {testHome2} \n Received : {responseContent} \n");
    }

    [Theory, TestPriority(4)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsCorrectURILocationOfHomeBeingCreatedInPOSTResponseHeaders(string url)
    {
        var client = _factory.CreateClient();

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Post, url);
        sendRequest.Content = new StringContent(testHome3,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);

        string? homeLocation = response.Headers.Location?.ToString();
        string expectedHomeLocation = "/Homes/3";

        Assert.True(homeLocation == expectedHomeLocation, $"HomeEnergyApi did not return the correct location of the Home being added in the response headers from POST request at {url}; \n Expected URI : {expectedHomeLocation} \n Received URI : {homeLocation} \n");
    }
}
