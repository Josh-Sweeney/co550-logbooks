﻿using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Logbook_1.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    
    public string? Name { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }

    public void OnPost()
    {
        Name = Request.Form["name"];
    }
}