using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    public class CharacterImageSearchResults
    {
        public List<CharacterImageSearchResult> CharacterImageSearchResultsList { get; set; }
        public CharacterImageSearchResults()
        {
            CharacterImageSearchResultsList = new List<CharacterImageSearchResult>();
        }
    }
}
