using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// UniverseStudios
    /// </summary>
    public partial class Universe3
    {
        /// <summary>
        /// A StudioGroup is a collection of Production Companies 
        /// Ex. "Marvel" is Marvel Enterprises, Marvel Studios, Marvel Entertainment. 
        /// The purpose is to address that a "Marvel" movie is actually all the above and even side Production Companies Like Disney but the common thread
        /// is Marvel movie is intended, vs the minutia of the full Production Company name for a given movie.
        /// </summary>
        public HashSet<StudioGroup> StudioGroups { get; set; }

        /// <summary>
        /// All Production Companies which produce a movie 
        /// </summary>
        public HashSet<ProductionCompany> ProductionCompanies { get; set; }

        public HashSet<MovieProductionCompanyMap> MovieProductionCompanyMap { get; set; }

        #region Production Company 

        public ProductionCompany AddProductionCompany(String pcName, int pcId, String logoPath, String originCountry)
        {
            ProductionCompany pc = new ProductionCompany(pcName, pcId, logoPath, originCountry);
            Boolean result = AddProductionCompany(pc);
            return pc;
        }
        public Boolean AddMovieToProductionCompany(int pcID, int movieID)
        {
            return false;
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
            //ProductionCompany pc = null;
            
            return ProductionCompanies.Where(o => o.Id == pcId).FirstOrDefault();
        }
        /// <summary>
        /// Get all Production Companies for a given MovieID
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public HashSet<ProductionCompany> GetProductionCompanies(int movieId)
        {
            HashSet<ProductionCompany> retVal = new HashSet<ProductionCompany>();
            var map = MovieProductionCompanyMap.Where(o => o.MovieId == movieId);
            foreach (var item in map)
            {
                ProductionCompany pc = ProductionCompanies.FirstOrDefault(o=>o.Id == item.ProductionCompanyId);
                if (pc != null)
                {
                    retVal.Add(pc);
                }
            }
            return retVal;
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
    }
}
