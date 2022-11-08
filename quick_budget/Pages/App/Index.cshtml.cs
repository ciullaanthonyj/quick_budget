using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quick_budget.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace quick_budget.Pages.App
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Data.BudgetContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(Data.BudgetContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public PaginatedList<Budgets> Budgets { get; set; }
        public PaginatedList<Expenses> Expenses { get; set; }

        public async Task<IActionResult> OnGetAsync(int? expenseIndex, int? budgetIndex)
        {
            string userId = (string)User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(expenseIndex is null)
            {
                expenseIndex = 1;
            }

            if(budgetIndex is null)
            {
                budgetIndex = 1;
            }

            IQueryable<Budgets> biq = from b in _context.Budgets select b;
            IQueryable<Expenses> eiq = from e in _context.Expenses select e;

            biq = biq.Where(b => b.Owner.Equals(userId));
            eiq = eiq.Where(e => e.Owner.Equals(userId));
            
            var pageSize = _configuration.GetValue("PageSize", 5);

            Budgets = await PaginatedList<Budgets>.CreateAsync(
                biq.AsNoTracking(), budgetIndex ?? 1, pageSize);

            Expenses = await PaginatedList<Expenses>.CreateAsync(
                eiq.AsNoTracking(), expenseIndex ?? 1, pageSize); 


            return Page();       
        }
    }
}
