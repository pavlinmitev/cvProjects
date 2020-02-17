using PE_PROJECT.Departments;
using PE_PROJECT.Employees;
using PE_PROJECT.engine;
using PE_PROJECT.firm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PE_PROJECT
{
    class StartUp : Engine
    {
        static void Main(string[] args)
        {
            //  StreamWriter writer = new StreamWriter(@"C:\Users\N10\Desktop\PE_PROJECT_UPDATE1\PE_PROJECT\PE_PROJECT\outputFile.txt");
            string test = @"C:\Users\N10\Desktop\PE_PROJECT_UPDATE1\PE_PROJECT\PE_PROJECT\outputFile.txt";
            File.WriteAllText(test, String.Empty);
            var oStream = new FileStream(test, FileMode.Append, FileAccess.Write, FileShare.Read);
            var iStream = new FileStream(test, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            var sw = new System.IO.StreamWriter(oStream);
            var sr = new System.IO.StreamReader(iStream);
            Console.WriteLine("Lets create the firm");
            Console.WriteLine("Firm Name");

            string name = Console.ReadLine();
            Console.WriteLine("Firm type");
            string type = Console.ReadLine();
            Console.WriteLine("Register with dds");
            Console.WriteLine("Yes/No");
            bool isRegistered = false;
            string registerResult = Console.ReadLine();
            if (registerResult == "Yes")
            {
                isRegistered = true;
            }
            else
            {
                isRegistered = false;
            }
            IFirm firm = new Firm(name, type, isRegistered);
            seedDeps(firm);
            seedEmployees(firm);
            Engine engine = new Engine();
            engine.Execute(firm, sw, sr);
        }
        public static void seedDeps(IFirm firm)
        {
            string path = "departments.txt";
            if (File.Exists(path))
            {
                string line = null;
                StreamReader reader = new StreamReader("departments.txt");
                while ((line = reader.ReadLine()) != null)
                {
                    Department dep = new Department(line.Split(",")[0], line.Split(",")[1]);
                    firm.AddDepartment(dep);
                }
                reader.Close();
            }
        }
        public static void seedEmployees(IFirm firm)
        {
            string path = "employees.txt";
            if (File.Exists(path)&& firm.Departments.Count>0)
            {
                string line = null;
                StreamReader reader = new StreamReader("employees.txt");
                while ((line = reader.ReadLine()) != null)
                {
                    Employee dep = new Employee(line.Split(",")[0], int.Parse(line.Split(",")[1]), int.Parse(line.Split(",")[2]), line.Split(",")[3]);
                    dep.DepartmentName = line.Split(",")[4];
                    string[] splitted = line.Split(",");
                    for (int i = 5; i < splitted.Length; i++)
                    {
                        dep.AddProject(splitted[i]);
                    }
                    firm.Departments.FirstOrDefault(x=>x.Name==dep.DepartmentName).AddEmployee(dep);
                }
                reader.Close();
            }
        }
    }
}
