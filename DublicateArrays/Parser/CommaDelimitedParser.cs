using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DublicateArrays.Parser
{
    public class CommaDelimitedParser : ILineParser
    {
        private Regex isValidRegex = new Regex(@"^\s*(\d+(\s*,\s*\d+)*)?\s*$");

        public bool IsStringValid(string inputLine)
        {
            return (!string.IsNullOrWhiteSpace(inputLine)) && isValidRegex.Match(inputLine).Success;
        }

        public string ParseString(string inputLine)
        {
            return string.Join(",", inputLine.Split(',').AsEnumerable().Select(s => int.Parse(s)).OrderBy(i => i).Select(i => i.ToString()));
        }
    }
}
