using System;

namespace DependencyInjectionExample
{
    public class ConsoleLogger : ILogger
    {
        public void Write(string message) => Console.Write(message);
    }
}
