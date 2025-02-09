﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CSharpLang71
{
    public class PatternMatchBehaviour
    {
        [Test]
        public void GenericTypeParametersAreSupportedIn71ForPatternMatching()
        {
            int[] values = { 2, 4, 6, 8, 10 };
            ShowCollectionInformation(values);

            var names = new List<string>();
            names.AddRange(new string[] { "Adam", "Abigail", "Bertrand", "Bridgette" });
            ShowCollectionInformation(names);

            List<int> numbers = null;
            ShowCollectionInformation(numbers);
        }

        // Performance benefit of not boxing
        private static void ShowCollectionInformation<T>(T coll)
        {
            switch (coll)
            {
                case Array arr:
                    Console.WriteLine($"An array with {arr.Length} elements.");
                    break;
                case IEnumerable<int> ieInt:
                    Console.WriteLine($"Average: {ieInt.Average(s => s)}");
                    break;
                case IList list:
                    Console.WriteLine($"{list.Count} items");
                    break;
                case IEnumerable ie:
                    string result = "";
                    foreach (var e in ie)
                        result += "${e} ";
                    Console.WriteLine(result);
                    break;
                //case null: // Not possible with where T : class on the parameter
                // Do nothing for a null.
                //break;
                default:
                    Console.WriteLine($"A instance of type {coll.GetType().Name}");
                    break;
            }
        }
    }
}
