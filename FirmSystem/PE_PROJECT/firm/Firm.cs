using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using PE_PROJECT.Departments;
using PE_PROJECT.Employees;

namespace PE_PROJECT.firm
{
    public class Firm : IFirm
    {
        private string name;
        private string firmType;
        private List<Department> departments;

        public Firm()
        {

        }
        public Firm(string name,string type,bool Dds)
        {
            this.Name = name;
            this.IsFirmWithDDS = Dds;
            this.FirmType = type;
            this.departments = new List<Department>();
        }
        [JsonProperty]
        public string FirmType {
            get { return this.firmType; }
         private   set { while (value != "ООД" && value != "АД" && value != "ЕООД")
                {
                    Console.WriteLine("Wrong Firm Type.Please enter either ООД,AД or ЕООД");
                    value=Console.ReadLine();
                    Console.Clear();

                }
                this.firmType = value;
            }

        }

        public IReadOnlyList<Department> Departments => this.departments.AsReadOnly();
        [JsonProperty]
        public string Name
        {
            get { return this.name; }
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
        [JsonProperty]
        public bool IsFirmWithDDS { get; private set; }

        public Department Department
        {
            get => default(Department);
            set
            {
            }
        }

        private String registrationType(bool dds)
        {
            if (dds == true)
            {
                return "With DDS";
            }
            else
            {
                return "Without DDS";
            }
        }
        public string Info()
        {
            return this.ToString();
        }
        public void AddDepartment(Department department)
        {
            this.departments.Add(department);
        }
        public string ListDepartments()
        {
            List<string> deps = new List<string>();
            foreach(var department in departments)
            {
                deps.Add(department.Name);
            }
            return string.Join(" , ", deps);

        }
        public override string ToString()
        {
            return $"Firm {this.Name} {this.FirmType} is {this.registrationType(this.IsFirmWithDDS)}";
        }
    }
    
}
