using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using PE_PROJECT.Employees;

namespace PE_PROJECT.Departments
{
    public class Department : IDepartment
    {
         public Department()
        {

        }
        public Department(string name,string date)
        {
            this.Name = name;
            
            this.employees = new List<Employee>();
            if (date == null)
            {
                date = "4/5/2018";
            }
            this.DateOfCreation = date;
        }
        private string name;
        private List<Employee> employees;
        private string dateOfCreation;
        public string Name
        {
            get { return this.name;}
          private  set 
            {
                if (value.Length < 2 || !Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                {
                    while (value.Length < 2 || !Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                    {
                        Console.WriteLine("Wrong input.Firm name Must contain only letters");
                        Console.WriteLine("Please enter Name with letters only");
                        value = Console.ReadLine();
                        Console.Clear();
                    }
                }
                name = value;

            }
        }

        public IReadOnlyList<Employee> Employees => this.employees.AsReadOnly();

        public string DateOfCreation
        {
            get { return this.dateOfCreation; }
           private set
            {
               while (value.Length < 8)
                {
                    Console.WriteLine("make sure you enter a valid date");
                    value = Console.ReadLine();
                    Console.Clear();
                };
                this.dateOfCreation = value;
            }
        }

        internal Employee Employee
        {
            get => default(Employee);
         private   set
            {
            }
        }

        public void AddEmployee(Employee employee)
        {
            this.employees.Add(employee);
            employee.DepartmentName = this.Name;
        }
        public string DepartmentEmployees()
        {
            List<string> employees = new List<string>();
            foreach(var employee in this.employees)
            {
                employees.Add(employee.Name);
            }
            return string.Join(" ", employees);
        }
        public override string ToString()
        {
            return $"{this.Name} was created on {this.DateOfCreation} and currently has {this.Employees.Count} employees";
        }
    }
}
