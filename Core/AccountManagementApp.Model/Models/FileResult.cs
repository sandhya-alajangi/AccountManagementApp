using System.Collections.Generic;
using AccountManagementApp.Model.Contracts;

namespace AccountManagementApp.Model.Models
{
    public class FileResult : IFileResult
    {
        /// <summary>
        /// Gets or sets the cashback counter.
        /// </summary>
        /// <value>
        /// The cashback counter.
        /// </value>
        public IResultCounter CashbackCounter { get; set; }

        /// <summary>
        /// Gets or sets the group list.
        /// </summary>
        /// <value>
        /// The group list.
        /// </value>
        public List<GroupResult> GroupList { get; set; }
    }
}