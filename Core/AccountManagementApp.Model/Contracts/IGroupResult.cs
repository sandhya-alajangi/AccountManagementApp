namespace AccountManagementApp.Model.Contracts
{
    public interface IGroupResult
    {
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        int Month { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        int Year { get; set; }

        /// <summary>
        /// Gets or sets the total cashback per month.
        /// </summary>
        /// <value>
        /// The total cashback per month.
        /// </value>
        decimal TotalCashbackPerMonth { get; set; }

        /// <summary>
        /// Gets or sets the average cashback per month.
        /// </summary>
        /// <value>
        /// The average cashback per month.
        /// </value>
        decimal AverageCashbackPerMonth { get; set; }

        /// <summary>
        /// Gets or sets the total cashback.
        /// </summary>
        /// <value>
        /// The total cashback.
        /// </value>
        decimal TotalCashback { get; set; }

        /// <summary>
        /// Gets or sets the average cashback.
        /// </summary>
        /// <value>
        /// The average cashback.
        /// </value>
        decimal AverageCashback { get; set; }

        /// <summary>
        /// Gets or sets the largest cashback.
        /// </summary>
        /// <value>
        /// The largest cashback.
        /// </value>
        decimal LargestCashback { get; set; }

        /// <summary>
        /// Gets or sets the smallest cashback.
        /// </summary>
        /// <value>
        /// The smallest cashback.
        /// </value>
        decimal SmallestCashback { get; set; }

        /// <summary>
        /// Gets or sets the number of rows processed per month.
        /// </summary>
        /// <value>
        /// The number of rows processed per month.
        /// </value>
        int NumberOfRowsProcessedPerMonth { get; set; }

    }
}