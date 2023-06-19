using AccountManagementApp.Model.Enumerators;

namespace AccountManagementApp.Model.Contracts
{
    public interface IDelimiters
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance has header.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has header; otherwise, <c>false</c>.
        /// </value>
        bool HasHeader { get; set; }

        /// <summary>
        /// Gets or sets the column delimiter.
        /// </summary>
        /// <value>
        /// The column delimiter.
        /// </value>
        Delimiter ColumnDelimiter { get; set; }

        /// <summary>
        /// Gets or sets the row delimiter.
        /// </summary>
        /// <value>
        /// The row delimiter.
        /// </value>
        Delimiter RowDelimiter { get; set; }

        /// <summary>
        /// Gets or sets the text qualifier.
        /// </summary>
        /// <value>
        /// The text qualifier.
        /// </value>
        TextQualifier TextQualifier { get; set; }
    }
}