using AccountManagementApp.Model.Contracts;

namespace AccountManagementApp.Model.Models
{
    public class ResultCounter: IResultCounter
    {
        /// <summary>
        /// Gets or sets the number of rows processed.
        /// </summary>
        /// <value>
        /// The number of rows processed.
        /// </value>
        public int NumberOfRowsProcessed { get; set; } = 0;
        public int InValidRows { get; set; }
    }
}