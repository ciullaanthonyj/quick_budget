using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quick_budget.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace quick_budget.Pages.App.Budget
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Data.BudgetContext _context;
        public IList<Budgets> Budgets { get; set; }

        public IndexModel(Data.BudgetContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            IQueryable<Budgets> biq = from b in _context.Budgets select b;
        
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            biq = biq.Where(b => b.Owner.Equals(userId));

            Budgets = await biq.AsNoTracking().ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            IQueryable<Budgets> biq = from b in _context.Budgets select b;

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            biq = biq.Where(b => b.Owner.Equals(userId));
            biq = biq.Where(b => b.Id.Equals(id));

            var budget = await biq.AsNoTracking().FirstOrDefaultAsync();

            if(budget is not null)
            {
                _context.Budgets.Remove(budget);
                await _context.SaveChangesAsync();

                return RedirectToPage();
            }

            else{
                return Page();
            }
        }
    }
}
