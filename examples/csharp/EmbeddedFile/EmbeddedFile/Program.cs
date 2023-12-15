using System;
using System.IO;
using System.Reflection;


var assembly = Assembly.GetExecutingAssembly();

using (var stream = assembly.GetManifestResourceStream("EmbeddedFile.Resources.text.txt"))
{
    using (var streamReader = new StreamReader(stream))
    {
        while (!streamReader.EndOfStream)
        {
            Console.WriteLine(streamReader.ReadLine());
        }
    }
}
