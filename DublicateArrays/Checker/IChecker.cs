using DublicateArrays.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DublicateArrays.Checker
{
    public interface IChecker
    {
        
        ReadOnlyDictionary<string, int> InvalidInputs { get; }
        ReadOnlyDictionary<string, int> ValidInputs { get; }
        int DublicateCount { get; }
        int NonDublicateCount { get; }

        AddLineResult AddLine(string inputLine);
        int[][] GetMostFrequentArrays();
        void AddLinesFromFile(string path);
    }
}
