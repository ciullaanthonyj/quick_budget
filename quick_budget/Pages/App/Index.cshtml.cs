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

        public IndexModel(Data.BudgetContext context)
        {
            _context = context;
        }

        public IList<Budgets> Budgets { get; set; }
        public IList<Expenses> Expenses { get; set; }

        public async Task OnGetAsync()
        {
            string userId = (string)User.FindFirstValue(ClaimTypes.NameIdentifier);
            Budgets = await _context.Budgets.Where(b => b.Owner.Equals(userId)).ToListAsync();
            Expenses = await _context.Expenses.Where(e => e.Owner.Equals(userId)).ToListAsync();
        }
    }
}
