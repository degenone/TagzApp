namespace TagzApp.WebAspireTest;

public class UnitTest1(PlaywrightAspireFixture fixture) : IClassFixture<PlaywrightAspireFixture>
{
	private readonly IBrowser _Browser = fixture.Browser;
	private readonly string _AppUri = fixture.AppUri;
	private readonly TestUser _TestUser = new("tester.mctestface@testing.com", "pas$W0rd1234", "Tester McTestface", "1234567890");

	[Fact]
	public async Task Test1()
	{
		var page = await _Browser.NewPageAsync();
		//await page.PauseAsync();

		// Go to the app and check we are on the right page.
		await page.GotoAsync(_AppUri);
		await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "TagzApp" }))
			.ToBeVisibleAsync();
		await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Hello, world!" }))
			.ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = 7_500 });
		await Assertions.Expect(page.GetByText("First Start Configuration")).Not
			.ToBeVisibleAsync();

		// Register as first user (and get admin status).
		await page.GetByRole(AriaRole.Link, new() { Name = "System Admin" }).ClickAsync();
		await page.GetByRole(AriaRole.Link, new() { Name = "Register as a new user" }).ClickAsync();
		await page.GetByPlaceholder("name@example.com").ClickAsync();
		await page.GetByPlaceholder("name@example.com").FillAsync(_TestUser.Email);
		await page.Locator("input[name=\"Input\\.Password\"]").ClickAsync();
		await page.Locator("input[name=\"Input\\.Password\"]").FillAsync(_TestUser.Password);
		await page.Locator("input[name=\"Input\\.ConfirmPassword\"]").ClickAsync();
		await page.Locator("input[name=\"Input\\.ConfirmPassword\"]").FillAsync(_TestUser.Password);
		await page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
		await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Register confirmation" }))
			.ToBeVisibleAsync();
		await page.ScreenshotAsync(new PageScreenshotOptions { Path = "screenshot.png" });
	}
}

internal record TestUser(string Email, string Password, string DisplayName, string Phone);
