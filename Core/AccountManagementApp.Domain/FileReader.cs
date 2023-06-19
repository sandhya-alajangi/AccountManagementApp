using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AccountManagementApp.Model.Models;
using AccountManagementApp.Model.Contracts;
using AccountManagementApp.FileDataRetriever.Interface;
using AccountManagementApp.Domain.Contracts;
using AccountManagementApp.FileDataRetriever;

namespace AccountManagementApp.Domain
{
    /// <summary>
    /// FileReader will read the files which setup for delimiters & read the actual file
    /// </summary>
    public class FileReader : IFileReader
    {
        #region Properties

        /// <summary>
        /// Gets or sets the chunk size in bytes.
        /// </summary>
        /// <value>
        /// The chunk size in bytes.
        /// </value>
        public int ChunkSizeInBytes { get; set; }

        /// <summary>
        /// Gets or sets the maximum degree of parallelism.
        /// </summary>
        /// <value>
        /// The maximum degree of parallelism.
        /// </value>
        public int MaxDegreeOfParallelism { get; set; } = 4;

        /// <summary>
        /// Gets or sets the path for file to process.
        /// </summary>
        /// <value>
        /// The path for file to process.
        /// </value>
        public string PathForFileToProcess { get; set; }

        /// <summary>
        /// Gets or sets the file retriever.
        /// </summary>
        /// <value>
        /// The file retriever.
        /// </value>
        public IFileDataRetriever FileRetriever { get; set; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string FilePath { get; set; }


        #endregion

        #region Methods

        /// <summary>
        /// Files the read.
        /// </summary>
        /// <param name="fileDetails">The file detail.</param>
        /// <returns></returns>
        public bool FileRead(IFileDetail fileDetails)
        {
            FilePath = Path.Combine(PathForFileToProcess, fileDetails.Name);

            if (!File.Exists(FilePath))
            {
                Console.WriteLine($"No files found in the location({PathForFileToProcess})");
                return false;
            }

            //set properties with delimiters for file data retriever
            FileRetriever = GetFileDataRetriever(fileDetails.Name, fileDetails);

            return true;
        }


        /// <summary>
        /// Gets the file data retriever.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="fileDetail">The file detail.</param>
        /// <returns></returns>
        public IFileDataRetriever GetFileDataRetriever(string filePath, IFileDetail fileDetail)
        {
            var fileFields = fileDetail.Fields.Select(
                    f => new Field { Name = f.Name, ColumnIndex = f.ColumnIndex })
                .Cast<IField>()
                .ToList();

            var fileDelimiters = new Delimiters
            {
                ColumnDelimiter = fileDetail.ColumnDelimiter,
                RowDelimiter = fileDetail.RowDelimiter,
                HasHeader = fileDetail.IsContainsHeaderRow,
                TextQualifier = fileDetail.TextQualifier
            };
            ITextDataRetriever textRetriever = GetTextDataRetriever(fileDelimiters, fileFields);
            return new FileDataRetriever.FileDataRetriever(textRetriever, fileDelimiters, fileFields, ChunkSizeInBytes);
        }

        /// <summary>
        /// Gets the text data retriever.
        /// </summary>
        /// <param name="fileDelimiters">The file delimiters.</param>
        /// <param name="fileFields">The file fields.</param>
        /// <returns></returns>
        private ITextDataRetriever GetTextDataRetriever(Delimiters fileDelimiters, List<IField> fileFields)
        {
            return new TextDataRetriever(fileDelimiters, fileFields); //This method allows child to change text data retriever when needed
        }


        #endregion
    }
}