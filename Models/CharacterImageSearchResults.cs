using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// Class for storing a collection of Google Image Search Results
    /// </summary>
    public class CharacterImageSearchResults
    {
        public List<CharacterImageSearchResult> CharacterImageSearchResultsList { get; set; }
        public CharacterImageSearchResults()
        {
            CharacterImageSearchResultsList = new List<CharacterImageSearchResult>();
        }
    }
}
