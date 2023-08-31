namespace TagzApp.Common;
public class ModeratorCard
{
	public string? Name { get; set; }
	public string? Description { get; set; }
	public string? ImageUrl { get; set; }
	public string? Url { get; set; }
	public string? Color { get; set; }
	public string? BackgroundColor { get; set; }
	public string? BorderColor { get; set; }
	public string? TextColor { get; set; }
	public string? IconUrl { get; set; }
	public string? IconBackgroundColor { get; set; }
	public string? IconBorderColor { get; set; }
	public string? IconTextColor { get; set; }
	public string? Icon { get; set; }
	public string? IconType { get; set; }
	public string? IconSize { get; set; }
	public string? IconPosition { get; set; }
	public string? IconAlignment { get; set; }
	public string? IconColor { get; set; }

	public ModeratorCard()
	{
#if DEBUG
		Name = "Moderator Card";
		Description = "This is a moderator card.";
		ImageUrl = "https://static-cdn.jtvnw.net/jtv_user_pictures/0f9f9f9f-0f9f-0f9f-0f9f-0f9f0f9f0f9f-profile_image-300x300.png";
		Url = "https://www.twitch.tv/";
		Color = "#000000";
		BackgroundColor = "#ffffff";
		BorderColor = "#000000";
		TextColor = "#000000";
		IconUrl = "https://static-cdn.jtvnw.net/jtv_user_pictures/0f9f9f9f-0f9f-0f9f-0f9f-0f9f0f9f0f9f-profile_image-300x300.png";
		IconBackgroundColor = "#ffffff";
		IconBorderColor = "#000000";
		IconTextColor = "#000000";
		Icon = "https://static-cdn.jtvnw.net/jtv_user_pictures/0f9f9f9f-0f9f-0f9f-0f9f-0f9f0f9f0f9f-profile_image-300x300.png";
		IconType = "image";
		IconSize = "1em";
		IconPosition = "left";
		IconAlignment = "center";
		IconColor = "#000000";
#endif

	}
}
