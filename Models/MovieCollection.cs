using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Utilities;
using TheMovieDb = TmdbWrapper.TheMovieDb;
namespace RelationMap.Models
{
    public class MovieCollection
    {
        #region basic properties from  movie info
        public String Name { get; set; }
        public int Id { get; set; }
        public String PosterPath { get; set; }
        public String BackdropPath { get; set; }
        #endregion
        #region extended properties from Collections info
        public String Overview { get; set; }
        /// <summary>
        /// Parts are the ids of the other movies
        /// </summary>
       // public HashSet<int> Parts { get; set; }

        #endregion
        public MovieCollection()
        {
            //Parts = new HashSet<int>();
        }
        /// <summary>
        /// Add move Collection from Movie Information
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="posterPath"></param>
        /// <param name="backdropPath"></param>
        public MovieCollection(String name, int id, String posterPath, String backdropPath)
        {
            Name = name;
            Id = id;
            PosterPath = posterPath;
            BackdropPath = backdropPath;
           // Parts = new HashSet<int>();
        }
        private Uri Uri(BackdropSize size)
        {
            return MakeImageUri(size.ToString(), BackdropPath);
        }
        private static Uri MakeImageUri(string size, string path)
        {
            //Hack for now and assuming that using SecureBaseUrl.. 
            return new Uri(string.Format("{0}{1}{2}", TheMovieDb.GetConfigurationSecureBaseUrl(), size, path));
        }

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
            String key = this.GetType().Name + Name + Id;
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
