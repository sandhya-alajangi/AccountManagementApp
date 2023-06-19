using System.Collections.Generic;
using AccountManagementApp.Model.Models;

namespace AccountManagementApp.Model.Contracts
{
    public interface IFileResult
    {
        /// <summary>
        /// Gets or sets the cashback counter.
        /// </summary>
        /// <value>
        /// The cashback counter.
        /// </value>
        IResultCounter CashbackCounter { get; set; }

        /// <summary>
        /// Gets or sets the group list.
        /// </summary>
        /// <value>
        /// The group list.
        /// </value>
        List<GroupResult> GroupList { get; set; }
    }
}