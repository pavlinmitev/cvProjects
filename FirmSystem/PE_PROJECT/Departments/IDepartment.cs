using PE_PROJECT.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace PE_PROJECT.Departments
{
   public interface IDepartment
    {
        string Name { get; }
        IReadOnlyList<Employee> Employees { get; }
        string DateOfCreation { get; }
        void AddEmployee(Employee employee);
       string DepartmentEmployees();

    }
}
