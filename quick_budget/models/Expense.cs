using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace quick_budget.Models
{
    /// <summary>
    /// Expenses Class
    /// </summary>
    [Index(nameof(Owner), nameof(Id))]
    public class Expenses
    {
        #region Constructors
        /// <summary>
        /// Parameter-less Constructor
        /// </summary>
        /// <remarks>
        /// Required for scaffolding the UI
        /// </remarks>
        public Expenses()
        {
        }
        #endregion //Constructors

        /// <summary>
        /// Expense Identifier, primary key
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Expense Title
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        /// <summary>
        /// Expense Description
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Expense Owner, String representation of the UserId
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Expense Value, Range 0.1 to 999,999.99
        /// </summary>
        [Required]
        [Range(0.1, 999999.99)]
        [Display(Name = "Value")]
        public double Value { get; set; }

        /// <summary>
        /// Is the expense Income?
        /// </summary>
        [Required]
        [Display(Name = "Is Income?")]
        public bool IsIncome { get; set; }

        /// <summary>
        /// Expense Created at Timestamp
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Expense Updated at Timestamp
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    } //end public class Expenses
} //end namespace quick_budget.Models