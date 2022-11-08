using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using quick_budget.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace quick_budget.Pages.App.Budget
{
    [Authorize]
    public class NewModel : PageModel
    {

        private readonly Data.BudgetContext _context;

        public NewModel(Data.BudgetContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }

        [BindProperty]
        public Budgets Budget { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            string userId = (string)this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if(ModelState.IsValid)
                {
                    Budget.Owner = userId;
                    _context.Add(Budget);

                    await _context.SaveChangesAsync();
                }
                return RedirectToPage("./Index");
            
        }
    }
}
