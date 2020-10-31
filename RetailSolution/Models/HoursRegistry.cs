using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSolution.Models
{
    public class HoursRegistry
    {
        public int Id { get; set; }

        public Employees Employees { get; set; }

        [Display(Name = "Employee")]
        public int EmployeesId { get; set; }


        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Hours Worked")]
        public float Hours { get; set; }

        [Display(Name = "Night Shift")]
        public bool Shift { get; set; }

        public float Sunday { get; set; }

        public float Monday { get; set; }

        public float Tuesday { get; set; }

        public float Wednesday { get; set; }

        public float Thursday { get; set; }

        public float Friday { get; set; }

        public float Saturday { get; set; }

        public HoursRegistry()
        {

        }

        public HoursRegistry(int id, Employees employees, DateTime date, float hours, bool shift, int sunday, int monday, int tuesday, int wednesday, int thursday, int friday, int saturday) 
        {
            Id = id;
            Employees = employees;
            Date = date;
            Hours = hours;
            Shift = shift;
            Sunday = sunday;
            Monday = monday;
            Tuesday = tuesday;
            Wednesday = wednesday;
            Thursday = thursday;
            Friday = friday;
            Saturday = saturday;
        }
    }
}
