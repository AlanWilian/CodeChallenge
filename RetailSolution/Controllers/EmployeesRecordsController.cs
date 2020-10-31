using Microsoft.AspNetCore.Mvc;
using RetailSolution.Services;
using System;
using System.Threading.Tasks;

namespace RetailSolution.Controllers
{
    public class EmployeesRecordsController : Controller
    {
        private readonly EmployeesRecordService _recordService;

        public EmployeesRecordsController(EmployeesRecordService recordService)
        {
            _recordService = recordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _recordService.FindByDateGroupingAsync(minDate, maxDate);
            return View(result);
        }
    }
}
