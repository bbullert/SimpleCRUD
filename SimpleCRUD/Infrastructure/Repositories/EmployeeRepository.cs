using SimpleCRUD.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCRUD.Infrastructure.Repositories
{
    public class EmployeeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Create(Employee employee)
        {
            _appDbContext.Employees.Add(employee);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Employee> Read()
        {
            return _appDbContext.Employees.ToList();
        }

        public Employee Read(int id)
        {
            return _appDbContext.Employees.FirstOrDefault(x => x.Id == id);
        }

        public void Update(int id, Employee model)
        {
            var employee = Read(id);

            if (employee != null)
            {
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.BirthDate = model.BirthDate;
                employee.Email = model.Email;
                employee.PhoneNumber = model.PhoneNumber;
                employee.Salary = model.Salary;
                _appDbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var employee = Read(id);

            if (employee != null)
            {
                _appDbContext.Employees.Remove(employee);
                _appDbContext.SaveChanges();
            }
        }
    }
}
