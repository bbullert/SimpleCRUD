using Microsoft.EntityFrameworkCore;
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

        public async Task CreateAsync(Employee employee)
        {
            await _appDbContext.Employees.AddAsync(employee);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> ReadAsync(int skip = 0, int limit = 20)
        {
            return await _appDbContext.Employees
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.BirthDate)
                .ThenBy(x => x.Email)
                .ThenBy(x => x.PhoneNumber)
                .ThenBy(x => x.Salary)
                .Skip(skip)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _appDbContext.Employees.CountAsync();
        }

        public async Task<Employee> ReadAsync(int id)
        {
            return await _appDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(int id, Employee model)
        {
            var employee = await ReadAsync(id);

            if (employee != null)
            {
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.BirthDate = model.BirthDate;
                employee.Email = model.Email;
                employee.PhoneNumber = model.PhoneNumber;
                employee.Salary = model.Salary;
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await ReadAsync(id);

            if (employee != null)
            {
                _appDbContext.Employees.Remove(employee);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
