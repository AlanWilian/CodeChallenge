using Microsoft.AspNetCore.Mvc;
using RetailSolution.Interfaces;
using RetailSolution.Models;
using RetailSolution.Models.ViewModels;
using RetailSolution.Services.Exceptions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RetailSolution.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployee _employeeRepository;
        
        public EmployeeController(IEmployee employeeRepository)
        {
            _employeeRepository = employeeRepository;
            
        }

        public async Task<IActionResult> Index()
        {
            var list = await _employeeRepository.GetEmployeesAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employees employee)
        {
            if (ModelState.IsValid)
            {
               await _employeeRepository.SaveAllAsync();                
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _employeeRepository.GetEmployeeByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                 _employeeRepository.DeleteById(id);
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

            var obj = await _employeeRepository.GetEmployeeByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employees = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            return View(employees);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employees employee)
        {
             if (!ModelState.IsValid)
            {
                return View(employee);
            }

            try
            {
                _employeeRepository.Update(employee);
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
