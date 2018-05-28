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
    /// 
    /// Universe can be thought of as all Movies the user is interested in. Probably should have called it MyMovies (Inspiration was from Marvel Cinematic Universe)
    /// 
    /// This is the Root Class and contains everything pertaining to a Movie (and later.. possibly TV Shows will be completed)
    /// 
    /// This is also an exercise in seeing how far I can push JSON Persistence and direct Object usage for in memory database usage.
    /// 
    /// </summary>
    public class Universe
    {
        /// <summary>
        /// A StudioGroup is a collection of Production Companies 
        /// Ex. "Marvel" is Marvel Enterprises, Marvel Studios, Marvel Entertainment. 
        /// The purpose is to address that a "Marvel" movie is actually all the above and even side Production Companies Like Disney but the common thread
        /// is Marvel movie is intended, vs the minutia of the full Production Company name for a given movie.
        /// </summary>
        public HashSet<StudioGroup> StudioGroups { get; set; }
        /// <summary>
        /// Movie Collections found by Movie Lookup
        /// </summary>
        public HashSet<MovieCollection> MovieCollections { get; set; }
        /// <summary>
        /// All Production Companies which produce a movie 
        /// </summary>
        public HashSet<ProductionCompany> ProductionCompanies { get; set; }

        public HashSet<Movie> Movies { get; set; }
        
        /// <summary>
        /// All Actual People related to movies, TV Shows, and the production of them.
        /// </summary>
        public HashSet<Person> People { get; set; }
        public int NextCharacterIndex { get; set; }
        /// <summary>
        /// All characters in all movies - (Seems could be role like Character or Crew - Producer etc.)
        /// </summary>
        public HashSet<Character> Characters { get; set; }
        public Universe()
        {
            StudioGroups = new HashSet<StudioGroup>();
            MovieCollections = new HashSet<MovieCollection>();
            ProductionCompanies = new HashSet<ProductionCompany>();
            Movies = new HashSet<Movie>();
            People = new HashSet<Person>();
            Characters = new HashSet<Character>();
        }
        #region People
        public Boolean AddPerson(Person person)
        {
            Boolean result = People.Add(person);
            if (!result) // Movie already there, but need to update (If they aren't already) //But I could be passing in a person with basic info and t
            {
                Person existingPerson =  People.First(o => o.Id == person.Id);
                if (existingPerson.Updated)
                {
                    result = true;
                }
                else
                {
                    People.Remove(person); // Hack at update by just replacing. 
                    result = People.Add(person);
                }
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
        #endregion Movie

        #region MovieCollection
        /// <summary>
        /// Adds or updates a movie collection
        /// if Update only poster and backdrop are updated
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="posterPath"></param>
        /// <param name="backdropPath"></param>
        /// <returns></returns>
        public MovieCollection AddMovieCollection(String name, int id, String posterPath, String backdropPath)
        {
            MovieCollection mc = new MovieCollection(name, id, posterPath, backdropPath);
            Boolean result = MovieCollections.Add(mc);
            if (!result) // Collection already exists and I believe has more than the basic information here
            {
                mc = MovieCollections.First(o => o.Id == id); // Update things that can be; (Name is part of hash code)
                mc.PosterPath = posterPath;
                mc.BackdropPath = backdropPath;
            }
            return mc;
        }
        //public MovieCollection UpdateMovieCollection(String name)
        //{
        //}
        //public MovieCollection UpdateMovieCollection(int id)
        //{

        //}
        #endregion

        #region Production Company 

        public ProductionCompany AddProductionCompany(String pcName, int pcId, String logoPath, String originCountry)
        {
            ProductionCompany pc = new ProductionCompany(pcName, pcId, logoPath, originCountry);
            Boolean result = AddProductionCompany(pc);
            return pc;
        }
        public Boolean AddProductionCompany(ProductionCompany pc)
        {
            Boolean result = ProductionCompanies.Add(pc);
            if (!result)
            {
                ProductionCompanies.Remove(pc); //Force Replace
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

        #region StudioGroup (One or more Production Company grouped into one)
        public StudioGroup AddStudio(String studioName)
        {
            StudioGroup s = new StudioGroup(studioName);
            Boolean result = StudioGroups.Add(s);
            return s;
        }
        public StudioGroup GetStudio(String studioName)
        {
            return StudioGroups.First(o => o.Name == studioName);
        }
        public StudioGroup GetStudio(int studioId)
        {
            return StudioGroups.First(o => o.Id == studioId);
        }

        #endregion StudioGroup

        ///// <summary>
        ///// Franchise is a Collection or List
        ///// </summary>
        ///// <returns></returns>
        //public HashSet<Franchise> GetAllFranchises()
        //{
        //    HashSet<Franchise> f = new HashSet<Franchise>();
        //    foreach (StudioGroup s in StudioGroups)
        //    {
                
        //        foreach (Franchise sf in s.Franchises)
        //        {
        //            f.Add(sf);
        //        }
        //    }
        //    return f;
        //}
        //public HashSet<Franchise> GetAllFranchises(StudioGroup s)
        //{
        //    HashSet<Franchise> f = new HashSet<Franchise>();
        //    foreach (Franchise sf in s.Franchises)
        //    {
        //        f.Add(sf);
        //    }
        //    return f;
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
        /// <summary>
        /// Get all Movies in a given Franchise
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public HashSet<Movie> GetAllMovies(Franchise f)
        {
            HashSet<Movie> m = new HashSet<Movie>();
            //foreach (StudioGroup s in StudioGroups)
            //{
            //    if (s.Franchises.Contains(f))
            //    {
            //        foreach (Movie item in s.Movies)
            //        {
            //            if (f.Movies.Contains(item.HashCode)) // Only add if movie in Franchise
            //            {
            //                m.Add(item);
            //            }
            //        }
            //    }
            //}
            return m;
        }
        /// <summary>
        /// Get all Movies not in any Franchise
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public HashSet<Movie> GetAllMoviesNotInAnyFranchise(StudioGroup s)
        {
            HashSet<Movie> m = new HashSet<Movie>();
            //foreach (Franchise f in s.Franchises) // All StudioGroup Franchises
            //{
            //    foreach (Movie item in s.Movies)
            //    {
            //        if (!f.Movies.Contains(item.HashCode)) // Only add if movie not in any franchise
            //        {
            //            m.Add(item);
            //        }
            //    }
            //}
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
        ///// <summary>
        ///// Get all TvShows in all StudioGroups
        ///// </summary>
        ///// <returns></returns>
        //public HashSet<TvShow> GetAllTvShows()
        //{
        //    HashSet<TvShow> tv = new HashSet<TvShow>();
        //    foreach (StudioGroup s in StudioGroups)
        //    {
        //        foreach (TvShow item in s.TvShows)
        //        {
        //            tv.Add(item);
        //        }
        //    }
        //    return tv;
        //}
        ///// <summary>
        ///// Get all TvShows in a given StudioGroup
        ///// </summary>
        ///// <param name="s"></param>
        ///// <returns></returns>
        //public HashSet<TvShow> GetAllTvShows(StudioGroup s)
        //{
        //    HashSet<TvShow> tv = new HashSet<TvShow>();
        //    foreach (TvShow item in s.TvShows)
        //    {
        //        tv.Add(item);
        //    }
        //    return tv;
        //}
        ///// <summary>
        ///// Get all TvShows in a given Franchise
        ///// </summary>
        ///// <param name="s"></param>
        ///// <returns></returns>
        //public HashSet<TvShow> GetAllTvShows(Franchise f)
        //{
        //    HashSet<TvShow> tv = new HashSet<TvShow>();
        //    foreach (StudioGroup s in StudioGroups)
        //    {
        //        if (s.Franchises.Contains(f))
        //        {
        //            foreach (TvShow item in s.TvShows)
        //            {
        //                if (f.TvShows.Contains(item.HashCode)) // Only add if TvShow in Franchise
        //                {
        //                    tv.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    return tv;
        //}
        //public TvShow GetTvShow(String tvShowName)
        //{
        //    TvShow tv = null;
        //    HashSet<TvShow> alltvShows = GetAllTvShows();
        //    if (alltvShows.Select(o => o.Name).Contains(tvShowName))
        //    {
        //        tv = alltvShows.First(o => o.Name == tvShowName);
        //    }
        //    return tv;
        //}
    }
}
