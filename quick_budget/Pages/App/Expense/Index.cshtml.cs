using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quick_budget.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace quick_budget.Pages.App.Expense
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Data.BudgetContext _context;
        public IList<Expenses> Expenses { get; set; }

        public IndexModel(Data.BudgetContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            IQueryable<Expenses> eiq = from e in _context.Expenses select e;

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            eiq = eiq.Where(e => e.Owner.Equals(userId));

            Expenses = await eiq.AsNoTracking().ToListAsync();

            return Page();
        }


        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            IQueryable<Expenses> eiq = from e in _context.Expenses select e;

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            eiq = eiq.Where(e => e.Owner.Equals(userId));
            eiq = eiq.Where(e => e.Id.Equals(id));

            var expense = await eiq.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));

            if(expense is not null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();

                return RedirectToPage();
            }
            else{
                return Page();
            }
        }
    }
}
