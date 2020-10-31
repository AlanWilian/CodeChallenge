using Microsoft.EntityFrameworkCore;
using RetailSolution.Data;
using RetailSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSolution.Services
{
    public class EmployeesRecordService
    {
        private readonly RetailContext _context;

        public EmployeesRecordService(RetailContext context)
        {
            _context = context;
        }

        public async Task<List<IGrouping<Employees, HoursRegistry>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.HoursRegistry select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Employees)
                .OrderByDescending(x => x.Employees.Name)
                .GroupBy(x => x.Employees)
                .ToListAsync();
        }
    }
}
