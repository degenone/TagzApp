namespace TagzApp.WebAspireTest;

public class PlaywrightAspireFixture : IAsyncLifetime
{
	private IPlaywright _PlaywrightInstance = null!;
	private DistributedApplication _App = null!;

	public IBrowser Browser { get; private set; } = null!;
	public string AppUri { get; private set; } = string.Empty;

	public PlaywrightAspireFixture()
	{
		Environment.SetEnvironmentVariable("RUNNING_MODE", "test");
		Environment.SetEnvironmentVariable("Parameters:dbPassword", "dbTestPassw0rd!");
	}

	public async Task InitializeAsync()
	{
		_PlaywrightInstance = await Playwright.CreateAsync();
		Browser = await _PlaywrightInstance.Chromium.LaunchAsync(
		//new BrowserTypeLaunchOptions { Headless = false }
		);

		var builder =
			await DistributedApplicationTestingBuilder.CreateAsync<Projects.TagzApp_AppHost>();
		_App = await builder.BuildAsync();
		await _App.StartAsync();

		AppUri = _App.GetEndpoint("web").AbsoluteUri;
	}

	public async Task DisposeAsync()
	{
		await Browser.CloseAsync();
		_PlaywrightInstance.Dispose();
		await _App.DisposeAsync();
	}
}
