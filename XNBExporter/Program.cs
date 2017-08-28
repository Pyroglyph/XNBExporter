using System;
using System.IO;
using System.Linq;
using XNBLib;

namespace XNBExporter
{
    public static class Program
    {
        private static Exporter exporter;

        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
                _                                  _            
    __  ___ __ | |__     _____  ___ __   ___  _ __| |_ ___ _ __ 
    \ \/ / '_ \| '_ \   / _ \ \/ / '_ \ / _ \| '__| __/ _ \ '__|
     >  <| | | | |_) | |  __/>  <| |_) | (_) | |  | ||  __/ |   
    /_/\_\_| |_|_.__/   \___/_/\_\ .__/ \___/|_|   \__\___|_|   
                                 |_|                 Release 1.0
            ");
            Console.ForegroundColor = ConsoleColor.Gray;

            WriteLine("Note: This version supports only supports audio and\nimages. More formats are planned for later versions.\n");

            if (args.Length == 1)
            {
                // We need to determine whether the user has passed in singular file or a directory.
                if (Directory.Exists(args[0]))
                {
                    var allFiles = Directory.EnumerateFiles(args[0]).ToList();
                    var xnbFiles = allFiles.Where(f => f.Contains(".xnb")).ToArray();
                    exporter = new Exporter(xnbFiles);
                }
                else
                {
                    exporter = new Exporter(args);
                }

                exporter.OnStatusUpdate += Exporter_OnStatusUpdate;
                exporter.Run();
            }
            else
            {
                // If there are too many (or no) arguments, tell the user how to use it properly.

                WriteLine("XNB Exporter is a command-line only application.");
                WriteLine("You must specify a file or directory to export.");
                WriteLine("ex. xnbe.exe \"C:/dir/file.xnb\"");
                WriteLine("ex. xnbe.exe \"C:/dir\"");
                WriteLine("\nDon't forget to use quotation marks in your path!\n");
                WriteLine("\nPress any key to exit . . .");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        private static void Exporter_OnStatusUpdate(string status)
        {
            WriteLine(status);
        }

        /// <summary>
        /// It's like Console.WriteLine, but it has a nice indent!
        /// </summary>
        public static void WriteLine(string s)
        {
            s = s.Replace("\n", "\n\t");
            Console.WriteLine("\t" + s);
        }

        /// <summary>
        /// Pretty self explanatory. It clears the last console line.
        /// </summary>
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
        }
    }
}
