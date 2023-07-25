using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR0083
{
    class Assembly : IComparable<Assembly>
    {
        // Variables
        private List<Dependency> dependsOn = new List<Dependency>();

        // Properties
        public string Name { get; set; }
        public int Version { get; set; }
        public Dependency DependsOn
        {
            set
            {
                dependsOn.Add(value);
            }
        }

        // Constructor
        public Assembly(string name, int version, params Dependency[] dependencies)
        {
            Name = name;
            Version = version;
            foreach(Dependency dependency in dependencies)
            {
                DependsOn = dependency;
            }
        }

        // Methods
        public override string ToString()
        {
            return "Name: " + Name + ", Version: " + Version;
        }

        public int CompareTo(Assembly other)
        {
            int comparison = Name.CompareTo(other.Name);
            if (comparison != 0) { return comparison; }
            else { return Version.CompareTo(other.Version); }
        }
    }
}
