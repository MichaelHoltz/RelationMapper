using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// Partial Class to Handle all Movie Collection actions
    /// </summary>
    public partial class Universe3
    {
        /// <summary>
        /// Movie Collections found by Movie Lookup
        /// </summary>
        public HashSet<MovieCollection> MovieCollections { get; set; }
        /// <summary>
        /// Normalization to avoid duplicate data.
        /// </summary>
        public HashSet<MovieCollectionMap> MovieCollectionMap { get; set; }
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

        public Boolean AddMovieToCollection(int CollectionId, int movieID)
        {
            MovieCollectionMap tmcm = new Models.MovieCollectionMap();
            tmcm.MovieCollectionId = CollectionId;
            tmcm.MovieId = movieID;
            return MovieCollectionMap.Add(tmcm);
        }
        /// <summary>
        /// Gets the Movie Collection for a movie
        /// </summary>
        /// <param name="movieID"></param>
        /// <returns></returns>
        public MovieCollection GetMovieCollection(int movieID)
        {
            MovieCollectionMap mcm = MovieCollectionMap.Where(o => o.MovieId == movieID).FirstOrDefault();
            MovieCollection mc = null;
            if (mcm != null)
            {
                int mcid = mcm.MovieCollectionId;
                mc = MovieCollections.Where(o => o.Id == mcid).FirstOrDefault();
            }
            return mc;
        }
        /// <summary>
        /// Only returns movies in collection that are cached
        /// </summary>
        /// <param name="collectionId"></param>
        /// <returns></returns>
        public HashSet<Movie> GetMoviesInCollection(int collectionId)
        {
            HashSet<Movie> retVal = new HashSet<Movie>();
            var mcm = MovieCollectionMap.Where(o => o.MovieCollectionId == collectionId);
            foreach (var item in mcm)
            {
                Movie m = GetMovie(item.MovieId);
                if(m!= null)
                    retVal.Add(m);
            }
            
            return retVal;
        }
        //public MovieCollection UpdateMovieCollection(String name)
        //{
        //}
        //public MovieCollection UpdateMovieCollection(int id)
        //{

        //}
        #endregion
    }
}
