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

namespace quick_budget.Pages.App.Expense
{
    public class IndexModel : PageModel
    {
        private readonly Data.BudgetContext _context;

        public IndexModel(Data.BudgetContext context)
        {
            _context = context;
        }

        public IList<Expenses> Expenses { get; set; }

        public async Task OnGetAsync()
        {
            string userId = (string)User.FindFirstValue(ClaimTypes.NameIdentifier);
            Expenses = await _context.Expenses.Where(b => b.Owner == userId).ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if(expense is not null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
