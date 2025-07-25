using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;

namespace PlaywrightTests;

public class GlobalConfig : PageTest
{
    [Fact]
    public async Task LoginAndSaveAuthState()
    {
        var context = await Browser.NewContextAsync();
        var page = await context.NewPageAsync();

        await page.GotoAsync("https://demo.snipeitapp.com/login");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Username" }).ClickAsync();
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Username" }).FillAsync("admin");
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).ClickAsync();
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).FillAsync("password");
        await page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
        // Save storage state
        await context.StorageStateAsync(new()
        {
            Path = "./state.json"
        });


        await Expect(page).ToHaveURLAsync("https://demo.snipeitapp.com/");
        await Expect(page.Locator("#success-notification")).ToContainTextAsync("Success You have successfully logged in");

        await context.CloseAsync();
    }
}
