using Microsoft.EntityFrameworkCore;
using RetailSolution.Data;
using RetailSolution.Models;
using RetailSolution.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSolution.Services
{
    public class HoursRegistryService
    {

        private readonly RetailContext _context;

        public HoursRegistryService(RetailContext context)
        {
            _context = context;
        }

        public async Task<List<HoursRegistry>> FindAllAsync()
        {
            return await _context.HoursRegistry
                .Include(obj => obj.Employees)
                .ToListAsync();

        }

        public async Task InsertAsync(HoursRegistry obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }


        public async Task<HoursRegistry> FindByIdAsync(int id)
        {
            return await _context.HoursRegistry
                .Include(obj => obj.Employees)
                .FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task<HoursRegistry> FindByDateAsync(DateTime date, int id)
        {
            return await _context.HoursRegistry
                .Include(obj => obj.Employees)
                .Where(x => x.Employees.Id == id)
                .FirstOrDefaultAsync(obj => obj.Date == date);
        }
            
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.HoursRegistry.FindAsync(id);
                _context.HoursRegistry.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Can't delete Hours Registry");
            }

        }

        public async Task UpdateAsync(HoursRegistry obj)
        {
            bool hasAny = await _context.HoursRegistry.AnyAsync(x => x.Id == obj.Id);
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
