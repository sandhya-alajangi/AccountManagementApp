using System.Collections.Generic;
using System.Xml.Linq;

namespace AccountManagementApp.FileDataRetriever.Interface
{
    public interface IFileDataRetriever
    {
        /// <summary>
        /// Gets the data in basic format.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        IEnumerable<IEnumerable<Dictionary<string, string>>> GetDataInBasicFormat(string filePath);

        /// <summary>
        /// Gets the data in json format.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        IEnumerable<IEnumerable<string>> GetDataInJsonFormat(string filePath);

        /// <summary>
        /// Gets the data in XML format.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        IEnumerable<IEnumerable<XElement>> GetDataInXmlFormat(string filePath);
    }
}
