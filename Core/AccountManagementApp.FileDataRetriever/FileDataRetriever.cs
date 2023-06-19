using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using AccountManagementApp.Model.Enumerators;
using AccountManagementApp.Model.Extension;
using AccountManagementApp.Model.Contracts;
using AccountManagementApp.FileDataRetriever.Interface;

namespace AccountManagementApp.FileDataRetriever
{
    public class FileDataRetriever : IFileDataRetriever
    {
        private readonly ITextDataRetriever _dataRetriever;
        private readonly IDelimiters _delimiters;
        private List<IField> _fields;
        private readonly int _eachFileSizeInBytes = int.MaxValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDataRetriever"/> class.
        /// </summary>
        /// <param name="dataRetriever">The data retriever.</param>
        /// <param name="delimiters">The delimiters.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="eachFileSizeInBytes">The each file size in bytes.</param>
        public FileDataRetriever(ITextDataRetriever dataRetriever, IDelimiters delimiters, List<IField> fields,
            int eachFileSizeInBytes = 1000000)
        {
            _dataRetriever = dataRetriever;
            _delimiters = delimiters;
            _fields = fields;

            _eachFileSizeInBytes = 6000000;
        }


        #region Public Methods



        /// <summary>
        /// Gets the data in basic format.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public IEnumerable<IEnumerable<Dictionary<string, string>>> GetDataInBasicFormat(string filePath)
        {
            foreach (string content in GetFileContent(filePath))
                yield return _dataRetriever.DataRetrieveInBasicFormat(content);
        }

        /// <summary>
        /// Gets the data in json format.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public IEnumerable<IEnumerable<string>> GetDataInJsonFormat(string filePath)
        {
            foreach (string content in GetFileContent(filePath))
                yield return _dataRetriever.DataRetrieveInJsonFormat(content);
        }


        /// <summary>
        /// Gets the data in XML format.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public IEnumerable<IEnumerable<XElement>> GetDataInXmlFormat(string filePath)
        {
            foreach (string content in GetFileContent(filePath))
                yield return _dataRetriever.DataRetrieveInXmlFormat(content);
        }

        #endregion

        #region Private Methods



        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        private IEnumerable<string> GetFileContent(string filePath)
        {
            foreach (string text in SplitFile(filePath, _eachFileSizeInBytes))
                yield return text;
        }


        /// <summary>
        /// Splits the file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="chunkSize">Size of the chunk.</param>
        /// <returns></returns>
        private IEnumerable<string> SplitFile(string filePath, int chunkSize)
        {
            byte[] fileBuffer = new byte[chunkSize];
            List<byte> extraBuffer = new List<byte>();
            string dataChunk = string.Empty;

            bool isHeaderFounded = _delimiters.HasHeader ? false : true;


            using (Stream originalFile = File.OpenRead(filePath)) //keep file open untill end
            {
                while (originalFile.Position < originalFile.Length)
                {
                    originalFile.Read(fileBuffer, 0, chunkSize);
                    byte extraByte = fileBuffer[chunkSize - 1]; //Check whether next  byte to chunk is blank space


                    while (extraByte != Convert.ToChar(_delimiters.RowDelimiter.ToStringValue()))
                    //read next byte till given word seperator.. By default it is space
                    {
                        int flag = originalFile.ReadByte();
                        if (flag == -1) break; //End of stream
                        extraByte = (byte)flag;
                        extraBuffer.Add(extraByte);
                    }

                    dataChunk = Encoding.UTF8.GetString(fileBuffer, 0, fileBuffer.Length);
                    if (extraBuffer.Count > 0)
                        dataChunk += Encoding.UTF8.GetString(extraBuffer.ToArray(), 0, extraBuffer.Count);
                    extraBuffer.Clear(); //clear buffer
                    Array.Clear(fileBuffer, 0, fileBuffer.Length + extraBuffer.Count);


                    if (isHeaderFounded == false) // skip first row with Header if we have it
                    {
                        string[] s =
                            dataChunk.Split(Convert.ToChar(_delimiters.RowDelimiter.ToStringValue()));
                        if (s != null && s.Count() > 0)
                            dataChunk = dataChunk.Remove(0,
                                s[0].Length + _delimiters.RowDelimiter.ToStringValue().Count());
                        isHeaderFounded = true;

                    }

                    yield return ReplaceSymbols(dataChunk);
                }


            }

        }

        /// <summary>
        /// Replace LineBreak and Hexadecimal Symbols
        /// </summary>
        /// <param name="chunkData"></param>
        /// <returns></returns>
        private string ReplaceSymbols(string chunkData)
        {
            if (string.IsNullOrEmpty(chunkData))
                return string.Empty;

            if (_delimiters.RowDelimiter == Delimiter.LineBreak)
                chunkData = ReplaceLineBreakSymbols(chunkData);
            //return chunkData;
            chunkData = ReplaceHexadecimalSymbols(chunkData);

            return ReplaceEmpty(chunkData);
        }


        /// <summary>
        /// Replace Carriage Return Symbol with NULL when RowDelimiter as LineBreak
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string ReplaceLineBreakSymbols(string str)
        {
            string symbol = Delimiter.CarriageReturn.ToStringValue();
            return Regex.Replace(str, symbol, string.Empty, RegexOptions.Compiled);
        }

        /// <summary>
        /// Replace Hexadecimal Symbols
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string ReplaceHexadecimalSymbols(string str)
        {
            // https://codebeautify.org/string-hex-converter - Website for getting the encoded string to use for exclusion
            string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26\ufffd]";
            return Regex.Replace(str, r, "", RegexOptions.Compiled); //.Replace("'", "''");
        }

        private string ReplaceEmpty(string str)
        {
            return str = str.Replace(" ", "");
        }

        #endregion
    }
}