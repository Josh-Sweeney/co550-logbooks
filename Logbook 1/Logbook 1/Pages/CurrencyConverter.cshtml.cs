using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Logbook_1.Pages;

public class CurrencyConverter : PageModel
{
    private readonly decimal _exchangeRate = 1.14m;
    
    // The input currency as GBP
    public decimal InputCurrency { get; set; }
    
    // The output currency as Euro
    public decimal OutputCurrency { get; set; }
    
    public void OnGet()
    {
        
    }

    public void OnPost()
    {
        InputCurrency = decimal.Parse(Request.Form["inputCurrency"]);
        
        OutputCurrency = InputCurrency * _exchangeRate;
    }
}