using RetailSolution.Models;
using System;
using System.Linq;

namespace RetailSolution.Data
{
    public class SeedingService
    {
        private RetailContext _context;

        public SeedingService(RetailContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Employees.Any())
            {
                return; 
            }

            Employees s1 = new Employees(1, "Bob Brown", "bob@gmail.com", new DateTime(1998, 4, 21), 1000.0);
            Employees s2 = new Employees(2, "Maria Green", "maria@gmail.com", new DateTime(1979, 12, 31), 3500.0);
            Employees s3 = new Employees(3, "Alex Grey", "alex@gmail.com", new DateTime(1988, 1, 15), 2200.0);
            Employees s4 = new Employees(4, "Martha Red", "martha@gmail.com", new DateTime(1993, 11, 30), 3000.0);
            Employees s5 = new Employees(5, "Donald Blue", "donald@gmail.com", new DateTime(2000, 1, 9), 4000.0);
            Employees s6 = new Employees(6, "Alex Pink", "bob@gmail.com", new DateTime(1997, 3, 4), 3000.0);

            _context.Employees.AddRange(s1, s2, s3, s4, s5, s6);

            _context.SaveChanges();
        }
    }
}

