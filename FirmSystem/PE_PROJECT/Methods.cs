using PE_PROJECT.firm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace PE_PROJECT
{
  public static class Methods
    {
        public static bool CheckIfEmpty(IFirm firm,string message)
        {
            int milliseconds = 4000;
            Console.WriteLine("proccessing");
            Thread.Sleep(milliseconds);
            if (firm.Departments.Count == 0)
            {
                Console.WriteLine(message);
               
                
                return false;
            }
          
            return true;
        }
       public static string noDepartments = "There are currently no departments";
        public static string noEmployees = "There are currently no employees in the department";

        public static void Save(StreamWriter writer, string output)
        {
            writer.WriteLine(output);
            writer.Flush();
          
        }

        public static engine.Engine Engine
        {
            get => default(engine.Engine);
            set
            {
            }
        }

        public static CommandExecuter.CommandExecute CommandExecute
        {
            get => default(CommandExecuter.CommandExecute);
            set
            {
            }
        }
    }
   

}

