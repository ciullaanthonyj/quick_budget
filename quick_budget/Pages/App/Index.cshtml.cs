using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quick_budget.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace quick_budget.Pages.App
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Data.BudgetContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(Data.BudgetContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public PaginatedList<Budgets> Budgets { get; set; }
        public PaginatedList<Expenses> Expenses { get; set; }

        public async Task OnGetAsync(int? expenseIndex, int? budgetIndex)
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

            IQueryable<Budgets> budgetIQ = from b in _context.Budgets select b;
            IQueryable<Expenses> expenseIQ = from e in _context.Expenses select e;

            budgetIQ = budgetIQ.Where(b => b.Owner.Equals(userId));
            expenseIQ = expenseIQ.Where(e => e.Owner.Equals(userId));
            
            var pageSize = Configuration.GetValue("PageSize", 5);


            Budgets = await PaginatedList<Budgets>.CreateAsync(
                budgetIQ.AsNoTracking(), budgetIndex ?? 1, pageSize);

            Expenses = await PaginatedList<Expenses>.CreateAsync(
                expenseIQ.AsNoTracking(), expenseIndex ?? 1, pageSize);    
        }
    }
}
