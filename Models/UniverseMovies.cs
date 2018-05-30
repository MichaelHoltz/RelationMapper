using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// UniverseMovies
    /// </summary>
    public partial class Universe3
    {
        public HashSet<Movie> Movies { get; set; }
        #region Movie
        public Boolean AddMovie(Movie movie)
        {
            Boolean result = Movies.Add(movie);
            if (!result) // Movie already there, but need to update
            {
                Movies.Remove(movie); // Hack at update by just replacing. (This would mess up any character settings if they were implemented)
                result = Movies.Add(movie);
            }
            return result;
        }

        /// <summary>
        /// Get all Movies in the "known Universe"
        /// </summary>
        /// <returns></returns>
        public HashSet<Movie> GetAllMovies()
        {
            return Movies;
        }
        /// <summary>
        /// Get all Movies in a given StudioGroup (Studio Groups not fully implemented)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public HashSet<Movie> GetAllMovies(StudioGroup s)
        {
            HashSet<Movie> m = new HashSet<Movie>();
            //foreach (Movie item in s.Movies)
            //{
            //    m.Add(item);
            //}
            return m;
        }
        /// <summary>
        /// Get Movie By Name and Year
        /// </summary>
        /// <param name="movieName"></param>
        /// <returns></returns>
        public Movie GetMovie(String movieName, int releaseYear)
        {
            return Movies.FirstOrDefault(o=>o.Title == movieName && o.ReleaseYear == releaseYear);
        }
        /// <summary>
        /// Get Movie By Name
        /// </summary>
        /// <param name="movieName"></param>
        /// <returns></returns>
        public Movie GetMovie(String movieName)
        {
            return Movies.FirstOrDefault(o => o.Title == movieName);
        }

        /// <summary>
        /// Get Movie by ID
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public Movie GetMovie(int movieId)
        {
            return Movies.FirstOrDefault(o => o.TmdbId == movieId);
        }
        #endregion Movie
    }
}
