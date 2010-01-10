using System;

namespace DomainTestClasses
{
    public class Foo
    {
        public int MyPorp { get; set; }

        public Foo()
        {
            MyPorp = 1;
        }
        
        static void First()
        {
            int x = 4;
            {
                int y = 5;
            }
            Console.WriteLine("Boo!");
        }

        static void Second()
        {
            Console.WriteLine("second");
            for (int x = 5; x < 7; x++)
                Console.WriteLine("bang");

        }

        static void Third(int i,string value)
        {
            if (i == 0)
                Console.WriteLine(value);

            else if (i < 5)
            {
                Console.WriteLine("less than 5");
            }
            else
            {
                Console.WriteLine("greater than 5");
            }
        }

        static int Fourth()
        {
            /*int i = 0;
            int k = 1;

            while(i < 5)
            {
                k++;
            }*/

            var random = new Random(4);

            int j = random.Next(0, 3);
            int l = 0;

            switch (j)
            {
                case 0:
                    l += 2;
                    break;

                case 1: l += 3;
                    break;

                case 2: l += 4;
                    break;

                default: l += 6;
                    break;
            }

            return 4;
        }

        public static void StaticMethod()
        {
            int i = 0;
            i++;
            return;
        }

        
    }

    public abstract class BaseClass
    {
        public abstract void AbstractMethod();
    }
}