using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace DirectoryTreePaths
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!CheckArgs(args))
            {
                Console.WriteLine("Invalid usage. Run the program with /? key for getting the usage.");
                return;
            }

            if (args[0] == "/?")
            {
                ShowUsage();
                return;
            }

            if (args.Length == 2)
            {
                if (args[1] == "/v1")
                {
                    ProcessDirectory1(args[0]);
                }
                else
                {
                    Console.WriteLine("Invalid usage. Run the program with /? key for getting the usage.");
                }
            }
            else
            {
                ProcessDirectory2(args[0]);
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine(@"
© Roman Tarasiuk, 2017
DirectoryTreePaths.exe path [/v1]
DirectoryTreePaths.exe /?
");
        }

        private static bool CheckArgs(string[] args)
        {
            if (args.Length == 0)
            {
                return false;
            }

            if ((!Directory.Exists(args[0])) && (args[0] != "/?"))
            {
                return false;
            }

            return true;
        }

        //
        // Reverse order (like 'tree' command).
        //
        private static void ProcessDirectory1(string path)
        {
            try
            {
                var files = Directory.GetFiles(path);
                foreach (var f in files)
                {
                    Console.WriteLine("f\t" + f);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("e\tError (getting files list): " + path);
            }

            try
            {
                var directories = Directory.GetDirectories(path);
                foreach (var d in directories)
                {
                    ProcessDirectory1(d);
                    Console.WriteLine("d\t" + d);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("e\tError (getting directories list): " + path);
            }
        }

        //
        // Directories first.
        //
        private static void ProcessDirectory2(string path)
        {
            try
            {
                var directories = Directory.GetDirectories(path);
                foreach (var d in directories)
                {
                    Console.WriteLine("d\t" + d);

                    ProcessDirectory2(d);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("e\tError (getting directories list): " + path);
            }

            try
            {
                var files = Directory.GetFiles(path);
                foreach (var f in files)
                {
                    Console.WriteLine("f\t" + f);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("e\tError (getting files list): " + path);
            }
        }
    }
}
