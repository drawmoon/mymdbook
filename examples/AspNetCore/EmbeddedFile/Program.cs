using System;
using System.IO;
using System.Reflection;

namespace AspNetCoreEmbeddedFile
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream("AspNetCoreEmbeddedFile.Resources.text.txt"))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    while (!streamReader.EndOfStream)
                    {
                        Console.WriteLine(streamReader.ReadLine());
                    }
                }
            }
        }
    }
}
