namespace BituBooking.Integration.Tests;

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Polly;

[Collection(nameof(IntegrationTestCollection))]
public abstract class TestBase : IClassFixture<IntegrationTestFixture>
{
    public struct PostResult
    {
        public System.Net.HttpStatusCode StatusCode { get; set; }

        public string Location { get; set; }
    }

    protected IntegrationTestFixture Fixture { get; }

    protected HttpClient Client { get; }

    // protected MockServerClient MockServerClient { get; }

    protected TestBase(IntegrationTestFixture fixture)
    {
        Fixture = fixture;

        Client = Fixture.CreateClient();
        // Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TestAuthHandler.AuthenticationSheme);
        // Client.DefaultRequestHeaders.Accept.Clear();
        // Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // MockServerClient = Fixture.Services.GetRequiredService<MockServerClient>();
    }

    protected async Task<PostResult> PostAsync(string path, object body)
    {
        var response = await Client.PostAsJsonAsync(path, body);
        return new PostResult
        {
            Location = response.Headers.Location?.OriginalString ?? string.Empty,
            StatusCode = response.StatusCode
        };
    }

    protected async Task AssertLocation<TResponse>(string url, Action<TResponse> assertion, int retryCount = 5)
    {
        var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(retryCount: retryCount, rn => TimeSpan.FromSeconds(Math.Pow(2, rn)));

        await retryPolicy
            .ExecuteAsync(async () =>
            {
                var getLocation = await Client.GetAsync(url);
                getLocation.StatusCode.Should().Be(HttpStatusCode.OK);

                var data = await getLocation.Content.ReadFromJsonAsync<TResponse>();
                assertion(data ?? throw new NullReferenceException("Location returned nothing"));
                //data.Should().BeEquivalentTo(request);
            });
    }
}
