using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// Mapping class to allow characters to be shared between movies.
    /// </summary>
    public class CharacterMovieMap
    {
        public int CharacterId { get; set; }
        public int MovieID { get; set; }
        public int CreditOrder { get; set; }

        public HashSet<Movie> MoviesIn { get; set; }

    }
}
