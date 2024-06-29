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
		await page.GetByRole(AriaRole.Link, new() { Name = "System Admin" })
			.ClickAsync();
		await page.GetByRole(AriaRole.Link, new() { Name = "Register as a new user" })
			.ClickAsync();
		var emailInput = page.GetByPlaceholder("name@example.com");
		await emailInput.ClickAsync();
		await emailInput.FillAsync(_TestUser.Email);
		var pwInput = page.Locator("input[name=\"Input\\.Password\"]");
		await pwInput.ClickAsync();
		await pwInput.FillAsync(_TestUser.Password);
		var pwConfirmInput = page.Locator("input[name=\"Input\\.ConfirmPassword\"]");
		await pwConfirmInput.ClickAsync();
		await pwConfirmInput.FillAsync(_TestUser.Password);
		await page.GetByRole(AriaRole.Button, new() { Name = "Register" })
			.ClickAsync();
		await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Register confirmation" }))
			.ToBeVisibleAsync();

		// 'confirm' email.
		await page.GetByRole(AriaRole.Link, new() { Name = "Click here to confirm your" })
			.ClickAsync();
		await Assertions.Expect(page.GetByText("Thank you for confirming your"))
			.ToBeVisibleAsync();

		// Log in.
		await page.GetByRole(AriaRole.Link, new() { Name = "System Admin" })
			.ClickAsync();
		emailInput = page.GetByPlaceholder("name@example.com");
		await emailInput.ClickAsync();
		await emailInput.FillAsync(_TestUser.Email);
		pwInput = page.GetByPlaceholder("password");
		await pwInput.ClickAsync();
		await pwInput.FillAsync(_TestUser.Password);
		await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
		await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "System Administration" }))
			.ToBeVisibleAsync();

		// Add a hastag.
		await page.GetByPlaceholder("New Hashtag").FillAsync("dotnet");
		await page.GetByRole(AriaRole.Button, new() { Name = "Add" }).ClickAsync();

		// Add provider(s).
		await page.GetByRole(AriaRole.Link, new() { Name = "Providers" })
			.ClickAsync();
		await page.GetByText("Mastodon" ).ClickAsync();
		var mastodonTimeout = page.GetByLabel("Mastodon")
			.Locator("input[name=\"Timeout\"]");
		await mastodonTimeout.ClickAsync();
		await mastodonTimeout.FillAsync("00:00:05");
		await page.GetByLabel("Mastodon")
			.Locator("input[name=\"Enabled\"]")
			.CheckAsync();
		await page.GetByRole(AriaRole.Button, new() { Name = "Save" })
			.ClickAsync();
		await page.ScreenshotAsync(new PageScreenshotOptions { Path = "screenshot.png" });
	}
}

internal record TestUser(string Email, string Password, string DisplayName, string Phone);
