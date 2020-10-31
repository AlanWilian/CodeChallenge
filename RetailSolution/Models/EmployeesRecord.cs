using System;
using System.ComponentModel.DataAnnotations;

namespace RetailSolution.Models
{
    public class EmployeesRecord
    {

        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        public EmployeesRecord()
        {
        }

        public EmployeesRecord(int id, DateTime date, double amount, Employees seller)
        {
            Id = id;
            Date = date;
        }
    }
}

