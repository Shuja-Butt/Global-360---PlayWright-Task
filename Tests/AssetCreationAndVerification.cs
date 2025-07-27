
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;


namespace PlaywrightTests;


public class AssetCreationAndVerification :PageTest
{
    private string _assetTag = "";


   override public async Task InitializeAsync()    
    {

        await base.InitializeAsync();
        var statePath = "../bin/Debug/net8.0/state.json";

Console.WriteLine($"Checking state path: {statePath}");
        if (false)
        {
             Console.WriteLine($"Checking state path: {statePath}");

            using var playwright = await Microsoft.Playwright.Playwright.CreateAsync(); 

            await Utils.LoginAndSaveState(playwright);
        }

   

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

        await page.GetByRole(AriaRole.Link, new() { Name = _assetTag }).ClickAsync();


        //Assert Status, tag and model
        await Expect(page.Locator("#details")).ToContainTextAsync("Ready to Deploy");
        await Expect(page.Locator("#details")).ToContainTextAsync(_assetTag);
        await Expect(page.Locator("#details")).ToContainTextAsync("Macbook Pro 13\"");


        await context.CloseAsync();
    }



   


















}
