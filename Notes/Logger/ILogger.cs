using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    interface ILogger
    {
        void Print(string s);
        void PrintError(String msg, Exception e);
    }
}
