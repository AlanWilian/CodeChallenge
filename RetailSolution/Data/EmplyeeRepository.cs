using Microsoft.EntityFrameworkCore;
using RetailSolution.Interfaces;
using RetailSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace RetailSolution.Data
{
    public class EmplyeeRepository : IEmployee
    {

        private readonly RetailContext _context;
        public EmplyeeRepository(RetailContext context)
        {
            _context = context;
        }

        public void DeleteById(int id)
        {
            var itemToRemove =  _context.Employees.SingleOrDefault(x => x.Id == id);
            if (itemToRemove != null)
            {
                _context.Remove(itemToRemove);
                _context.SaveChanges();
            }
        }

        public async Task<Employees> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employees> GetEmployeeByUserNameAsync(string userName)
        {
            return await _context.Employees.SingleOrDefaultAsync(x => x.Name == userName);
        }

        public async Task<IEnumerable<Employees>> GetEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Employees employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
