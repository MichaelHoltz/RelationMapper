using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// All Studios are in here
    /// </summary>
    public class Universe
    {
        public HashSet<Studio> Studios { get; set; }

        public Universe()
        {
            Studios = new HashSet<Studio>();
        }
        public Studio AddStudio(String studioName)
        {
            Studio s = new Studio(studioName);
            Studios.Add(s);
            return s;
        }
        public Studio GetStudio(String studioName)
        {
            return Studios.First(o => o.Name == studioName);
        }
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
