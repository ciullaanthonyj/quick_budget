using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace quick_budget.Models
{
    /// <summary>
    /// Budgets Class
    /// </summary>
    [Index(nameof(Owner), nameof(Id))]
    public class Budgets
    {
        #region Constructors
        /// <summary>
        /// Parameter-less Constructor
        /// </summary>
        /// <remarks>
        /// Required for scaffolding the UI
        /// </remarks>
        public Budgets()
        {
            //End public Budgets()
        }
        #endregion //Constructors

        /// <summary>
        /// Budget Identifier, primary key
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Budget Title
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        /// <summary>
        /// Budget Description
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Budget Owner, String representation of the UserId
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Budget Balance, Range 0.1 to 999,999.99
        /// </summary>
        [Required]
        [Range(0.1, 999999.99)]
        [Display(Name = "Balance")]
        public double Balance { get; set; }

        /// <summary>
        /// Budget Created at Timestamp
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "Created Date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Budget Updated At Timestamp
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "Updated Date")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    } //end public class Budgets
} //end namespace quick_budget.Models

