using Microsoft.EntityFrameworkCore;
using RetailSolution.Data;
using RetailSolution.Interfaces;
using RetailSolution.Models;
using RetailSolution.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSolution.Services
{
    public class EmployeeService
    {
        private readonly RetailContext _context;

        public EmployeeService(RetailContext context)
        {
            _context = context;
        }

        public async Task<List<Employees>> FindAllAsync()
        {
            return await _context.Employees.OrderBy(x => x.Name).ToListAsync();
        }
        

        public async Task InsertAsync(Employees obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }


        public async Task<Employees> FindByIdAsync(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Employees.FindAsync(id);
                _context.Employees.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new Exception("Can't delete employee");
            }

        }

        public async Task UpdateAsync(Employees obj)
        {
            bool hasAny = await _context.Employees.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }

}
