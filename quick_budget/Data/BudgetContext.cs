using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using quick_budget.Models;

namespace quick_budget.Data
{
    public class BudgetContext : DbContext
    {
        public BudgetContext(DbContextOptions<BudgetContext> options) : base(options)
        {

        }

        #region DbSets
        public DbSet<quick_budget.Models.Budgets> Budgets { get; set; }
        public DbSet<quick_budget.Models.Expenses> Expenses { get; set; }
        #endregion //DbSets
    }
}

