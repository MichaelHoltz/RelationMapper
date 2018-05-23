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


namespace RelationMap.Models
{
    /// <summary>
    /// Production Companies produce movies and tv shows 
    /// 
    /// This class should represent Company Details
    /// 
    /// Relationships:
    /// Production company is in a studio collection
    /// Movies and TvShows have production companies
    /// Movies and TvShows MAY have a Studio..
    /// </summary>
    public class ProductionCompany
    {
        /// <summary>
        /// TMDB Name of the Production COmpany
        /// </summary>
        public String Name { get; set; }
        
        /// <summary>
        /// TMDB Id of the production company
        /// </summary>
        public int Id { get; set; }


        public String Description { get; set; }
        public String Headquarters { get; set; }
        public Uri Homepage { get; set; }
        public String LogoPath { get; set; }
        public String OriginCountry { get; set; }
        /// <summary>
        /// TMDB returns the entire object as part of the Details, but for normalization only the ID should be stored here..
        /// This gets in to Caching and lookup linking 
        /// </summary>
        public int ParentCompanyId { get; set; }

        public ProductionCompany()
        {
        }

        public ProductionCompany(String pcName)
        {
            Name = pcName;
        }
        public ProductionCompany(String pcName, int id)
        {
            Name = pcName;
            Id = id;
        }

        public async Task<Image> GetLogo(TmdbWrapper.Utilities.LogoSize logoSize)
        {
            return await GetLogo(Id, logoSize, this.LogoPath);
        }
        public static async Task<Image> GetLogo(int companyID, TmdbWrapper.Utilities.LogoSize logoSize, String logoPath = null)
        {
            Image x = null;
            //TODO - need to do something about the time it takes to get Company info over and over..
            TmdbWrapper.Companies.Company c = await TheMovieDb.GetCompanyAsync(companyID); // TMDB Company ID
            
            if (c.LogoPath != null)
            {

                String cachePath = PrivateData.GetAppPath() + @"\Cache\Images\production\" + c.LogoPath.Replace("/", "");

                if (File.Exists(cachePath))
                {
                    x = Image.FromFile(cachePath);
                }
                else
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(cachePath));  // Insure Directory Exists
                    Uri uri = c.Uri(logoSize);
                    var wc = new WebClient();
                    x = Image.FromStream(wc.OpenRead(uri));
                    x.Save(cachePath); // This should be a seperate non-blocking Task
                }
            }
            return x;

        }

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
