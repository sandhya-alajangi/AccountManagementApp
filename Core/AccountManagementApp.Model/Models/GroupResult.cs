using AccountManagementApp.Model.Contracts;

namespace AccountManagementApp.Model.Models
{
    public class GroupResult : IGroupResult
    {
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public int Month { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the total cashback per month.
        /// </summary>
        /// <value>
        /// The total cashback per month.
        /// </value>
        public decimal TotalCashbackPerMonth { get; set; }

        /// <summary>
        /// Gets or sets the average cashback per month.
        /// </summary>
        /// <value>
        /// The average cashback per month.
        /// </value>
        public decimal AverageCashbackPerMonth { get; set; }

        /// <summary>
        /// Gets or sets the total cashback.
        /// </summary>
        /// <value>
        /// The total cashback.
        /// </value>
        public decimal TotalCashback { get; set; }

        /// <summary>
        /// Gets or sets the average cashback.
        /// </summary>
        /// <value>
        /// The average cashback.
        /// </value>
        public decimal AverageCashback { get; set; }

        /// <summary>
        /// Gets or sets the largest cashback.
        /// </summary>
        /// <value>
        /// The largest cashback.
        /// </value>
        public decimal LargestCashback { get; set; }

        /// <summary>
        /// Gets or sets the smallest cashback.
        /// </summary>
        /// <value>
        /// The smallest cashback.
        /// </value>
        public decimal SmallestCashback { get; set; }

        /// <summary>
        /// Gets or sets the number of rows processed per month.
        /// </summary>
        /// <value>
        /// The number of rows processed per month.
        /// </value>
        public int NumberOfRowsProcessedPerMonth { get; set; }
    }
}