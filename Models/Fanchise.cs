using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{

    /// <summary>
    /// Evolution.. Started as Franchise, specifically with Marvel Cinematic Universe(MCU), and others like it.
    /// After seeing what is available online it is clear that the original idea will have to mostly manual
    /// 
    /// TMDB has collections but that is only for sequels. So a Franchise can contain collections (Movies are in a collection)
    /// But a Franchise is to be more than simply sequels so it's a SuperCollection or SuperGroup.
    /// TMDB has custom lists which can be used to save / read in, but they are per-user (I guess if they are public they can be referenced by any user.)
    /// 
    /// Personally have less interest in TVShows but not zero, however I'm considering removing them for now..
    /// The original idea (again) was from wanting to order the MCU in perfect viewing order (any SuperCollection for that matter) and MCU includes TV Shows/Seasons/Episodes
    /// Seasons and Episodes are necessary for Perfect order, but beyond the scope.
    /// 
    /// </summary>
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
        //public Boolean IsTvShowInFranchise(TvShow tvShow)
        //{
        //    return TvShows.Contains(tvShow.HashCode);
        //}
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
