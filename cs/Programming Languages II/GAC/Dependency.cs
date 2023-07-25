using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR0083
{
    class Dependency
    {
        // Properties
        public string Name { get; set; }
        public int MinimumVersion { get; set; }
        public int MaximumVersion { get; set; }

        // Constructor
        public Dependency(string name, int minimumVersion, int maximumVersion)
        {
            Name = name;
            MinimumVersion = minimumVersion;
            MaximumVersion = maximumVersion;
        }

        public override string ToString()
        {
            return "Name: " + Name + ", MinimumVersion: " + MinimumVersion + ", MaximumVersion: " + MaximumVersion;
        }
    }
}
