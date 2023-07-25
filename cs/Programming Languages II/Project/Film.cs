using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR0083
{
    class Film
    {
        private int id;
        private string name;
        private int? duration = null;
        private bool adultsOnly;
        private DateTime? releaseDate = null;
        private string description;

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public int? Duration
        {
            get { return this.duration; }
            set { this.duration = value; }
        }

        public bool AdultsOnly
        {
            get { return this.adultsOnly; }
            set { this.adultsOnly = value; }
        }

        public DateTime? ReleaseDate
        {
            get { return this.releaseDate; }
            set { this.releaseDate = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
    }
}
