using TextFileDataRetriever.BusinessServices.Interface;
namespace TextFileDataRetriever.BusinessServices.DataObject
{
    public class Field : IField
    {
        public string Name { get; set; }
        public int ColumnIndex { get; set; }
    }
}
