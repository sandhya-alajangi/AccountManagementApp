using System;
using System.Collections.Generic;
using System.Linq;
using AccountManagementApp.Model.Models;
using AccountManagementApp.Model.Contracts;
using AccountManagementApp.Domain.Contracts;
using AccountManagementApp.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AccountManagementApp.Domain
{
    /// <summary>
    /// FileProcessor which is process the file and do the calculations
    /// </summary>
    public class FileProcessor : IFileProcessor
    {
        private readonly AccountContext _context;

        public FileProcessor(AccountContext context)
        {
            _context = context;
        }

        #region Public Methods

        /// <summary>
        /// Data Validator
        /// </summary>
        /// <param name="chunkData">The chunk data.</param>
        /// <param name="cashbackResult">The  result.</param>
        /// <returns></returns>
        public virtual List<MeterReaders> DataValidator(IEnumerable<Dictionary<string, string>> chunkData,
            IResultCounter resultCounter)
        {
            //Process each chunk of data
            var fileRows = ProcessChuckData(chunkData, resultCounter);

            _context.MeterReaders.AddRange(fileRows);
            _context.SaveChanges();

            return fileRows;
        }

        #endregion

        #region Private Methods


        /// <summary>
        /// Processes the chuck data.
        /// </summary>
        /// <param name="chunkData">The chunk data.</param>
        /// <param name="resultCounter">The cashback result.</param>
        /// <returns></returns>
        private List<MeterReaders> ProcessChuckData(IEnumerable<Dictionary<string, string>> chunkData, IResultCounter resultCounter)
        {
            var fileRow = new List<MeterReaders>();
            //Get accounts data
            List<Account> accounts = _context.Accounts.ToListAsync().Result;
            foreach (var dataPair in chunkData)
            {

                var accountId = dataPair.FirstOrDefault(x => x.Key.Equals("AccountId", StringComparison.OrdinalIgnoreCase))
                    .Value.Trim();
                int.TryParse(accountId, out var id);


                var meterRead = dataPair.FirstOrDefault(x => x.Key.Equals("MeterReadValue", StringComparison.OrdinalIgnoreCase))
                    .Value.Trim();
                var isValid = int.TryParse(meterRead, out var meterReadValue);

                //Consider Valid only
                if (isValid && accounts.Any(p => p.Id == id))
                {
                    resultCounter.NumberOfRowsProcessed++;
                    var dateValue = dataPair.FirstOrDefault(x => x.Key.Equals("MeterReadingDateTime", StringComparison.OrdinalIgnoreCase))
                    .Value;
                    DateTime.TryParse(dateValue, out var MeterReadingDateTime);

                    fileRow.Add(new MeterReaders { AccountId = id, MeterReadingDateTime = MeterReadingDateTime, MeterReadValue = meterReadValue });
                }
                else
                    resultCounter.InValidRows++;

            }
            return fileRow;
        }
        #endregion

    }
}