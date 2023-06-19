using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AccountManagementApp.Model.Models
{
    public class MeterReaders
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public int AccountId { get; set; }

        public DateTime MeterReadingDateTime { get; set; }

        public int MeterReadValue { get; set; }
    }
}
