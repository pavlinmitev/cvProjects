using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PE_PROJECT.Employees
{
   public class Employee : IEmployee
    {
       
        private List<string> projects;
        private string egn;
      
        public String DepartmentName { get; set; }
        public Employee(string name,int workExpirience,int horarirum,string egn)
        {
            this.Name = name;
            this.WorkЕxpirience = workExpirience;
            this.WeeklyHorarium = horarirum;
            this.projects = new List<string>();
            this.Egn = egn;
        }
        public Employee()
        {
            this.projects = new List<string>();
        }
        public string Name { get; private set; }
        [JsonProperty]
        public int WorkЕxpirience  { get; private set; }
        public string Egn { get { return this.egn; } private set
            {
                
while(value.Length!=10)
                {
                    Console.WriteLine("input is not 10 numbers long");
                    value = Console.ReadLine();
                    Console.Clear();
                }
                egn = value;
            }
        }
        [JsonProperty]
        public int WeeklyHorarium  { get; private set; }

        public IReadOnlyList<string> Projects => projects.AsReadOnly();

        public void AddProject(string project)
        {   
            this.projects.Add(project);
        }
        public override string ToString()
        {
            return $"{this.Name} has  {this.projects.Count} with egn {this.egn} and expirience {this.WorkЕxpirience} ";
        }
    }
}
