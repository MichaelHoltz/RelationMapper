using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Search;
using TmdbWrapper.Utilities;
namespace TmdbWrapper.Companies
{
    /// <summary>
    /// Michael Holtz added as it isn't just an id any more..
    /// </summary>
    public class ParentCompany: ITmdbObject
    {
        #region properties
        /// <summary>
        /// Id of this company
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Name of this company.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Path of the logo for this company
        /// </summary>
        public string LogoPath { get; private set; }
        #endregion
        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            LogoPath = jsonObject.GetSafeString("logo_path");
            Name = jsonObject.GetSafeString("name");
        }
        #endregion
    }
}
