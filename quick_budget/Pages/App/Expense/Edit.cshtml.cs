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
    public class EditModel : PageModel
    {
        private readonly quick_budget.Data.BudgetContext _context;

        public EditModel(quick_budget.Data.BudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Expenses Expense { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if(id is null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Expense = await _context.Expenses.Where(b => b.Owner == userId).FirstOrDefaultAsync(b => b.Id.Equals(id));

            if(Expense is null)
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

            if(Expense is not null)
            {
                try
                {
                    _context.Entry(Expense).Property("UpdatedAt").IsModified = true;
                    _context.Entry(Expense).Property("Title").IsModified = true;
                    _context.Entry(Expense).Property("Description").IsModified = true;
                    _context.Entry(Expense).Property("Value").IsModified = true;
                    _context.Entry(Expense).Property("IsIncome").IsModified = true;

                    Expense.UpdatedAt = DateTime.Now;

                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!ExpenseExists(Expense.Id))
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

        private bool ExpenseExists(Guid id)
        {
            return _context.Expenses.Any(b => b.Id.Equals(id));
        }
    }
}
