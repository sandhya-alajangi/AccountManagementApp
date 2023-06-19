using AccountManagementApp.FileDataRetriever.Interface;
using AccountManagementApp.Model.Contracts;

namespace AccountManagementApp.Domain.Contracts
{
    /// <summary>
    /// FileReader will read the files which setup for delimiters & read the actual file
    /// </summary>
    public interface IFileReader
    {

        #region Properties

        /// <summary>
        /// Gets or sets the chunk size in bytes.
        /// </summary>
        /// <value>
        /// The chunk size in bytes.
        /// </value>
        int ChunkSizeInBytes { get; set; }

        /// <summary>
        /// Gets or sets the maximum degree of parallelism.
        /// </summary>
        /// <value>
        /// The maximum degree of parallelism.
        /// </value>
        int MaxDegreeOfParallelism { get; set; }

        /// <summary>
        /// Gets or sets the path for file to process.
        /// </summary>
        /// <value>
        /// The path for file to process.
        /// </value>
        string PathForFileToProcess { get; set; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        string FilePath { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Files the read.
        /// </summary>
        /// <param name="fileDetail">The file detail.</param>
        /// <returns></returns>
        bool FileRead(IFileDetail fileDetail);

        /// <summary>
        /// Gets or sets the file retriever.
        /// </summary>
        /// <value>
        /// The file retriever.
        /// </value>
        IFileDataRetriever FileRetriever { get; set; }

        /// <summary>
        /// Gets the file data retriever.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="fileDetail">The file detail.</param>
        /// <returns></returns>
        IFileDataRetriever GetFileDataRetriever(string filePath, IFileDetail fileDetail);

        #endregion
    }
}