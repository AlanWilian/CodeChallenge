using Microsoft.AspNetCore.Mvc;
using RetailSolution.Models;
using RetailSolution.Models.ViewModels;
using RetailSolution.Services;
using RetailSolution.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RetailSolution.Controllers
{
    public class HoursRegistryController : Controller
    {
        private readonly HoursRegistryService _HoursRegistry;
        private readonly EmployeeService _EmployeeService;

        public HoursRegistryController(HoursRegistryService hoursRegistryService, EmployeeService employeeService)
        {
            _HoursRegistry = hoursRegistryService;
            _EmployeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _HoursRegistry.FindAllAsync(); 
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var employee = await _EmployeeService.FindAllAsync();
            var viewModel = new HoursRegistryFormViewModel { Employees = employee };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HoursRegistry hoursRegistry)
        {
            if (!ModelState.IsValid)
            {               
                var employee = await _EmployeeService.FindAllAsync();
                var viewModel = new HoursRegistryFormViewModel { Employees = employee, HoursRegistry = hoursRegistry };
                return View(viewModel);
            }

            var findDate = await _HoursRegistry.FindByDateAsync(hoursRegistry.Date, hoursRegistry.EmployeesId);
            if (findDate != null)
            {
                return RedirectToAction(nameof(Create), new { message = "Data ja cadastrada" });
            }

            hoursRegistry.GetType().GetProperty(hoursRegistry.Date.DayOfWeek.ToString()).SetValue(hoursRegistry, hoursRegistry.Hours);
            
            await _HoursRegistry.InsertAsync(hoursRegistry);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _HoursRegistry.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _HoursRegistry.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _HoursRegistry.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _HoursRegistry.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Employees> employees = await _EmployeeService.FindAllAsync();
            HoursRegistryFormViewModel viewModel = new HoursRegistryFormViewModel { HoursRegistry = obj , Employees = employees };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HoursRegistry hoursRegistry)
        {
            if (!ModelState.IsValid)
            {
                var allEmployee = await _EmployeeService.FindAllAsync();
                var viewModel = new HoursRegistryFormViewModel { HoursRegistry = hoursRegistry, Employees = allEmployee };
                return View(viewModel);
            }
            if (id != hoursRegistry.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _HoursRegistry.UpdateAsync(hoursRegistry);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
