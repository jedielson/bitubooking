namespace BituBooking.Integration.Tests;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

public class IntegrationTestFixture : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // services.Replace(new ServiceDescriptor(typeof(IAuthorizationService), ReplaceAuthorizationService()));
            // services
            //     .AddAuthentication(TestAuthHandler.AuthenticationSheme)
            //     .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.AuthenticationSheme, options => { });

            // services.AddSingleton(sp =>
            // {
            //     var conf = sp.GetRequiredService<IConfiguration>();
            //     return new MockServerClient(conf["Mockserver:Url"], conf.GetValue<int>("Mockserver:Port"));
            // });
        });
    }

    // private static IAuthorizationService ReplaceAuthorizationService()
    // {
    //     var authorizationServiceMock = Substitute.For<IAuthorizationService>();

    //     authorizationServiceMock
    //         .AuthorizeAsync(Arg.Any<ClaimsPrincipal>(), Arg.Any<object>(), Arg.Any<IEnumerable<IAuthorizationRequirement>>())
    //         .Returns(Task.FromResult(AuthorizationResult.Success()));

    //     authorizationServiceMock
    //         .AuthorizeAsync(Arg.Any<ClaimsPrincipal>(), Arg.Any<object>(), Arg.Any<string>())
    //         .Returns(Task.FromResult(AuthorizationResult.Success()));

    //     return authorizationServiceMock;
    // }
}
