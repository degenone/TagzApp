using Microsoft.Extensions.Configuration;

namespace TagzApp.WebAspireTest;
public class PlaywrightFixture : IAsyncLifetime
{
    private IPlaywright _PlaywrightInstance = null!;

    public IBrowser Browser { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        _PlaywrightInstance = await Playwright.CreateAsync();
        Browser = await _PlaywrightInstance.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions { Headless = false }
        );
    }

    public async Task DisposeAsync()
    {
        await Browser.CloseAsync();
        _PlaywrightInstance.Dispose();
    }
}

public class UnitTest1(PlaywrightFixture fixture) : IClassFixture<PlaywrightFixture>
{
    private readonly IBrowser _Browser = fixture.Browser;

    [Fact]
    public async Task Test1()
    {
        var builder = await DistributedApplicationTestingBuilder.CreateAsync<Projects.TagzApp_AppHost>();
        builder.Configuration.AddInMemoryCollection(
            [
                new KeyValuePair<string, string?>("Parameters:dbPassword", "dbTestPassword1!"),
            ]);

        await using var app = await builder.BuildAsync();
        await app.StartAsync();
        var endpoint = app.GetEndpoint("web").AbsoluteUri;

        var page = await _Browser.NewPageAsync();
        // await page.PauseAsync();
        await page.GotoAsync(endpoint);
        await Assertions.Expect(page.GetByText("First Start Configuration")).ToBeVisibleAsync();
        await page.ScreenshotAsync(new PageScreenshotOptions { Path = "screenshot.png" });
    }
}