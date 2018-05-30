using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovieDb = TmdbWrapper.TheMovieDb;
using TmdbSearch = TmdbWrapper.Search;

namespace RelationMap.Models
{
    /// <summary>
    /// Normalized version of the Universe
    /// </summary>
    public partial class Universe3
    {





        public Universe3()
        {
            StudioGroups = new HashSet<StudioGroup>();
            ProductionCompanies = new HashSet<ProductionCompany>();
            MovieProductionCompanyMap = new HashSet<Models.MovieProductionCompanyMap>();

            Movies = new HashSet<Movie>();
            MovieCollections = new HashSet<MovieCollection>();
            MovieCollectionMap = new HashSet<Models.MovieCollectionMap>();

            People = new HashSet<Person>();
            Characters = new HashSet<Character>();
            MovieCharacterMap = new HashSet<Models.MovieCharacterMap>();
            CharacterAliases = new HashSet<CharacterAlias>();
            CharacterAliasMap = new HashSet<Models.CharacterAliasMap>();

            Crew = new HashSet<Models.Crew>();
            MovieCrewMap = new HashSet<Models.MovieCrewMap>();
        }
        #region People
        public Boolean AddPerson(Person person)
        {
            Boolean result = People.Add(person);
            if (!result) // Movie already there, but need to update (If they aren't already) //But I could be passing in a person with basic info and t
            {
                Person existingPerson = People.First(o => o.Id == person.Id);
                if (existingPerson.Updated)
                {
                    result = true;
                }
                else
                {
                    People.Remove(person); // Hack at update by just replacing. 
                    result = People.Add(person);
                }
            }
            return result;
        }

        #endregion
    }
}
