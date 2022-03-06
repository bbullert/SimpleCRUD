using SimpleCRUD.Infrastructure.Entities;
using SimpleCRUD.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleCRUD.Helpers
{
    public class EmployeeConverter
    {
        public Employee Convert(EmployeeViewModel model)
        {
            return new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                Email = model.Email,
                PhoneNumber = Regex.Replace(model.PhoneNumber, "[^0-9]", string.Empty),
                Salary = decimal.Parse(model.Salary.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture)
            };
        }

        public EmployeeViewModel Convert(Employee model)
        {
            return new EmployeeViewModel
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                Email = model.Email,
                PhoneNumber = Regex.Replace(model.PhoneNumber, @"(\d{2})(\d{3})(\d{3})(\d{3})", @"+$1 $2 $3 $4"),
                Salary = model.Salary.ToString().Replace(",", ".")
            };
        }
    }
}
