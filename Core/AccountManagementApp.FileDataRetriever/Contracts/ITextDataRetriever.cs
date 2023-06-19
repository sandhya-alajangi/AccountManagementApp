using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AccountManagementApp.FileDataRetriever.Interface
{
    public interface ITextDataRetriever
    {
        /// <summary>
        /// Datas the retrieve in basic format.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        List<Dictionary<string, string>> DataRetrieveInBasicFormat(string data);

        /// <summary>
        /// Datas the retrieve in basic format.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="invalidData">The invalid data.</param>
        /// <returns></returns>
        List<Dictionary<string, string>> DataRetrieveInBasicFormat(string data, out List<String> invalidData);

        /// <summary>
        /// Datas the retrieve in json format.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        IEnumerable<string> DataRetrieveInJsonFormat(string data);

        /// <summary>
        /// Datas the retrieve in XML format.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        IEnumerable<XElement> DataRetrieveInXmlFormat(string data);

        /// <summary>
        /// Retrieves the specified data rows.
        /// </summary>
        /// <param name="dataRows">The data rows.</param>
        /// <param name="invalidData">The invalid data.</param>
        /// <returns></returns>
        List<Dictionary<string, string>> Retrieve(String[] dataRows, List<string> invalidData = null);

        /// <summary>
        /// Creates the header row.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="isRowContainsColumnNames">if set to <c>true</c> [is row contains column names].</param>
        void CreateHeaderRow(string header, bool isRowContainsColumnNames = true);
    }
}
