using PE_PROJECT.Departments;
using System;
using System.Collections.Generic;
using System.Text;

namespace PE_PROJECT.firm
{
   public interface IFirm
    {
        bool IsFirmWithDDS { get; }
        string Name { get; }
        string FirmType { get; }
        IReadOnlyList<Department> Departments { get; }
       string Info();
        void AddDepartment(Department department);
        string ListDepartments();


    }
}
