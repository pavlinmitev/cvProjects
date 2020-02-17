using System;
using System.Collections.Generic;
using System.Text;

namespace PE_PROJECT.MenuFolder
{
   public static class Menu
    {
        public static CommandExecuter.CommandExecute CommandExecute
        {
            get => default(CommandExecuter.CommandExecute);
            set
            {
            }
        }

        public static void Display()
        {

            Console.WriteLine("Menu");
            Console.WriteLine("1.Add Employee");
            Console.WriteLine("2.Add Department");
            Console.WriteLine("3.Search Employee");
            Console.WriteLine("4.Search Department");
            Console.WriteLine("5.Firm Info");          
            Console.WriteLine("6.Exit Menu");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("7.Print Information");
        }
        public static void EmployeeInfo()
        {

            Console.WriteLine("Menu");
            Console.WriteLine("1.Employee Name");
            Console.WriteLine("2.Employee egg");
            Console.WriteLine("3.Employee projects");
            Console.WriteLine("4.Add project");
            Console.WriteLine("5.Full info");
            Console.WriteLine("6.Exit Menu");

        }
        public static void PrintMenu()
        {

            Console.WriteLine("Menu");
            Console.WriteLine("1.Print Employees");
            Console.WriteLine("2.Print Departments");
           
            

        }
        
        public static void DepartmentInfo()
        {

            Console.WriteLine("Menu");
            Console.WriteLine("1.Department Name");
            Console.WriteLine("2.Department creation date");
            Console.WriteLine("3.Department employees");
            Console.WriteLine("4.Exit");
            Console.WriteLine("5.Full info");          
        }
    }
}
