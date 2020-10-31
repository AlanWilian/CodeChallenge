using Microsoft.EntityFrameworkCore;
using RetailSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSolution.Data
{
    public class RetailContext : DbContext
    {
        public RetailContext(DbContextOptions<RetailContext> options)
            : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<HoursRegistry> HoursRegistry { get; set; }
    }
}
