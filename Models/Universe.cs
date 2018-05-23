using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovieDb = TmdbWrapper.TheMovieDb;
using TmdbSearch = TmdbWrapper.Search;

namespace RelationMap.Models
{
    /// <summary>
    /// All Studios are in here
    /// </summary>
    public class Universe
    {
        public HashSet<Studio> Studios { get; set; }
        public HashSet<ProductionCompany> ProductionCompanies { get; set; }

        public HashSet<Movie> Movies { get; set; }
        public HashSet<Person> People { get; set; }

        public Universe()
        {
            Studios = new HashSet<Studio>();
            ProductionCompanies = new HashSet<ProductionCompany>();
            Movies = new HashSet<Movie>();
            People = new HashSet<Person>();
        }
        #region People
        public Boolean AddPerson(Person person)
        {
            Boolean result = People.Add(person);
            if (!result) // Movie already there, but need to update
            {
                People.Remove(person); // Hack at update by just replacing.
                result = People.Add(person);
            }
            return result;
        }

        #endregion
        #region Movie
        public Boolean AddMovie(Movie movie)
        {
            Boolean result = Movies.Add(movie);
            if (!result) // Movie already there, but need to update
            {
                Movies.Remove(movie); // Hack at update by just replacing.
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
        #endregion Movie

        #region Production Company 

        public ProductionCompany AddProductionCompany(String pcName, int pcId)
        {
            ProductionCompany pc = new ProductionCompany(pcName, pcId);
            Boolean result = AddProductionCompany(pc);
            return pc;
        }
        public Boolean AddProductionCompany(ProductionCompany pc)
        {
            Boolean result = ProductionCompanies.Add(pc);
            if (!result)
            {
                ProductionCompanies.Remove(pc);
                result = ProductionCompanies.Add(pc);
            }
            return result;
        }

        public ProductionCompany GetProductionCompany(String pcName)
        {
            return ProductionCompanies.First(o => o.Name == pcName);
        }
        public ProductionCompany GetProductionCompany(int pcId)
        {
            return ProductionCompanies.First(o => o.Id == pcId);
        }
        #endregion Production Company

        #region Studio (One or more Production Company grouped into one)
        public Studio AddStudio(String studioName)
        {
            Studio s = new Studio(studioName);
            Boolean result = Studios.Add(s);
            return s;
        }
        public Studio GetStudio(String studioName)
        {
            return Studios.First(o => o.Name == studioName);
        }
        public Studio GetStudio(int studioId)
        {
            return Studios.First(o => o.Id == studioId);
        }

        #endregion Studio

        /// <summary>
        /// Franchise is a Collection or List
        /// </summary>
        /// <returns></returns>
        public HashSet<Franchise> GetAllFranchises()
        {
            HashSet<Franchise> f = new HashSet<Franchise>();
            foreach (Studio s in Studios)
            {
                
                foreach (Franchise sf in s.Franchises)
                {
                    f.Add(sf);
                }
            }
            return f;
        }
        public HashSet<Franchise> GetAllFranchises(Studio s)
        {
            HashSet<Franchise> f = new HashSet<Franchise>();
            foreach (Franchise sf in s.Franchises)
            {
                f.Add(sf);
            }
            return f;
        }

        /// <summary>
        /// Get all Movies in all Studios
        /// </summary>
        /// <returns></returns>
        public HashSet<Movie> GetAllMovies()
        {
            HashSet<Movie> m = new HashSet<Movie>();
            foreach (Studio s in Studios)
            {
                foreach (Movie item in s.Movies)
                {
                    m.Add(item);
                }
            }
            return m;
        }
        /// <summary>
        /// Get all Movies in a given Studio
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public HashSet<Movie> GetAllMovies(Studio s)
        {
            HashSet<Movie> m = new HashSet<Movie>();
            foreach (Movie item in s.Movies)
            {
                m.Add(item);
            }
            return m;
        }
        /// <summary>
        /// Get all Movies in a given Franchise
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public HashSet<Movie> GetAllMovies(Franchise f)
        {
            HashSet<Movie> m = new HashSet<Movie>();
            foreach (Studio s in Studios)
            {
                if (s.Franchises.Contains(f))
                {
                    foreach (Movie item in s.Movies)
                    {
                        if (f.Movies.Contains(item.HashCode)) // Only add if movie in Franchise
                        {
                            m.Add(item);
                        }
                    }
                }
            }
            return m;
        }
        /// <summary>
        /// Get all Movies not in any Franchise
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public HashSet<Movie> GetAllMoviesNotInAnyFranchise(Studio s)
        {
            HashSet<Movie> m = new HashSet<Movie>();
            foreach (Franchise f in s.Franchises) // All studio Franchises
            {
                foreach (Movie item in s.Movies)
                {
                    if (!f.Movies.Contains(item.HashCode)) // Only add if movie not in any franchise
                    {
                        m.Add(item);
                    }
                }
            }
            return m;
        }
        public Movie GetMovie(String movieName)
        {
            Movie m = null;
            HashSet<Movie> allMovies = GetAllMovies();
            if(allMovies.Select(o => o.Title).Contains(movieName))
            {
                m = allMovies.First(o => o.Title == movieName);
            }
            return m;
        }
        public Movie GetMovie(int movieId)
        {

            Movie m = null;
            if (Movies.Select(o => o.DmdbId).Contains(movieId))
            {
                m = Movies.First(o => o.DmdbId == movieId);
            }
            return m;
        }
        /// <summary>
        /// Get all TvShows in all Studios
        /// </summary>
        /// <returns></returns>
        public HashSet<TvShow> GetAllTvShows()
        {
            HashSet<TvShow> tv = new HashSet<TvShow>();
            foreach (Studio s in Studios)
            {
                foreach (TvShow item in s.TvShows)
                {
                    tv.Add(item);
                }
            }
            return tv;
        }
        /// <summary>
        /// Get all TvShows in a given Studio
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public HashSet<TvShow> GetAllTvShows(Studio s)
        {
            HashSet<TvShow> tv = new HashSet<TvShow>();
            foreach (TvShow item in s.TvShows)
            {
                tv.Add(item);
            }
            return tv;
        }
        /// <summary>
        /// Get all TvShows in a given Franchise
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public HashSet<TvShow> GetAllTvShows(Franchise f)
        {
            HashSet<TvShow> tv = new HashSet<TvShow>();
            foreach (Studio s in Studios)
            {
                if (s.Franchises.Contains(f))
                {
                    foreach (TvShow item in s.TvShows)
                    {
                        if (f.TvShows.Contains(item.HashCode)) // Only add if TvShow in Franchise
                        {
                            tv.Add(item);
                        }
                    }
                }
            }
            return tv;
        }
        public TvShow GetTvShow(String tvShowName)
        {
            TvShow tv = null;
            HashSet<TvShow> alltvShows = GetAllTvShows();
            if (alltvShows.Select(o => o.Name).Contains(tvShowName))
            {
                tv = alltvShows.First(o => o.Name == tvShowName);
            }
            return tv;
        }
    }
}
