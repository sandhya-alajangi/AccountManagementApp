using TextFileDataRetriever.BusinessServices.Enum;
using TextFileDataRetriever.BusinessServices.Interface;
namespace TextFileDataRetriever.BusinessServices.DataObject
{
    public class Delimiters : IDelimiters
    {
        public bool HasHeader { get; set; }

        public Delimeter ColumnDelimiter { get; set; }

        public Delimeter RowDelimiter { get; set; }

        public TextQualifier TextQualifier { get; set; }
    }
}
