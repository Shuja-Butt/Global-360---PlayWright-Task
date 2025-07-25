namespace PlaywrightTests;

using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;

public class AssetCreatiom
{
    [Fact]
    public async Task AutomateAssetPageAsync()
    {
    

        await page.GetByRole(AriaRole.Link, new() { Name = "Create New" }).ClickAsync();
        await page.Locator("#select2-company_select-container").ClickAsync();
        await page.GetByText("Company Select Company Select").ClickAsync();

        var assetTagBox = page.GetByRole(AriaRole.Textbox, new() { Name = "Asset Tag", Exact = true });
        await assetTagBox.ClickAsync();
        await assetTagBox.PressAsync("Control+a");
        await assetTagBox.PressAsync("Control+c");
        await assetTagBox.PressAsync("Control+c");
        await assetTagBox.PressAsync("Control+c");

        await page.GetByText("Default Location Select a").ClickAsync();
        await page.Locator("#select2-model_select_id-container").ClickAsync();
        await page.GetByText("Laptops - Macbook Pro 13\"").ClickAsync();

        await page.GetByLabel("Select Status").GetByText("Select Status").ClickAsync();
        await page.GetByRole(AriaRole.Option, new() { Name = "Ready to Deploy" }).ClickAsync();

        await page.Locator("#select2-assigned_user_select-container").ClickAsync();
        await page.GetByText("Bahringer, Mabelle (wolf.").ClickAsync();

        await page.Locator("#submit_button").ClickAsync();
    }
}
