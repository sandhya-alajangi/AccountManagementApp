using System.Collections.Generic;
using AccountManagementApp.Model.Enumerators;
using AccountManagementApp.Model.Models;

namespace AccountManagementApp.Model.Contracts
{
    public interface IFileDetail
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>

        string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is contains header row. To define if the file has header Row with definition of columns
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is contains header row; otherwise, <c>false</c>.
        /// </value>
        bool IsContainsHeaderRow { get; set; }

        /// <summary>
        /// Gets or sets the row delimiter. Separates Rows in a file
        /// </summary>
        /// <value>
        /// The row delimiter.
        /// </value>
        Delimiter RowDelimiter { get; set; }

        /// <summary>
        /// Gets or sets the column delimiter. Separates Columns in a row
        /// </summary>
        /// <value>
        /// The column delimiter.
        /// </value>
        Delimiter ColumnDelimiter { get; set; }

        /// <summary>
        /// Gets or sets the fields. Fields in the file
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        List<Field> Fields { get; set; }
        
        /// <summary>
        /// Gets or sets the text qualifier. Specifies if there is any character or string qualifies text
        /// </summary>
        /// <value>
        /// The text qualifier.
        /// </value>
        TextQualifier TextQualifier { get; set; }
    }
}