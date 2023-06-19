namespace AccountManagementApp.Model.Contracts
{
    public interface IResultCounter
    {
        /// <summary>
        /// Gets or sets the number of rows processed.
        /// </summary>
        /// <value>
        /// The number of rows processed.
        /// </value>
        int NumberOfRowsProcessed { get; set; }

        /// <summary>
        /// Gets or sets the number of rows In valid rows.
        /// </summary>
        /// <value>
        /// The number of rows processed.
        /// </value>
        int InValidRows { get; set; }
    }
}