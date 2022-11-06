using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quick_budget.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace quick_budget.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly quick_budget.Data.BudgetContext _context;

    public IndexModel(ILogger<IndexModel> logger, quick_budget.Data.BudgetContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void OnGet()
    {

    }
}

