namespace TextFileDataRetriever.BusinessServices.Interface
{
    public interface IField
    {
        string Name { get; set; }
        int ColumnIndex { get; set; }
    }
}
