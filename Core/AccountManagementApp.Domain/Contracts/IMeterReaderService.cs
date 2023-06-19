using System.Collections.Generic;
using System.Threading.Tasks;
using AccountManagementApp.Model.Models;

namespace AccountManagementApp.Domain.Contracts
{
    public interface IMeterReaderService
    {
        /// <summary>
        /// Save File Async
        /// </summary>
        /// <param name="postRequest"></param>
        /// <returns></returns>
        Task<ResultResponse> SaveFileAsync(ReadingRequest postRequest);

        /// <summary>
        /// Process File Async
        /// </summary>
        /// <param name="postRequest"></param>
        /// <returns></returns>
        Task<ResultResponse> ProcessFileAsync(ReadingRequest postRequest);

        /// <summary>
        /// Gets the Users.
        /// </summary>
        /// <returns></returns>
        Task<List<Account>> GetAccounts();

        /// <summary>
        /// Adds the specified User
        /// </summary>
        /// <param name="candidate">The User.</param>
        /// <returns></returns>
        Task Add(Account user);       

       
        /// <summary>
        /// Is UserId Exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsFileNameExists(int id);

    }
}