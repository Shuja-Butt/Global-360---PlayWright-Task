
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;

namespace PlaywrightTests;


public class AssetCreationAndVerification 
{
    private string _assetTag = "";
    private string _assetModel = "";
    private string _assetStatus = "";



    [Fact(DisplayName = "#1 Login and Save Auth State")]
    public async Task LoginAndSaveAuthState()
    {
        var context = await Browser.NewContextAsync();
        var page = await context.NewPageAsync();

        await page.GotoAsync("https://demo.snipeitapp.com/login");
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Username" }).FillAsync("admin");
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).FillAsync("password");
        await page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        await Expect(page).ToHaveURLAsync("https://demo.snipeitapp.com/");
        await Expect(page.Locator("#success-notification")).ToContainTextAsync("Success");

        // Save storage state
        await context.StorageStateAsync(new() { Path = "./state.json" });
        await context.CloseAsync();
    }






    [Fact(DisplayName = "#2 Asset Creation and Validation")]
    public async Task AssetCreationValidation()
    {
        var context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            StorageStatePath = "./state.json"
        });

        var page = await context.NewPageAsync();

        // Navigate and create asset
        await page.GotoAsync("https://demo.snipeitapp.com");

        await page.GetByRole(AriaRole.Link, new() { Name = "Assets view all" }).ClickAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "Create New" }).ClickAsync();

        _assetTag = await page.GetByRole(AriaRole.Textbox, new() { Name = "Asset Tag", Exact = true }).InputValueAsync();

        await page.Locator("#select2-model_select_id-container").ClickAsync();
        await page.GetByText("Laptops - Macbook Pro 13\"").ClickAsync();

        await page.GetByLabel("Select Status").GetByText("Select Status").ClickAsync();
        await page.GetByRole(AriaRole.Option, new() { Name = "Ready to Deploy" }).ClickAsync();

        await page.Locator("#select2-assigned_user_select-container").ClickAsync();
        await page.Locator(".select2-results ul li").First.ClickAsync();

        await page.Locator("#submit_button").ClickAsync();

        await page.GetByRole(AriaRole.Link, new() { Name = "Assets view all" }).ClickAsync();
        await page.GetByRole(AriaRole.Searchbox, new() { Name = "Search" }).ClickAsync();
        await page.GetByRole(AriaRole.Searchbox, new() { Name = "Search" }).FillAsync(_assetTag);



        // Validate asset is visible
        var assetLink = page.GetByRole(AriaRole.Link, new() { Name = $"{_assetTag}" });
        await Expect(assetLink).ToBeVisibleAsync();

        await context.CloseAsync();
    }



   


















}
