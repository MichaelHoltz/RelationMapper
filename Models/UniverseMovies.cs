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

        ////Function for Universe which will find a movie and return a Search Result list.
        //public async Task<TmdbSearch.SearchResult<TmdbSearch.MovieSummary>> FindMovie(String movieName)
        //{
        //    TmdbSearch.SearchResult<TmdbSearch.MovieSummary> movieSummaryList = await TheMovieDb.SearchMovieAsync(movieName);
        //    return movieSummaryList;
        //}
        /// <summary>
        /// Get all Movies in the "known Universe"
        /// </summary>
        /// <returns></returns>
        public HashSet<Movie> GetAllMovies()
        {
            //HashSet<Movie> m = new HashSet<Movie>();
            //foreach (StudioGroup s in StudioGroups)
            //{
            //    foreach (Movie item in s.Movies)
            //    {
            //        m.Add(item);
            //    }
            //}
            return Movies;
        }
        /// <summary>
        /// Get all Movies in a given StudioGroup
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
        public Movie GetMovie(String movieName)
        {
            Movie m = null;
            HashSet<Movie> allMovies = GetAllMovies();
            if (allMovies.Select(o => o.Title).Contains(movieName))
            {
                m = allMovies.First(o => o.Title == movieName);
            }
            return m;
        }
        public Movie GetMovie(int movieId)
        {

            Movie m = null;
            if (Movies.Select(o => o.TmdbId).Contains(movieId))
            {
                m = Movies.First(o => o.TmdbId == movieId);
            }
            return m;
        }
        #endregion Movie
    }
}
