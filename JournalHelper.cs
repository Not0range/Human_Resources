using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources
{
    internal static class JournalHelper
    {
        public static void Write(string msg)
        {
            File.AppendAllText("journal.txt", "[" + DateTime.Now.ToString() + "] " + msg + Environment.NewLine);
        }
    }
}
