using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnKrypt.Utilities
{
    public class ErrorHandler
    {
        private const bool Logging = true;

        protected static void Log(Exception e)
        {
            if(Logging)
            {
                Console.WriteLine("Error:");
                Console.WriteLine("Message: {0}", e.Message);
                Console.WriteLine("Stack Trace:");
                Console.WriteLine(e.StackTrace.ToString());
                Console.WriteLine("");
            }
        }
    }
}
