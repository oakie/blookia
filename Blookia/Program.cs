using System;

namespace Blookia
{
    class Program
    {
        static void Main(string[] args)
        {
            var blookia = new Blookia();

            blookia.Transfer(0, 1, 20);
            blookia.Transfer(0, 2, 20);
            blookia.Transfer(1, 2, 3);

            Console.WriteLine(blookia);
            Console.Read();
        }
    }
}
