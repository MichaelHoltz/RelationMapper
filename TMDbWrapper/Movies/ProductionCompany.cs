using System;
using System.Threading.Tasks;
using TmdbWrapper.Companies;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Movies
{
    /// <summary>
    /// A production company
    /// </summary>
    public class ProductionCompany : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Name of the production company
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Id of the production company
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Logo Path of the production company
        /// </summary>
        public string LogoPath { get; private set; }
        /// <summary>
        /// Origin Country of the production company
        /// </summary>
        public string OriginCountry { get; set; }

        #endregion

        #region image uri's
        /// <summary>
        /// Uri to the logo image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(LogoSize size)
        {
            return Extensions.MakeImageUri(size.ToString(), LogoPath);
        }
        #endregion
        #region overrides
        /// <summary>
        /// Returns this instance ToString 
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            Name = jsonObject.GetSafeString("name");
            LogoPath = jsonObject.GetSafeString("logo_path");
            OriginCountry = jsonObject.GetSafeString("origin_country");
        }
        #endregion

        #region navigation properties
        /// <summary>
        /// Retrieves the associated company.
        /// </summary>
        public async Task<Company> CompanyAsync()
        {
            return await TheMovieDb.GetCompanyAsync(Id);
        }
        #endregion
    }
}
