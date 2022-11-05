using System.ComponentModel.DataAnnotations;

namespace quick_budget.Models
{
    public class Expenses
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Owner { get; set; }

        public double Value { get; set; }

        public bool IsIncome { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}