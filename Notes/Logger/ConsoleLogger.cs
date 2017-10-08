using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    class ConsoleLogger : ILogger
    {

        public ConsoleLogger()
        {

        }

        public void Print(string s)
        {
            Console.WriteLine(s);
        }

        public void PrintError(String message, Exception ex)
        {
            Print(message);
            var realException = ex;
            while (realException != null)
            {
                Print(realException.Message);
                Print(realException.StackTrace);
                realException = realException.InnerException;
            }
        }
    }
}
