using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Recursion
{
    class Movie
    {
        public string Name { get; private set; }
        public int Duration { get; private set; }

        public Movie(string name)
        {
            Name = name;
        }

        public Movie(string name, int duration)
        {
            Name = name;
            Duration = duration;
        }

        public override bool Equals(object obj)
        {
            Movie movie = (Movie)obj;
            if (this.Name == movie.Name && this.Duration == movie.Duration)
            {
                return true;
            }
            return false;
        }
    }
}
