using System;

namespace EmailTestApp
{
    internal class MyLogger
    {
        public static void Info(string message)
        {
            Console.WriteLine(message);
        }

        public static void Error(string s, Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(s);

            Console.WriteLine(exception);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

    }
}