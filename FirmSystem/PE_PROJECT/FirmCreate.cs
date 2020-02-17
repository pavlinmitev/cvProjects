using PE_PROJECT.firm;
using System;
using System.Collections.Generic;
using System.Text;

namespace PE_PROJECT
{
   public static class FirmCreate
    {

        public static IFirm CreateFirm()
        {
            Console.WriteLine("Lets create a firm");
            Console.WriteLine("Type Firm Name");
            string firmName = Console.ReadLine();
            Console.WriteLine("Type Firm type");
            string firmType = Console.ReadLine();
            
             return new Firm(firmName, firmType, true);
        }
    }
}
