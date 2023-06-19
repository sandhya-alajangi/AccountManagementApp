using TextFileDataRetriever.BusinessServices.Enum;
namespace TextFileDataRetriever.BusinessServices.Interface
{
    public interface IDelimiters
    {
        bool HasHeader { get; set; }
        Delimeter ColumnDelimiter { get; set; }
        Delimeter RowDelimiter { get; set; }
        TextQualifier TextQualifier { get; set; }
    }
}
