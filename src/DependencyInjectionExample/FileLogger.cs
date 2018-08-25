using System;
using System.IO;

namespace DependencyInjectionExample
{
    public class FileLogger : ILogger
    {
        public static string Path = $".\\{DateTime.Today.ToString("yyyy-MM-dd")}.log";
        public void Write(string message) => File.AppendAllText(Path, $"{message}{Environment.NewLine}");
    }
}
