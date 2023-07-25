using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR0083
{
    class GAC
    {
        // Properties
        public List<Assembly> Assemblies { get; set; } = new List<Assembly>();
        public static GAC Instance = new GAC();

        // Enums
        public enum Operation { Add, Update }

        // Delegates
        public delegate void ActionPerformed(string s, Operation o);

        // Event
        public event ActionPerformed Update;

        // Methods
        public void Install(Assembly assembly)
        {
            if (Assemblies.Any(a => a.Name == assembly.Name && a.Version == assembly.Version))
            {
                Update.Invoke(assembly.Name, Operation.Update);
            }
            else
            {
                Update.Invoke(assembly.Name, Operation.Add);
                Assemblies.Add(assembly);
            }
        }

        public void Clean()
        {
            Assemblies.Clear();
        }

        public void List()
        {
            foreach (Assembly assembly in Assemblies)
            {
                Console.WriteLine(assembly.Name + " v" + assembly.Version);
            }
        }
    }
}
