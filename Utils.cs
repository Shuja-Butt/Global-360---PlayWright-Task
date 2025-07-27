
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;


namespace PlaywrightTests;


public static class Utils 
{
   



    
       public static async Task LoginAndSaveState(IPlaywright playwright){


        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

    

        await page.GotoAsync("https://demo.snipeitapp.com/login");
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Username" }).FillAsync("admin");
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).FillAsync("password");
        await page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

       
        // Save storage state
        await context.StorageStateAsync(new() { Path = "./state.json" });
        await context.CloseAsync();
        

       }
       
    



   
       

}
