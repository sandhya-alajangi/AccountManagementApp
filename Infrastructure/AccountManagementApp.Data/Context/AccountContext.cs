using AccountManagementApp.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountManagementApp.Data.Context
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Accounts.
        /// </summary>
        /// <value>
        /// The Users.
        /// </value>
        public DbSet<Account> Accounts { get; set; }

       public DbSet<MeterReaders> MeterReaders { get; set; }
    }
}
