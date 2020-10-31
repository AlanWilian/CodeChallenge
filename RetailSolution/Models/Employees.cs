using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RetailSolution.Models
{
    public class Employees
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }
                
        public ICollection<HoursRegistry> HoursRegistry { get; set; } = new List<HoursRegistry>();

        public Employees()
        {

        }

        public Employees(int id, string name, string email, DateTime birthDate, double baseSalary)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
        }


        public double TotalHours(DateTime initial, DateTime final)
        {
            return HoursRegistry.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Hours);
        }


        public string TotalBreak(DateTime initial, DateTime final)
        {

            var calcBreakNightShift = HoursRegistry
                       .Where(sr => sr.Date >= initial && sr.Date <= final && sr.Shift == true)
                       .Select(x => x.Hours)
                       .Aggregate(0, (x, y) => { return (int)(x + (y >= 4 ? (y * 15) + ((float)Math.Floor(y / 4) * 30) : y * 15) + ((float)Math.Floor(y / 12) * 40)); });

            var calcBreakShift = HoursRegistry
                        .Where(sr => sr.Date >= initial && sr.Date <= final && sr.Shift == false)
                        .Select(x => x.Hours)
                        .Aggregate(0, (x, y) => { return (int)(x + ( y * 10) + ((float)Math.Floor(y / 4) * 20)); });


            var span = TimeSpan.FromMinutes(calcBreakShift + calcBreakNightShift);
            return ((int)span.TotalHours).ToString() + ":" + span.Minutes.ToString();

        }
    }
}