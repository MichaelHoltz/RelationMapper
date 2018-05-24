using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// StudioGroup has Franchise
    /// 
    /// StudioGroup is one or more Production Companies rolled into one.
    /// 
    /// Major re-think based on hooking up to TheMoveDB data.
    /// They only have Collections and (custom) lists to represent the idea of a Franchise.
    /// 
    /// Also Since moves are produced by more than one studio (Production Company) StudioGroup will need to have a collection of Production Companies..
    /// Further - In an attempt to normalize the data it will be necessary to make many more changes.
    /// 
    /// </summary>
    public class StudioGroup
    {


        public String Name { get; set; }
        /// <summary>
        /// Id of the Primary / Selected production company
        /// </summary>
        public int Id { get; set; }

        ///// <summary>
        ///// All Franchises in StudioGroup
        ///// EX X-Men, Avengers, Batman, Superman
        ///// </summary>
        //public HashSet<Franchise> Franchises {get; set;}
        ///// <summary>
        ///// All Movies in the StudioGroup
        ///// </summary>
        //public HashSet<Movie> Movies { get; set; }


        public StudioGroup()
        {
            InitializeHashSets();
        }
        public StudioGroup(String studioName)
        {
            Name = studioName;
            InitializeHashSets();
        }
        private void InitializeHashSets()
        {
            //Franchises = new HashSet<Franchise>();
            //Movies = new HashSet<Movie>();
            //TvShows = new HashSet<TvShow>();

        }
        //public Franchise AddFranchise(String franchiseName)
        //{
        //    Franchise f = new Franchise(franchiseName);
        //    Franchises.Add(f);
        //    return f;
        //}
        //public bool AddFranchise(Franchise franchiseName)
        //{
        //    return Franchises.Add(franchiseName);
        //}
        //public Franchise GetFranchise(String franchiseName)
        //{
        //    foreach (Franchise f in Franchises)
        //    {
        //        if (f.Name == franchiseName)
        //            return f;
        //    }
        //    return null;
        //}
        //public Movie AddMovie(String name, int releaseYear)
        //{
        //    Movie m = new Movie(name, releaseYear);
        //    Movies.Add(m);
        //    return m;
        //}
        //public Movie AddMovieToFranchise(String movieName, int releaseYear, String franchiseName)
        //{
        //    Movie m = AddMovie(movieName, releaseYear);
        //    Franchise f = GetFranchise(franchiseName);
        //    if (f == null)
        //        f = AddFranchise(franchiseName);
        //    f.AddMovie(m.GetHashCode());
        //    return m;
        //}
        //public Franchise AddMovieToFranchise(Movie movie, String franchiseName)
        //{
        //    Franchise f = GetFranchise(franchiseName);
        //    if (f == null)
        //    {
        //        f = AddFranchise(franchiseName);
        //    }
        //    f.AddMovie(movie.GetHashCode());
        //    return f;
        //}
        //public Boolean AddMovieToFranchise(Movie movie, Franchise franchise)
        //{
        //    return franchise.AddMovie(movie.GetHashCode());
        //}
        //public Boolean AddMovie(Movie m)
        //{
        //    return Movies.Add(m);
        //}

        //public Movie GetMovie(String name)
        //{
        //    return Movies.First(o => o.Title == name);
        //}

        //public TvShow AddTvShow(String name)
        //{
        //    TvShow tv = new TvShow(name);
        //    TvShows.Add(tv);
        //    return tv;

        //}
        //public Boolean AddTvShow(TvShow tvShow)
        //{
        //    return TvShows.Add(tvShow);
        //}
        //public TvShow GetTvShow(String name)
        //{
        //    return TvShows.First(o => o.Name == name);
        //}
        //public TvShow AddTvShowToFranchise(String showName, String franchiseName)
        //{
        //    TvShow m = AddTvShow(showName);
        //    Franchise f = GetFranchise(franchiseName);
        //    if (f == null)
        //        f = AddFranchise(franchiseName);
        //    f.AddTvShow(m.GetHashCode());
        //    return m;
        //}
        //public Franchise AddTvShowToFranchise(TvShow tvShow, String franchiseName)
        //{
        //    Franchise f = GetFranchise(franchiseName);
        //    if (f == null)
        //    {
        //        f = AddFranchise(franchiseName);
        //    }
        //    f.AddTvShow(tvShow.GetHashCode());
        //    return f;
        //}
        //public Boolean AddTvShowToFranchise(TvShow tvShow, Franchise franchise)
        //{
        //    return franchise.AddTvShow(tvShow.GetHashCode());
        //}

        #region Overrides
        /// <summary>
        /// Returns this instance ToString
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
        #endregion
        #region HashCodes / Object Identification
        //TODO - use / include the "correct" id..
        private int _hashCode = 0;
        public int HashCode
        {
            get
            {
                return _hashCode == 0 ? generateHashCode() : _hashCode;
            }
            //Need set for persistence to restore 
            set
            {
                _hashCode = value;
            }
        }
        private int generateHashCode()
        {
            //THis is expensive and should be done only once since it will not be changing
            //TODO - use / include the "correct" id..
            String key = this.GetType().Name + Name;
            //Google: "disable fips mode" if the line below fails
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(key));
            int ivalue = BitConverter.ToInt32(hashed, 0);
            return ivalue;

        }
        public override int GetHashCode()
        {
            return HashCode;
        }
        public override bool Equals(object obj)
        {
            return obj.GetHashCode().Equals(HashCode); // == this.GetHashCode();
        }
        #endregion
    }
}
