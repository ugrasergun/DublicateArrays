using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DublicateArrays;
using DublicateArrays.Parser;
using DublicateArrays.Checker;

namespace DublicateArraysTest
{
    [TestClass]
    public class DublicateArrayTest
    {
        [TestMethod]
        public void IsStringValid()
        {
            ILineParser parser = new CommaDelimitedParser();
            string valid1 = "1,2,3,4";
            string valid2 = "1";

            Assert.IsTrue(parser.IsStringValid(valid1));
            Assert.IsTrue(parser.IsStringValid(valid2));

        }

        [TestMethod]
        public void IsStringInvalid()
        {
            ILineParser parser = new CommaDelimitedParser();
            string[] invalids = new string[] {"a,b,c,d",
             "1,2,3,4,",
             "",
             ",,,",
             "1,2,3,a,4",
             "12 312 212",
             null
            };


            foreach (string invalid in invalids)
            {
                Assert.IsFalse(parser.IsStringValid(invalid));
            }
        }

        [TestMethod]
        public void AddLine()
        {
            IChecker checker = new DublicateChecker(new CommaDelimitedParser());

            Assert.AreEqual(checker.AddLine("1,2,3,4,5"), AddLineResult.New);
            Assert.AreEqual(checker.AddLine("1,2,3,4,"), AddLineResult.Invalid);
            Assert.AreEqual(checker.AddLine("1,2,3"),AddLineResult.New);
            Assert.AreEqual(checker.AddLine("1,2,3,4,5"), AddLineResult.Dublicate);
            Assert.AreEqual(checker.AddLine("2,1,3"), AddLineResult.Dublicate);
            Assert.AreEqual(checker.InvalidInputs.Count, 1);
        }

        [TestMethod]
        public void CheckDublicates()
        {
            IChecker checker = new DublicateChecker(new CommaDelimitedParser());

            checker.AddLine("1,2,3,4,5");
            checker.AddLine("1,2,3,4");
            checker.AddLine("1,2,3");
            checker.AddLine("1,2,3,4,5");
            checker.AddLine("2,1,3");

            Assert.AreEqual(checker.DublicateCount, 2);
            Assert.AreEqual(checker.NonDublicateCount, 1);
        }

        [TestMethod]
        public void GetMostFrequentArray()
        {
            IChecker checker = new DublicateChecker(new CommaDelimitedParser());

            checker.AddLine("1,2,3,4,5");
            checker.AddLine("1,2,3,4");
            checker.AddLine("1,2,3");
            checker.AddLine("1,2,3,4,5");
            checker.AddLine("2,1,3");
            checker.AddLine("3,1,2");

            var mostFrequentArray = checker.GetMostFrequentArrays();

            Assert.AreEqual(mostFrequentArray.Length, 1);
            Assert.AreEqual(mostFrequentArray[0].Length, 3);
            Assert.AreEqual(mostFrequentArray[0][0], 1);
            Assert.AreEqual(mostFrequentArray[0][1], 2);
            Assert.AreEqual(mostFrequentArray[0][2], 3);

            checker.AddLine("1,2,3,4,5");
            mostFrequentArray = checker.GetMostFrequentArrays();
            Assert.AreEqual(mostFrequentArray.Length, 2);
        }
        [TestMethod]
        public void GetInvalidReport()
        {
            IChecker checker = new DublicateChecker(new CommaDelimitedParser());

            checker.AddLine("1,2,3,4,5,");
            checker.AddLine("");
            checker.AddLine("1,2,a");
            checker.AddLine("invalid");
            checker.AddLine(",,,,");
            checker.AddLine("1,2,a");

            Assert.AreEqual(checker.InvalidInputs.Count, 5);
            Assert.IsTrue(checker.InvalidInputs.ContainsKey(""));
            Assert.AreEqual(checker.InvalidInputs["1,2,a"], 2);
        }

        [TestMethod]
        public void AddLinesFromFile()
        {
            string path = "input.txt";
            IChecker checker = new DublicateChecker(new CommaDelimitedParser());
            checker.AddLinesFromFile(path);
            Assert.AreEqual(checker.ValidInputs.Count, 26);
            Assert.AreEqual(checker.InvalidInputs.Count, 6);
        }
    }
}
