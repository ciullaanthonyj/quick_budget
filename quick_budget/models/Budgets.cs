using System.ComponentModel.DataAnnotations;

namespace quick_budget.Models
{
    public class Budgets
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Owner { get; set; }

        public double Balance { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

