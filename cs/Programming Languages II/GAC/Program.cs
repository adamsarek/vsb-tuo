using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAR0083.GAC;

namespace SAR0083
{
    class Program
    {
        static void Main(string[] args)
        {
            GAC.Instance.Update += Print;
            GAC.Instance.Install(new Assembly("A", 2));
            GAC.Instance.Install(new Assembly("B", 1, new Dependency("A", 0, 3)));
            GAC.Instance.Install(new Assembly("A", 2));
            GAC.Instance.Install(new Assembly("A", 1));
            GAC.Instance.Install(new Assembly("C", 2,
            new Dependency("A", 1, 2),
            new Dependency("B", 1, 1)));
            GAC.Instance.Install(new Assembly("B", 2, new Dependency("A", 0, 2)));
            GAC.Instance.Install(new Assembly("A", 3));
            GAC.Instance.Clean();
            GAC.Instance.List();

            Console.ReadKey();
        }

        static void Print(string s, Operation o)
        {
            Console.WriteLine(o + ": " + s);
        }
    }
}
