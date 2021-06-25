using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace subtitles
{
    class Program
    {
        private static int inSecond;
        static void Main(string[] args)
        {
            string[] file = File.ReadAllLines("input.txt");
            List<Subtitle> subtitles = ReadFile(file);
            
            while (subtitles.Count != 0)
            {
                List<Subtitle> temp1 = new List<Subtitle>();
                List<Subtitle> temp2 = new List<Subtitle>();
                foreach (var element in subtitles) 
                {
                    if (inSecond >= element.Start && inSecond <= element.End) 
                    {
                        temp2.Add(element);
                    }
                    else if (inSecond > element.End) 
                    {
                        temp1.Add(element);
                    }
                }
                Console.Clear();
                foreach (var element in temp2) 
                {
                    Drawer.CreatePosition(element.Position, element.Text);
                    Drawer.CreateColor(element.Color);
                    Console.WriteLine(element.Text);
                }
                Thread.Sleep(1000);
                subtitles.RemoveAll(elem => temp1.Contains(elem));
                inSecond++;
                foreach (var element in temp2) 
                {
                    Console.WriteLine(element.Text);
                }
            }
        }

        private static List<Subtitle> ReadFile(string[] file) {
            var list = new List<Subtitle>();
            for (int i = 0; i < file.Length; i++) {
                string[] str = file[i].Split(' ');

                int start = int.Parse(str[0].Remove(str[0].IndexOf('0'), 4));
                int end = int.Parse(str[2].Remove(str[2].IndexOf('0'), 4));
                string position = string.Empty;
                string color = string.Empty;
                string text = string.Empty;
                if (str[3].Contains('[')) 
                {
                    string str1 = str[3].Remove(str[3].IndexOf('['), 1);
                    position = str1.Remove(str1.IndexOf(','), 1);
                    color = str[4].Remove(str[4].IndexOf(']'), 1);
                    for (int j = 5; j < str.Length; j++) 
                    {
                        text += str[j] + " ";
                    }
                }
                else 
                {
                    for (int j = 3; j < str.Length; j++) 
                    {
                        text += str[j] + " ";
                    }
                }
                list.Add(new Subtitle(start, end, position, color, text));
            }
            return list;
        }
    }

    class Subtitle
    {
        public int Start { get; }
        public int End { get; }
        public int Time { get; }
        public string Position { get; }
        public string Color { get; }
        public string Text { get; }
        
        public Subtitle(int start, int end, string position, string color, string text) {
            Start = start;
            End = end;
            Time = End - Start;
            Position = position;
            Color = color;
            Text = text;
        }
    }

    static class Drawer
    {
        public static void CreatePosition(string position, string text) {
            if (position != string.Empty) 
            {
                switch (position) 
                {
                    case "Top" :
                        Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, 0);
                        break;
                    case "Bottom" :
                        Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Console.WindowHeight);
                        break;
                    case "Right" :
                        Console.SetCursorPosition(Console.WindowWidth - text.Length, Console.WindowHeight / 2);
                        break;
                    case "Left" :
                        Console.SetCursorPosition(text.Length, Console.WindowHeight / 2 );
                        break;
                }
            }
            else 
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Console.WindowHeight / 2);
            }
        }
        public static void CreateColor(string color) {
            if (color != string.Empty) 
            {
                switch (color) 
                {
                    case "Red" :
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case "Green" :
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case "Blue" :
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                }
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static void DrawFrame()
        {
        }
    }
}