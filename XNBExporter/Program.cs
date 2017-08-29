using System;
using System.IO;
using System.Linq;
using System.Threading;
using XNBLib;

namespace XNBExporter
{
    public static class Program
    {
        private static Exporter _exporter;

        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
                _                                  _            
    __  ___ __ | |__     _____  ___ __   ___  _ __| |_ ___ _ __ 
    \ \/ / '_ \| '_ \   / _ \ \/ / '_ \ / _ \| '__| __/ _ \ '__|
     >  <| | | | |_) | |  __/>  <| |_) | (_) | |  | ||  __/ |   
    /_/\_\_| |_|_.__/   \___/_/\_\ .__/ \___/|_|   \__\___|_|   
                                 |_|                 Release 1.2
            ");
            Console.ForegroundColor = ConsoleColor.Gray;

            WriteLine("Note: This version supports audio, images, and\nsprite font textures. More formats are planned for\nlater versions.\n\n");

            if (args.Length == 1)
            {
                // We need to determine whether the user has passed in singular file or a directory.
                if (Directory.Exists(args[0]))
                {
                    var allFiles = Directory.EnumerateFiles(args[0]).ToList();
                    var xnbFiles = allFiles.Where(f => f.Contains(".xnb")).ToArray();
                    _exporter = new Exporter(xnbFiles);
                }
                else
                {
                    _exporter = new Exporter(args);
                }

                RunExporter();
            }
            else if (args.Length == 2)
            {
                // If the destination directory doesn't exist, create it.
                if (!Directory.Exists(args[1])) Directory.CreateDirectory(args[1]);
                // Make sure the output directory has a \ at the end.
                if (!args[1].EndsWith("\\") || !args[1].EndsWith("/")) args[1] += "\\";

                // We need to determine whether the user has passed in singular file or a directory.
                if (Directory.Exists(args[0]))
                {
                    // EnumerateFiles is not recursive, by the way.
                    var allFiles = Directory.EnumerateFiles(args[0]).ToList();
                    var xnbFiles = allFiles.Where(f => f.Contains(".xnb")).ToArray();
                    _exporter = new Exporter(xnbFiles, args[1]);
                }
                else
                {
                    _exporter = new Exporter(new [] { args[0] }, args[1]);
                }

                RunExporter();
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
                Environment.Exit(2);
            }
        }

        private static void RunExporter()
        {
            _exporter.OnStatusUpdate += Exporter_OnStatusUpdate;
            _exporter.OnCompleted += Exporter_OnCompleted;
            _exporter.Run();
        }

        private static void Exporter_OnCompleted()
        {
            WriteLine("Exiting...");
            // Please don't kill me, this is to give the user a chance to read the message.
            Thread.Sleep(2000);
            Environment.Exit(0);
        }

        private static string _previousLine = "";
        private static void Exporter_OnStatusUpdate(string status)
        {
            if (!_previousLine.StartsWith("[")) ClearLine();
            WriteLine(status);
            _previousLine = status;
        }

        /// <summary>
        /// It's like Console.WriteLine, but it has a nice indent!
        /// </summary>
        public static void WriteLine(string s)
        {
            var oldConsoleColor = Console.ForegroundColor;
            if (s.Contains("SUCCESS")) Console.ForegroundColor = ConsoleColor.Green;
            if (s.Contains("WARNING")) Console.ForegroundColor = ConsoleColor.Yellow;
            s = s.Replace("\n", "\n\t");
            Console.WriteLine("\t" + s);
            Console.ForegroundColor = oldConsoleColor;
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
