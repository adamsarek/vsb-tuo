using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR0083
{
    class FilmList : ICollection<Film>
    {
        private List<Film> items = new List<Film>();
        private int nextId = 1;

        public int Count
        {
            get { return this.items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(Film item)
        {
            item.Id = this.nextId;
            this.items.Add(item);
            this.nextId++;
        }

        public Film Get(int index)
        {
            return this.items[index];
        }

        public void Set(int index, Film item)
        {
            this.items[index] = item;
        }

        public void SortByReleaseDate(bool desc = false)
        {
            List<Film> sortedItems = (desc ? this.items.OrderByDescending(o => o.ReleaseDate).ToList() : this.items.OrderBy(o => o.ReleaseDate).ToList());
            this.items.Clear();
            this.items = sortedItems;
        }
        
        public bool Remove(Film item)
        {
            return this.items.Remove(item);
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public bool Contains(Film item)
        {
            return this.items.Contains(item);
        }

        public void CopyTo(Film[] array, int arrayIndex)
        {
            this.items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Film> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
    }
}
