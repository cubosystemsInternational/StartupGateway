using System;

namespace StartupGateway.Testing
{
    public class Main
    {
        public static void main(string[] args)
        {
            // Call Function 1
            Function1();

            // Call Function 2
            Function2();
        }

        static void Function1()
        {
            Console.WriteLine("Function 1 called.");
        }

        static void Function2()
        {
            Console.WriteLine("Function 2 called.");
        }
    }
}
