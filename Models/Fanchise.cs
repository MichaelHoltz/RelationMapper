using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{

    public class Franchise
    {
        public String Name { get; set; }
        /// <summary>
        /// All Movies in Franchise
        /// </summary>
        public HashSet<int> Movies = new HashSet<int>();
        /// <summary>
        /// All TvShows in Franchise
        /// </summary>
        public HashSet<int> TvShows = new HashSet<int>();
        public Franchise()
        {
            Movies = new HashSet<int>();
            TvShows = new HashSet<int>();
        }
        public Franchise(String name)
        {
            Name = name;
            Movies = new HashSet<int>();
            TvShows = new HashSet<int>();
        }
        public Boolean AddMovie(int movieHashCode)
        {
            return Movies.Add(movieHashCode);

        }
        public Boolean IsMovieInFranchise(Movie movie)
        {
            return Movies.Contains(movie.HashCode);
        }
        public Boolean AddTvShow(int tvShowHashCode)
        {
            return TvShows.Add(tvShowHashCode);
        }
        public Boolean IsTvShowInFranchise(TvShow tvShow)
        {
            return TvShows.Contains(tvShow.HashCode);
        }
        public override int GetHashCode()
        {
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(this.GetType().Name + Name));
            int ivalue = BitConverter.ToInt32(hashed, 0);
            Console.WriteLine(ivalue);
            return ivalue; 
        }
        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == this.GetHashCode();
        }
    }
}
