using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AccountManagementApp.Model.Extension;
using AccountManagementApp.Model.Models;
using AccountManagementApp.Model.Contracts;
using AccountManagementApp.FileDataRetriever.Interface;

namespace AccountManagementApp.FileDataRetriever
{
    /// <summary>
	/// Provides Functionality to retrieve data in xml or json or basic formats from  given data consists of multiple rows of data
	/// </summary>
	public class TextDataRetriever : ITextDataRetriever
    {
        #region Private properties

        private List<IField> _fields;
        private readonly string[] _rowDelimiter;
        private readonly string[] _columnDelimiter;
        private readonly string _textQualifier;
        private readonly bool _hasHeader;

        #endregion

        /// <summary>
        /// Provides Functionality to retrieve data in xml or json or basic formats from  given data consists of multiple rows of data
        /// </summary>
        public TextDataRetriever(IDelimiters delimiters, List<IField> fields)
        {
            _textQualifier = delimiters.TextQualifier.ToStringValue();
            _rowDelimiter = new[] { delimiters.RowDelimiter.ToStringValue() };
            _columnDelimiter = new[] { delimiters.ColumnDelimiter.ToStringValue() };
            _hasHeader = delimiters.HasHeader;
            _fields = fields;
        }

        /// <summary>
        /// Retrieves data in basic .net format
        /// </summary>        
        public List<Dictionary<string, string>> DataRetrieveInBasicFormat(string data)
        {
            string[] dataRows = RowSplit(data);
            return Retrieve(dataRows, new List<string>());
        }

        /// <summary>
        /// Retrieves data in basic .net format with invalid records
        /// </summary>        
        public List<Dictionary<string, string>> DataRetrieveInBasicFormat(string data, out List<string> invalidData)
        {
            string[] dataRows = RowSplit(data);
            invalidData = null;
            return Retrieve(dataRows, invalidData);
        }

        /// <summary>
        /// Retrieves data in basic Json format
        /// </summary>       
        public IEnumerable<string> DataRetrieveInJsonFormat(string data)
        {
            foreach (var row in DataRetrieveInBasicFormat(data))
                yield return JsonConvert.SerializeObject(row);
        }

        /// <summary>
        /// Retrieves data in basic Xml format
        /// </summary>        
        public IEnumerable<XElement> DataRetrieveInXmlFormat(string data)
        {
            foreach (var row in DataRetrieveInBasicFormat(data))
                yield return new XElement("DataRow", from column in row select new XElement(column.Key, column.Value));
        }

        /// <summary>
        /// Retrieves data in basic .net format
        /// </summary>     
        public List<Dictionary<string, string>> Retrieve(string[] dataRows, List<string> invalidData)
        {
            if (_fields == null)
            {
                if (dataRows == null || !dataRows.Any())
                    throw new Exception("Could not create or recognize columns. DataRow is empty.");

                CreateHeaderRow(dataRows[0], _hasHeader);
            }

            var result = new List<Dictionary<string, string>>();

            foreach (var dataRow in dataRows)
            {
                if (!string.IsNullOrWhiteSpace(dataRow))
                    result.Add(ReadDataRow(dataRow, invalidData));
            }
            result.RemoveAll(f => f == null);
            return result;
        }

        /// <summary>
        /// Create Header Row
        /// </summary>     
        public void CreateHeaderRow(string header, bool isRowContainsColumnNames = true)
        {
            string[] rowSplit = RowSplit(header);
            if (rowSplit == null || !rowSplit.Any())
                throw new Exception("Incorrect column delimiter.");

            header = rowSplit[0];

            string[] headerColumns = header.Split(_columnDelimiter, StringSplitOptions.None);

            if (_fields == null)
                _fields = new List<IField>();

            int i = 0;
            foreach (var column in headerColumns)
            {
                _fields.Add(new Field() { Name = isRowContainsColumnNames ? column : "column" + i.ToString(), ColumnIndex = i });
                i++;
            }
        }

        private string[] RowSplit(string textData)
        {
            return textData.Split(_rowDelimiter, StringSplitOptions.RemoveEmptyEntries);
        }

        public virtual Dictionary<string, string> ReadDataRow(string text, List<string> invalidData)
        {

            Dictionary<string, string> result = new Dictionary<string, string>();
            IEnumerable<string> splitResult = ColumnSplitWithColumnQualifierTextQualifier(text, string.Join("", _columnDelimiter), _textQualifier);
            foreach (var f in _fields)
            {
                string val = string.Empty;
                if (splitResult.Count() > f.ColumnIndex)
                    val = splitResult.ElementAt(f.ColumnIndex);
                result.Add(f.Name, val);
            }
            return result;
        }

        public IEnumerable<string> ColumnSplitWithColumnQualifierTextQualifier(string text, string columnDelimiter, string textQualifier)
        {
            string splitText = $"{textQualifier}{columnDelimiter}{textQualifier}";
            string[] splittedStrings = text.Split(new[] { splitText }, StringSplitOptions.None);
            if (textQualifier.Length > 0)
            {

                int firstRow = 0, lastRow = splittedStrings.Length - 1;
                splittedStrings[firstRow] = splittedStrings[firstRow].Replace(textQualifier, "");
                splittedStrings[lastRow] = splittedStrings[lastRow].Replace(textQualifier, "");
            }
            return splittedStrings;
        }


    }
}
