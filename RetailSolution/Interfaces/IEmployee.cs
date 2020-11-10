using RetailSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSolution.Interfaces
{
    public interface IEmployee
    {
        void Update(Employees employee);

        Task<bool> SaveAllAsync();

        Task<IEnumerable<Employees>> GetEmployeesAsync();

        Task<Employees> GetEmployeeByIdAsync(int id);

       void DeleteById(int id);


    }
}
