using System;

namespace AccountManagementApp.Model.Contracts
{
    public interface IFileRow
    {
        int Id { get; set; }

         DateTime MeterReadingDateTime { get; set; }

        int MeterReadValue { get; set; }

    }
}