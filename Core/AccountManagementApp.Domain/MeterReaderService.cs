using AccountManagementApp.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountManagementApp.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AccountManagementApp.Model.Models;
using AccountManagementApp.Model.Contracts;
using Newtonsoft.Json;
using System.IO;
using System;

namespace AccountManagementApp.Domain
{
    /// <summary>
    /// MeterReader Service Provides CURD functionalists
    /// </summary>
    /// <seealso cref="IMeterReaderService" />
    public class MeterReaderService : IMeterReaderService
    {
        private readonly AccountContext _context;
        
        /// <summary>
        /// The file reader
        /// </summary>
        private readonly IFileReader _fileReader;

        /// <summary>
        /// The file processor
        /// </summary>
        private readonly IFileProcessor _fileProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeterReaderService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public MeterReaderService(AccountContext context, IFileReader fileReader, IFileProcessor fileProcessor)
        {
            _context = context;
            _fileReader = fileReader;
            _fileProcessor= fileProcessor;
        }


        /// <summary>
        /// Process a file
        /// </summary>
        /// <param name="readingRequest"></param>
        /// <returns></returns>
        public virtual async Task<ResultResponse> ProcessFileAsync(ReadingRequest readingRequest)
        {
            var resultCounter = new ResultCounter();

            //Set File Delimiters
            var fileDetails = GetFileDelimiters(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                  "files\\InputFileConfiguration.json"));

            //Read Complete File
            fileDetails.Name = readingRequest.File.FileName;
            _fileReader.PathForFileToProcess = readingRequest.Path;
            var isFileAbleToRead = _fileReader.FileRead(fileDetails);
            if (!isFileAbleToRead)
                return new ResultResponse { Success = false, Error = "Issue while reading a file", ErrorCode = "CP01" };

            //IEnumerable<Dictionary<string, string>> chunkData = new List<Dictionary<string, string>>();
            ////File read by chunk by chunk
            //foreach (var item in _fileReader.FileRetriever.GetDataInBasicFormat(_fileReader.FilePath))
            //{
            //    var result = _fileProcessor.DataValidator(item, resultCounter);
            //}

            Parallel.ForEach(_fileReader.FileRetriever.GetDataInBasicFormat(_fileReader.FilePath),
                new ParallelOptions() { MaxDegreeOfParallelism = _fileReader.MaxDegreeOfParallelism / 2 },
                (chunkData, state, index) =>
                {
                    //Calculate rows
                    if (chunkData.ToList().Any())
                    {
                        var result = _fileProcessor.DataValidator(chunkData, resultCounter);
                    }

                    chunkData = null;
                    if (index % 100 == 0)
                        GC.Collect();

                });

            return new ResultResponse { Success = true, NumberOfRowsProcessed= resultCounter.NumberOfRowsProcessed, InValidRows= resultCounter.InValidRows };

        }

        /// <summary>
        /// Save File Async
        /// </summary>
        /// <param name="postRequest"></param>
        /// <returns></returns>
        public virtual async Task<ResultResponse> SaveFileAsync(ReadingRequest postRequest)
        {

            var uploads = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");

            var filePath = Path.Combine(uploads, postRequest.File.FileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            if(File.Exists(filePath))
                return new ResultResponse { Success = false, Error = "File processed already", ErrorCode = "CP02" };

            var stream = new FileStream(filePath, FileMode.Create);
            await postRequest.File.CopyToAsync(stream);
            stream.Close();

            postRequest.Path = uploads;

            return null;
        }


        /// <summary>
        /// Gets the object from json.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        private IFileDetail GetFileDelimiters(string filePath)
        {
            string content;
            using (var r = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonFiles", filePath)))
            {
                content = r.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<FileDetail>(content);
        }


        /// <summary>
        /// Gets the Users.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Account>> GetAccounts()
        {            
            return  await _context.Accounts.ToListAsync();      
           
        }


        /// <summary>
        /// Adds the specified User.
        /// </summary>
        /// <param name="candidate">The User.</param>
        public async Task Add(Account user)
        {           
            _context.Accounts.Add(user);
            await _context.SaveChangesAsync();
        }
                          

        /// <summary>
        /// Is UserId Exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsFileNameExists(int id)
        {
            return _context.Accounts.Any(x => x.Id == id);
        }

    }
}