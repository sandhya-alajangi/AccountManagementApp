using AccountManagementApp.Model.Enumerators;
using AccountManagementApp.Model.Contracts;
namespace AccountManagementApp.Model.Models
{
    /// <summary>
    /// Delimiters
    /// </summary>
    /// <seealso cref="AccountManagementApp.Model.Contracts.IDelimiters" />
    public class Delimiters : IDelimiters
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance has header.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has header; otherwise, <c>false</c>.
        /// </value>
        public bool HasHeader { get; set; }

        /// <summary>
        /// Gets or sets the column delimiter.
        /// </summary>
        /// <value>
        /// The column delimiter.
        /// </value>
        public Delimiter ColumnDelimiter { get; set; }

        /// <summary>
        /// Gets or sets the row delimiter.
        /// </summary>
        /// <value>
        /// The row delimiter.
        /// </value>
        public Delimiter RowDelimiter { get; set; }

        /// <summary>
        /// Gets or sets the text qualifier.
        /// </summary>
        /// <value>
        /// The text qualifier.
        /// </value>
        public TextQualifier TextQualifier { get; set; }
        
       
    }
}
