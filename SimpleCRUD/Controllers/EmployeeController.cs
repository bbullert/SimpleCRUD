using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using SimpleCRUD.Models;
using SimpleCRUD.Helpers;
using SimpleCRUD.Infrastructure.Repositories;

namespace SimpleCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly EmployeeConverter _employeeConverter;

        public EmployeeController(EmployeeRepository employeeRepository,
            EmployeeConverter employeeConverter)
        {
            _employeeRepository = employeeRepository;
            _employeeConverter = employeeConverter;
        }

        private async Task DataSetAsync()
        {
            if (await _employeeRepository.CountAsync() == 0)
            {
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var file = System.IO.File.ReadAllText("dataSet.json");
                var models = System.Text.Json.JsonSerializer.Deserialize<List<EmployeeViewModel>>(file, options);

                foreach (var model in models)
                {
                    var employee = _employeeConverter.Convert(model);
                    await _employeeRepository.CreateAsync(employee);
                }
            }
        }

        [HttpGet("[controller]/{page:min(1)=1}")]
        public async Task<IActionResult> IndexAsync(int page)
        {
            await DataSetAsync();

            int limit = 20;
            int skip = Math.Max((page - 1) * limit, 0);
            var employees = await _employeeRepository.ReadAsync(skip, limit);
            int count = await _employeeRepository.CountAsync();
            var list = new List<EmployeeViewModel>();

            employees.ToList().ForEach(employee => list.Add(_employeeConverter.Convert(employee)));

            var model = new EmployeePaginationViewModel(list, page, limit, count, 9);

            return View(model);
        }

        public async Task<IActionResult> DetailsAsync(int id)
        {
            var employee = await _employeeRepository.ReadAsync(id);
            EmployeeViewModel model = new EmployeeViewModel();

            if (employee != null)
            {
                model = _employeeConverter.Convert(employee);
            }

            return View(model);
        }

        public IActionResult Create()
        {
            return View(new EmployeeViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeConverter.Convert(model);
                await _employeeRepository.CreateAsync(employee);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            var employee = await _employeeRepository.ReadAsync(id);
            EmployeeViewModel model = new EmployeeViewModel();

            if (employee != null)
            {
                model = _employeeConverter.Convert(employee);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeConverter.Convert(model);
                await _employeeRepository.UpdateAsync(employee.Id, employee);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        public IActionResult VerifyPhoneNumber(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return Json("Phone number is required");
            }
            else if (!Regex.IsMatch(phoneNumber, @"^[0-9\(\)\.\+\- ]*$"))
            {
                return Json("Invalid phone number");
            }

            string obtainedDigits = new string(phoneNumber.Where(c => char.IsDigit(c)).ToArray());

            if (obtainedDigits.Length != 11)
            {
                return Json("Only 11 digits long phone number allowed");
            }

            return Json(true);
        }

        public IActionResult VerifySalary(string salary)
        {
            decimal result;

            if (!decimal.TryParse(salary, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                return Json("Salary must be a number.");
            }
            else if (result.DecimalPartCount() > 2)
            {
                return Json("Invalid number value.");
            }
            else if (result < 0 || result >= 100000000)
            {
                return Json("Salary must be in range from 0 to 99999999.99");
            }

            return Json(true);
        }
    }
}
