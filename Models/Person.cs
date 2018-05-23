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
    /// <summary>
    /// Person can be an Actor or Crew
    /// 
    /// After Connecting to TMDB it is clear that this class should represent a person to include "crew"
    /// </summary>
    public class Person
    {
        //------------------------------------------------------
        //TMDB Get Person has.
        // adult Boolean
        //also_known_as array[object]
        //biography string
        //birthday string or null
        //deathday string or null
        //gender integer (0:not set, 1:Female, 2:Male
        //imdb_id string
        //place_of_birth string or null
        //popularity number (decimal?)
        //
        // Images are an additional query
        // Having the thumbnail 
        //
        //-------------------------------------------
        public int Id { get; set; }
        public String Name { get; set; }
        public String Prefix { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; } // Samuel L. Jackson
        public String LastName { get; set; }
        public String Suffix { get; set; } //Robert Downey Jr.
        public String ProfilePath { get; set; }
        public Uri HomePage { get; set; }
        public String Biography { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? Deathday { get; set; }
        public String PlaceOfBirth { get; set; }

        /// <summary>
        /// Default constructor required for Persistance restore if "bad data"
        /// </summary>
        public Person()
        {

        }
        /// <summary>
        /// Original plan was to split name
        /// TODO - Complete or abandon splitting name.
        /// </summary>
        /// <param name="actorName"></param>
        public Person(String actorName)
        {
            if (actorName != null)
            {
                Name = actorName;
                Char delimiter = ' ';
                String[] substrings = actorName.Split(delimiter);
                if (substrings.Length == 1)
                {
                    FirstName = LastName = actorName;
                }
                else if (substrings.Length == 2)
                {
                    FirstName = substrings[0];
                    LastName = substrings[1];
                }
                else if(substrings.Length == 3 ) // Middle name or Suffix or Doctor Steven Strange
                {
                    if (isSuffix(substrings[2]))
                    {
                        FirstName = substrings[0];
                        LastName = substrings[1];
                        Suffix = substrings[2];
                    }
                    else if (isPrefix(substrings[0]))
                    {
                        Prefix = substrings[0];
                        FirstName = substrings[1];
                        LastName = substrings[2];
                    }
                    else
                    {
                        FirstName = substrings[0];
                        MiddleName = substrings[1];
                        LastName = substrings[2];
                    }
                }
                

            }
        }
        private Boolean isSuffix(String item)
        {
            Boolean result = false;
            switch (item.ToUpper())
            {
                case "JR":
                case "JR.":
                case "SR":
                case "SR.":
                    result = true;
                    break;
                default:
                    break;
            }
            return result;
        }
        private Boolean isPrefix(String item)
        {
            Boolean result = false;
            switch (item.ToUpper())
            {
                case "DOCTOR":
                case "DR":
                case "DR.":
                case "MR":
                case "MR.":
                case "MS":
                case "MS.":
                case "MRS":
                case "MRS.":

                    result = true;
                    break;
                default:
                    break;
            }
            return result;
        }
        /// <summary>
        /// Gets the actual Movie Poster for this Movie
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public Image GetProfileImage(ProfileSize size)
        {
            Image x = null;
            if (ProfilePath != null)
            {
                String cachePath = PrivateData.GetAppPath() + @"\Cache\Images\Person\" + ProfilePath.Replace("/", "");

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
        private Uri Uri(ProfileSize size)
        {
            return MakeImageUri(size.ToString(), ProfilePath);
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
            //Need set for persistance to restore 
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
