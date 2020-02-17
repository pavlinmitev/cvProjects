using System;
using System.Collections.Generic;
using System.Text;

namespace PE_PROJECT.Employees
{
  public  interface IEmployee
    {
        string Name { get; }
        int WorkЕxpirience { get; }
        int WeeklyHorarium { get; }
        IReadOnlyList<string> Projects { get; }
        string Egn { get; }
        void AddProject(string project);
        string DepartmentName { get; set; }


    }
}
