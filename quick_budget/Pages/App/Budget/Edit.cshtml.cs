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
    public class EditModel : PageModel
    {
        private readonly quick_budget.Data.BudgetContext _context;

        public EditModel(quick_budget.Data.BudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Budgets Budget { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if(id is null)
            {
                return NotFound();
            }

            string userId = (string)User.FindFirstValue(ClaimTypes.NameIdentifier);

            Budget = await _context.Budgets.Where(b => b.Owner == userId).FirstOrDefaultAsync(b => b.Id.Equals(id));

            if(Budget is null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            if(Budget is not null)
            {
                try
                {
                    _context.Entry(Budget).Property("UpdatedAt").IsModified = true;
                    _context.Entry(Budget).Property("Title").IsModified = true;
                    _context.Entry(Budget).Property("Description").IsModified = true;
                    _context.Entry(Budget).Property("Balance").IsModified = true;

                    Budget.UpdatedAt = DateTime.Now;

                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!BudgetExists(Budget.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BudgetExists(Guid id)
        {
            return _context.Budgets.Any(b => b.Id.Equals(id));
        }
    }
}
