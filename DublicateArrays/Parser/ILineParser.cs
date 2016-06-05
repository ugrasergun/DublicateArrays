using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DublicateArrays.Parser
{
    public interface ILineParser
    {
        bool IsStringValid(string inputLine);
        string ParseString(string inputLine);
    }
}
