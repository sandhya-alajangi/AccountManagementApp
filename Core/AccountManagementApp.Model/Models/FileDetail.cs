using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using AccountManagementApp.Model.Enumerators;
using AccountManagementApp.Model.Contracts;

namespace AccountManagementApp.Model.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FileDetail : IFileDetail
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is contains header row. To define if the file has header Row with definition of columns
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is contains header row; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty]
        public bool IsContainsHeaderRow { get; set; }

        /// <summary>
        /// Gets or sets the row delimiter. Separates Rows in a file
        /// </summary>
        /// <value>
        /// The row delimiter.
        /// </value>
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public Delimiter RowDelimiter { get; set; }

        /// <summary>
        /// Gets or sets the column delimiter. Separates Columns in a row
        /// </summary>
        /// <value>
        /// The column delimiter.
        /// </value>
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public Delimiter ColumnDelimiter { get; set; }

        /// <summary>
        /// Gets or sets the fields. Fields in the file
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        [JsonProperty]
        public List<Field> Fields { get; set; } = new List<Field>();

        /// <summary>
        /// Gets or sets the text qualifier. Specifies if there is any character or string qualifies text
        /// </summary>
        /// <value>
        /// The text qualifier.
        /// </value>
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public TextQualifier TextQualifier { get; set; }

    }
}