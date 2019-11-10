using System;

namespace HangfireServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            GlobalConfiguration.Configuration.UseSqlServerStorage("connection_string");
        }
    }
}
