using System.Collections.Generic;
using AccountManagementApp.Model.Models;
using AccountManagementApp.Model.Contracts;

namespace AccountManagementApp.Domain.Contracts
{
    /// <summary>
    /// FileProcessor which is process the file and do the calculations
    /// </summary>
    public interface IFileProcessor
    {
        /// <summary>
        /// Data Validator
        /// </summary>
        /// <param name="chunkData">The chunk data.</param>
        /// <param name="resultCounter">The cashback result.</param>
        /// <returns></returns>
        List<MeterReaders> DataValidator(IEnumerable<Dictionary<string, string>> chunkData, IResultCounter resultCounter);

        
    }
}