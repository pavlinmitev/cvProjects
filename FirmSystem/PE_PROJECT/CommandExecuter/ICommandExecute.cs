using PE_PROJECT.firm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PE_PROJECT.CommandExecuter
{
   public interface ICommandExecute
    {
        void Execute(IFirm firm,int command,StreamWriter writer,StreamReader reader);
    }
}
