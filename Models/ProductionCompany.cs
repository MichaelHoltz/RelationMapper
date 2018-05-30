using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using TmdbWrapper;
using System.IO;
using System.Net;
using TmdbWrapper.Utilities;
using Newtonsoft.Json;

namespace RelationMap.Models
{
    /// <summary>
    /// Production Companies produce movies and tv shows 
    /// 
    /// This class should represent Company Details
    /// 
    /// Relationships:
    /// Production company is in a StudioGroup collection
    /// Movies and TvShows have production companies
    /// Movies and TvShows MAY have a StudioGroup..
    /// </summary>
    public class ProductionCompany
    {
        #region Basic information contained in movie info
        /// <summary>
        /// TMDB Name of the Production COmpany
        /// </summary>
        [JsonProperty("A")]
        public String Name { get; set; }

        /// <summary>
        /// TMDB Id of the production company
        /// </summary>
        [JsonProperty("B")]
        public int Id { get; set; }
        /// <summary>
        /// Property to avoid looking up a logo that doesn't exist.
        /// </summary>
        [JsonProperty("C")]
        public Boolean HasLogo { get; set; }
        [JsonProperty("D")]
        public String LogoPath { get; set; }
        [JsonProperty("E")]
        public String OriginCountry { get; set; }
        #endregion

        #region Extended information contained in the Company Query
        [JsonProperty("F")]
        public String Description { get; set; }
        [JsonProperty("G")]
        public String Headquarters { get; set; }
        [JsonProperty("I")]
        public Uri Homepage { get; set; }

        /// <summary>
        /// TMDB returns the entire object as part of the Details, but for normalization only the ID should be stored here..
        /// This gets in to Caching and lookup linking 
        /// </summary>
        [JsonProperty("J")]
        public int ParentCompanyId { get; set; }
        /// <summary>
        /// Crude way to prevent many redundant updates. (Also prevents any updates)
        /// </summary>
        [JsonProperty("K")]
        public Boolean Updated { get; set; } 
        #endregion

        public ProductionCompany()
        {
        }
        public ProductionCompany(String pcName, int id, String logoPath, String originCountry)
        {
            Name = pcName;
            Id = id;
            LogoPath = logoPath;
            HasLogo = LogoPath != null;
            OriginCountry = originCountry;
        }

        public async Task<Image> GetLogo(TmdbWrapper.Utilities.LogoSize logoSize)
        {
            return await GetLogo(Id, logoSize, this.LogoPath);
        }
        public async Task<Image> GetLogo(int companyID, TmdbWrapper.Utilities.LogoSize logoSize, String logoPath = null)
        {
            Image x = null;
            if (logoPath == null && HasLogo == true)
            {
                //LogoPath can be null and going out and trying to get it doesn't do anything but add useless time!
                //TODO - need to do something about the time it takes to get Company info over and over..
                TmdbWrapper.Companies.Company c = await TheMovieDb.GetCompanyAsync(companyID); // TMDB Company ID
                logoPath = c.LogoPath;
            }
            
            if (logoPath != null)
            {

                String cachePath = PrivateData.GetAppPath() + @"\Cache\Images\production\" + logoPath.Replace("/", "");

                if (File.Exists(cachePath))
                {
                    x = Image.FromFile(cachePath);
                }
                else
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(cachePath));  // Insure Directory Exists
                    Uri uri = Uri(logoSize);
                    var wc = new WebClient();
                    x = Image.FromStream(wc.OpenRead(uri));
                    x.Save(cachePath); // This should be a separate non-blocking Task
                }
            }
            return x;

        }
        private Uri Uri(LogoSize size)
        {
            return MakeImageUri(size.ToString(), LogoPath);
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
        [JsonIgnore]
        [JsonProperty("H")]
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
            if (obj is int)
            {
               return Id.Equals((int)obj);
            }
            else
            {
                return obj.GetHashCode().Equals(HashCode); // == this.GetHashCode();
            }
        }

        #endregion
    }
}
