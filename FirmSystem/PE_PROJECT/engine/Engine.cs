using PE_PROJECT.Employees;
using PE_PROJECT.firm;
using PE_PROJECT.MenuFolder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using PE_PROJECT.Departments;
using System.Linq;
using System.Windows.Input;
using PE_PROJECT.CommandExecuter;
using System.IO;

namespace PE_PROJECT.engine
{
  public class Engine
    {
        public CommandExecute CommandExecute
        {
            get => default(CommandExecute);
            set
            {
            }
        }

        public void Execute(IFirm firm,StreamWriter writer,StreamReader reader)
        {
            Console.WriteLine("Welcome");
            Console.WriteLine("To Access Menu please type Yes");

            string input = Console.ReadLine();
            Console.Clear();


            while (input != "No")
            {

                if (input != "Yes")
                {
                    Console.WriteLine("Wrong Input.");
                    Methods.Save(writer, "Wrong Input.");
                    

                }
                else
                {
                    Menu.Display();
                    string value = Console.ReadLine();
                    int command;
                    while (!int.TryParse(value, out command))
                    {
                        Console.WriteLine("Please enter a number between 1 and 7 to access menu options.");
                       // Methods.Save(writer, "Please enter a number between 1 and 6 to access menu options.");
                        value = Console.ReadLine();
                    }
                    ICommandExecute commandExecuter = new CommandExecute();
                    commandExecuter.Execute(firm, command,writer,reader);
                  

                }
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("To access menu please type Yes");
                Console.WriteLine("To end application please type No");
                input = Console.ReadLine();
                 if (input == "No")
                {
                    writer.Close();
                }
                Console.Clear();
            }
        }
    }
}


