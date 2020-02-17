using Newtonsoft.Json;
using PE_PROJECT.Departments;
using PE_PROJECT.Employees;
using PE_PROJECT.firm;
using PE_PROJECT.MenuFolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PE_PROJECT.CommandExecuter
{
    public class CommandExecute : ICommandExecute
    {
        public Firm Firm
        {
            get => default(Firm);
            set
            {
            }
        }

        public void Execute(IFirm firm,int command,StreamWriter writer,StreamReader reader)
        {
            switch (command)
            {
                case 1:
                    AddEmployee(firm,writer);
                    break;
                case 2:
                    CreateDepartment(firm, writer);
                    break;
                case 3:
                    SearchEmployee(firm, writer);
                    break;
                case 4:
                    SearchForDepartment(firm, writer);

                    break;
                case 5:
                    Console.WriteLine(firm.Info(), writer);

                    break;
                case 6:

                    Console.WriteLine("goodbye");
                    Methods.Save(writer, "goodbye");
                    writer.Close();
                    Environment.Exit(0);
                    break;
                case 7:
                    // string lines;
                    // var list = new List<string>();
                    // while ((lines =reader.ReadLine()) != null)
                    // {
                    //    list.Add(lines);
                    // }
                    // Console.WriteLine(string.Join(',', list));
                    // break;
                    Console.Clear();
                    Menu.PrintMenu();
                    int number = int.Parse(Console.ReadLine());
                    switch (number)
                    {
                        case 1:
                            PrintEmployees(firm);
                            break;
                        case 2:
                            PrintDepartments(firm);
                            break;
                       

                        
                    }
                    break;
               
            }
        }

        public void readFirm()
        {
            var text = File.ReadAllText("firm.json");
            var f = JsonConvert.DeserializeObject<Firm>(text);
            Console.WriteLine(f);
        }

        public void readDepartments()
        {
           
            var text = File.ReadAllText("departments.json");
           var departments = JsonConvert.DeserializeObject<List<Department>>(text);
            foreach(var department in departments)
            {
                Console.WriteLine(department.Name);
            }

            
            // Bad Boys
        }

  

        public void PrintDepartments(IFirm firm)
        {
            List<Department> departments = firm.Departments.ToList();
            StreamWriter writer = new StreamWriter("departments.txt");

            foreach (var emp in departments)
            {
                writer.WriteLine(emp.Name + "," + emp.DateOfCreation);
            }
            if (departments.Count > 0)
            {
                Console.WriteLine("print successful");
            }
            else
            {
                Console.WriteLine("empty");
            }
            writer.Flush();
            writer.Close();
        }

        public void PrintEmployees(IFirm firm)
        {
            List<Employee> employessTaken = firm.Departments.SelectMany(x => x.Employees).ToList();
            StreamWriter writer = new StreamWriter("employees.txt");

            foreach (var emp in employessTaken)
            {
                writer.WriteLine(emp.Name + "," + emp.WorkЕxpirience + "," + emp.WeeklyHorarium + "," + emp.Egn + "," + emp.DepartmentName+ ","+string.Join(",",emp.Projects));
            }
            if (employessTaken.Count > 0)
            {
                Console.WriteLine("print successful");
            }
            else
            {
                Console.WriteLine("empty");
            }
            writer.Flush();
            writer.Close();
            //   string employees = JsonConvert.SerializeObject(employessTaken);
            // StreamWriter writer = new StreamWriter("employees.json");
            //  writer.Write(employees);


        }

        public void SearchEmployee(IFirm firm,StreamWriter writer)
        {
            Console.Clear();
            bool result = Methods.CheckIfEmpty(firm, Methods.noEmployees);
            if (result)
            {
                Console.Write("Employee Name: ");
                Methods.Save(writer, "Employee Name: ");

                string employeeSearch = Console.ReadLine();
                IEmployee activeEmployee = null;
                foreach (var deps in firm.Departments)
                {
                    foreach (var currEmployee in deps.Employees)
                    {
                        if (currEmployee.Name == employeeSearch)
                        {
                            if (activeEmployee == null)
                            {
                                activeEmployee = currEmployee;
                            }
                            break;
                            //Console.WriteLine($"{currEmployee } with egn {currEmployee.Egn} works in department {deps.Name} in {firm.Name}");
                        }
                    }
                }
                if (activeEmployee != null)
                {
                    Console.WriteLine("employee found");
                    Methods.Save(writer, "employee found");
                    this.EmployeeInfo(activeEmployee, firm,writer);
                }
                else
                {
                    List<String> emps = new List<string>();
                    Console.WriteLine("no such employee exists");
                    foreach(var emp in firm.Departments)
                    {
                       foreach(var e in emp.Employees)
                        {
                            emps.Add(e.Name);
                        }
                    }
                    Console.WriteLine("Avalailable employees are "+string.Join(", ",emps));
                    Methods.Save(writer, "no such employee exists");
                 
                   
                }
            }
        }
        public void CreateDepartment(IFirm firm, StreamWriter writer)
        {
            Console.Write("Department Name: ");

            string departmentName = Console.ReadLine();
            Console.Write("Department Creation Date: ");
            string departmentCreation = Console.ReadLine();
            Department department = new Department(departmentName, departmentCreation);
            firm.AddDepartment(department);
        }
        public void SearchForDepartment(IFirm firm, StreamWriter writer)
        {
            Console.Clear();
            bool available = Methods.CheckIfEmpty(firm, Methods.noDepartments);
            if (available)
            {
                Console.WriteLine("Department name: ");
                string searchForDepartment = Console.ReadLine();
                IDepartment departmentResult = firm.Departments.FirstOrDefault(x => x.Name == searchForDepartment);
                if (departmentResult == null)
                {
                    Console.WriteLine("No such department exists");
                    Methods.Save(writer, "No such department exists");
                    Console.WriteLine($"Available departments {firm.ListDepartments()}");
                    Methods.Save(writer, $"Available departments {firm.ListDepartments()}");
                }
                else
                {
                    this.DepartmentInfo(departmentResult, firm, writer);


                }

            }
        }
        public void AddEmployee(IFirm firm, StreamWriter writer)
        {
            bool resultSearch = Methods.CheckIfEmpty(firm, Methods.noDepartments);
            if (resultSearch)
            {
                Console.Clear();
                Console.Write("Employee name: ");
                string name = Console.ReadLine();
                Console.Write("Employee Expirience: ");
                string expirienceP = Console.ReadLine();
                while (!Regex.IsMatch(expirienceP, @"^[0-9]+$"))
                {
                    Console.WriteLine("incorrect input.It must be a number");
                    expirienceP = Console.ReadLine();
                    Console.Clear();
                }
                int expirience = int.Parse(expirienceP);
                Console.Write("Employee weekly horarium: ");
                int horarium = int.Parse(Console.ReadLine());
                Console.Write("Type egn: ");
                string egn = Console.ReadLine();
                Console.Clear();
                Employee employee = new Employee(name, expirience, horarium, egn);
                Console.WriteLine($"Available departments {firm.ListDepartments()}");
                Methods.Save(writer, $"Available departments {firm.ListDepartments()}");
                Console.Write("Choose Department to add employee in: ");
                Methods.Save(writer, $"Available departments {firm.ListDepartments()}");
                string departmentToAddIn = Console.ReadLine();
                IDepartment currentDepartment = firm.Departments.FirstOrDefault(x => x.Name == departmentToAddIn);
                while (currentDepartment == null)
                {
                    Console.WriteLine("No such department please try again");
                    Methods.Save(writer, "No such department please try again");
                    departmentToAddIn = Console.ReadLine();
                    currentDepartment = firm.Departments.FirstOrDefault(x => x.Name == departmentToAddIn);
                };
                currentDepartment.AddEmployee(employee);
                employee.DepartmentName = currentDepartment.Name;
                Console.WriteLine($"Added employee to Department {currentDepartment.Name}");
                Methods.Save(writer, $"Added employee to Department {currentDepartment.Name}");
            }
        }
            public void EmployeeInfo(IEmployee employee,IFirm firm, StreamWriter writer)
        {
            Console.Write("To enter menu press Yes. ");
            string command = Console.ReadLine();
            while (command != "No")
            {
                if (command == "Yes")
                {
                    Menu.EmployeeInfo();
                   string param = Console.ReadLine();
                    int commandOperator;
                    while (!int.TryParse(param, out commandOperator))
                    {
                        Console.WriteLine("Please enter a number between 1 and 6 to access menu options.");
                        param= Console.ReadLine();
                    }
                    switch (commandOperator)
                    {
                        case 1:
                            Console.WriteLine(employee.Name);
                            Methods.Save(writer, employee.Name);
                            break;
                        case 2:
                            Console.WriteLine(employee.Egn);
                            Methods.Save(writer, employee.Egn);
                            break;
                        case 3:
                            if (employee.Projects.Count == 0)
                            {
                                Console.WriteLine("employee has no projects currently");
                            }
                            else
                            {
                                Console.WriteLine(string.Join(" ", employee.Projects));
                                Methods.Save(writer, string.Join(" ", employee.Projects));
                            }
                            break;
                        case 4:
                            Console.WriteLine("add project");
                            string project = Console.ReadLine();
                            employee.AddProject(project);
                            break;
                        case 5:

                            Console.WriteLine("Employee Name");
                            string employeeSearch = Console.ReadLine();
                            foreach (var deps in firm.Departments)
                            {
                                foreach (var currEmployee in deps.Employees)
                                {
                                    if (currEmployee.Name == employeeSearch)
                                    {
                                        Methods.Save(writer, $"{employee.Name} with egn {employee.Egn} works in department {deps.Name} in {firm.Name}");
                                        Console.WriteLine($"{currEmployee.Name} with egn {currEmployee.Egn} works in department {deps.Name} in {firm.Name}");
                                        break;
                                    }
                                }
                            }
                            break;
                        case 6:
                            writer.Close();
                            Console.WriteLine("goodbye");
                            writer.Close();
                            Environment.Exit(0);

                            break;
                        

                            
                    
                    }

                }
                else
                {
                    Console.WriteLine("Wrong input.Type Yes to access menu or No to exit");
                }
                Console.Write("Type Yes to access menu or No to exit. ");
                            
                command = Console.ReadLine();
               
                Console.Clear();
            }
        }
        public void DepartmentInfo(IDepartment department, IFirm firm, StreamWriter writer)
        {
            Console.Write("To enter menu press Yes. ");
            string command = Console.ReadLine();
            while (command != "No")
            {
                if (command == "Yes")
                {
                    Menu.DepartmentInfo();
                    string param = Console.ReadLine();
                    int commandOperator;
                    while (!int.TryParse(param, out commandOperator))
                    {
                        Console.WriteLine("Please enter a number between 1 and 6 to access menu options.");
                        param = Console.ReadLine();
                    }
                    switch (commandOperator)
                    {
                        case 1:
                            Console.WriteLine(department.Name);
                            Methods.Save(writer, department.Name);
                            break;
                        case 2:
                            Console.WriteLine(department.DateOfCreation);
                            Methods.Save(writer, department.DateOfCreation);
                            break;
                        case 3:
                            Console.WriteLine(string.Join(" ", department.DepartmentEmployees()));
                            Methods.Save(writer, string.Join(" ", department.DepartmentEmployees()));
                            break;
                      
                        case 5:

                          //  Console.WriteLine("department Name");
                         //   string departmentSearch = Console.ReadLine();
                         //   foreach (var deps in firm.Departments)
                       //     {
                                
                                  //  if (deps.Name == departmentSearch)
                                  //  {
                                        Methods.Save(writer, $"{department.Name} with date of creation {department.DateOfCreation} has {department.Employees.Count} employees.");
                                        Console.WriteLine($"{department.Name} with date of creation {department.DateOfCreation} has {department.Employees.Count} employees.");
                                 //   }
                                
                          //  }
                            break;
                        case 4:
                            writer.Close();
                            Console.WriteLine("goodbye");
                            writer.Close();
                            Environment.Exit(0);

                            break;

                    }
                }
                else
                {
                    Console.WriteLine("Wrong input.Type Yes to access menu or No to exit");
                }
                Console.Write("Type Yes to access menu or No to exit. ");

                command = Console.ReadLine();

                Console.Clear();
            }
        }
    }
    
    }

