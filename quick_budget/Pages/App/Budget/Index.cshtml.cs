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

namespace quick_budget.Pages.App.Budget
{
    public class IndexModel : PageModel
    {
        private readonly Data.BudgetContext _context;

        public IndexModel(Data.BudgetContext context)
        {
            _context = context;
        }

        public IList<Budgets> Budgets { get; set; }

        public async Task OnGetAsync()
        {
            string userId = (string)User.FindFirstValue(ClaimTypes.NameIdentifier);
            Budgets = await _context.Budgets.Where(b => b.Owner.Equals(userId)).ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var budget = await _context.Budgets.FindAsync(id);

            if(budget is not null)
            {
                _context.Budgets.Remove(budget);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
