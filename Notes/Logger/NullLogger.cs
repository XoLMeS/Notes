using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    class NullLogger : ILogger
    {

        public  NullLogger()
        {

        }

        public void Print(string s)
        {
           
        }

        public void PrintError(String msg, Exception e)
        {

        }
    }
}
