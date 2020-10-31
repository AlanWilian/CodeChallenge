using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSolution.Models.ViewModels
{
    public class HoursRegistryFormViewModel
    {

        public HoursRegistry HoursRegistry { get; set; }
        public ICollection<Employees> Employees { get; set; }
    }
}
