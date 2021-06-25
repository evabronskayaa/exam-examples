using System;
using System.Collections.Generic;
using System.IO;

namespace interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] file = File.ReadAllLines("commands.txt");
            Dictionary<string, double> dictionary = new Dictionary<string, double>();
            dictionary = ExecuteCommands(file, dictionary);
            foreach (var value in dictionary) {
                Console.WriteLine($"{value.Key} = {value.Value}");
            }
        }

        private static Dictionary<string, double> ExecuteCommands(string[] file, Dictionary<string, double> dictionary)
        {
            for (int i = 0; i < file.Length; i++) {
                string[] str = file[i].Split(' ');
                str[1] = str[1].Replace(",", "");

                if (str[0] == "var") {
                    dictionary.Add(str[1], 0);
                }else if (str[0] == "mov") {
                    if (dictionary.ContainsKey(str[2])){
                        dictionary[str[1]] = dictionary[str[2]];
                    }else {
                        dictionary[str[1]] = double.Parse(str[2]);
                    }
                }else if (file[i].StartsWith("add")) {
                    if (dictionary.ContainsKey(str[2])) {
                        dictionary[str[1]] += dictionary[str[2]];
                    }else {
                        dictionary[str[1]] += double.Parse(str[2]);
                    }
                }else if (file[i].StartsWith("sub")) {
                    if (dictionary.ContainsKey(str[2])) {
                        dictionary[str[1]] -= dictionary[str[2]];
                    }else {
                        dictionary[str[1]] -= double.Parse(str[2]);
                    }
                }else if (file[i].StartsWith("mul")) {
                    if (dictionary.ContainsKey(str[2])) {
                        dictionary[str[1]] *= dictionary[str[2]];
                    }else {
                        dictionary[str[1]] *= double.Parse(str[2]);
                    }
                }else {
                    if (dictionary.ContainsKey(str[2])) {
                        dictionary[str[1]] /= dictionary[str[2]];
                    }else {
                        dictionary[str[1]] /= double.Parse(str[2]);
                    }
                }
            }
            return dictionary;
        }
    }
}