using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TmdbWrapper.Utilities;
using TheMovieDb = TmdbWrapper.TheMovieDb;

namespace RelationMap.Models
{
    public class Movie
    {

        public String Title { get; set; }
        public String OriginalTitle { get; set; }

        //public DateTime ReleaseDate {get; set; }
        public int ReleaseYear { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public String BackdropPath { get; set; }
        public String PosterPath { get; set; }
        public int TmdbId { get; set; }
        /// <summary>
        /// Id of this movie in the IMDB.
        /// </summary>
        public string ImdbId { get; set; }
        /// <summary>
        /// Home page of this movie
        /// </summary>
        public Uri HomePage { get; set; }
        /// <summary>
        /// Overview of this movie.
        /// </summary>
        public string Overview { get; set; }
        /// <summary>
        /// Original runtime in minutes.
        /// </summary>
        public int Runtime { get; set; }
        /// <summary>
        /// Revenue that this movie gathered.
        /// </summary>
        public Int64 Revenue { get; set; }
        public String TrailerLink { get; set; }
        
        public Movie()
        {
            Title = null;
            ReleaseYear = 1900;

        }
        public Movie(String name, int releaseYear)
        {
            Title = name;
            ReleaseYear = releaseYear;
        }


        /// <summary>
        /// Gets the actual Movie Poster for this Movie
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public Image GetMoviePoster(PosterSize size)
        {
            Image x = null;
            if (PosterPath != null)
            {
                String cachePath = PrivateData.GetAppPath() + @"\Cache\Images\Movie\" + PosterPath.Replace("/", "");

                if (File.Exists(cachePath))
                {
                    x = Image.FromFile(cachePath); // Use Image from Cache
                }
                else
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(cachePath));  // Insure Directory Exists

                    Uri uri = Uri(size);
                    var wc = new WebClient();
                    x = Image.FromStream(wc.OpenRead(uri)); //Read from the Internet
                    x.Save(cachePath);                      //Save Image in Cache
                }
            }
            return x;
        }
        private Uri Uri(PosterSize size)
        {
            return MakeImageUri(size.ToString(), PosterPath);
        }
        private static Uri MakeImageUri(string size, string path)
        {
            //Hack for now and assuming that using SecureBaseUrl.. 
            return new Uri(string.Format("{0}{1}{2}",  TheMovieDb.GetConfigurationSecureBaseUrl(), size, path));
        }

        #region Overrides
        /// <summary>
        /// Returns this instance ToString
        /// </summary>
        public override string ToString()
        {
            return Title;
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
            String key = this.GetType().Name + Title;
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
