namespace AccountManagementApp.Model.Contracts
{
    public interface IField
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the index of the column.
        /// </summary>
        /// <value>
        /// The index of the column.
        /// </value>
        int ColumnIndex { get; set; }
    }
}
