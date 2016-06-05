
using DublicateArrays.Parser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace DublicateArrays.Checker
{
    public class DublicateChecker:IChecker
    {
        public DublicateChecker(ILineParser parser)
        {
            _parser = parser;
        }

        private ILineParser _parser;

        private Dictionary<string, int> _invalidInputs = new Dictionary<string, int>();

        public ReadOnlyDictionary<string, int> InvalidInputs { get { return new ReadOnlyDictionary<string, int>(_invalidInputs); } }

        private Dictionary<string, int> _validInputs = new Dictionary<string, int>();

        public ReadOnlyDictionary<string, int> ValidInputs { get { return new ReadOnlyDictionary<string, int>(_validInputs); } }

        public int DublicateCount { get { return _validInputs.Where(q => q.Value > 1).Count(); } }

        public int NonDublicateCount { get { return _validInputs.Where(q => q.Value == 1).Count(); } }
        
        public AddLineResult AddLine(string inputLine)
        {
            if (!_parser.IsStringValid(inputLine))
            {
                AddLineToDictionary(_invalidInputs, inputLine);
                return AddLineResult.Invalid;
            }

            var sortedInputLine = _parser.ParseString(inputLine);
            
            return AddLineToDictionary(_validInputs, sortedInputLine);
        }

        private AddLineResult AddLineToDictionary(IDictionary<string,int> dictionary, string inputLine)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary can not be null");

            if(dictionary.ContainsKey(inputLine))
            {
                dictionary[inputLine]++;
                return AddLineResult.Dublicate;
            }
            else
            {
                dictionary[inputLine] = 1;
                return AddLineResult.New;
            }
        }
        
        public int[][] GetMostFrequentArrays()
        {
            return _validInputs.OrderByDescending(D => D.Value).Where(q=> q.Value == _validInputs.Max(v=>v.Value)).Select(s => s.Key.Split(',').Select(q => int.Parse(q)).ToArray()).ToArray();
        }

        public void AddLinesFromFile(string path)
        {
            var lines =  File.ReadAllLines(path);
            foreach (var line in lines)
            {
                AddLine(line);
            }
        }
    }
}
