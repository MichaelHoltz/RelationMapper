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
    public class MovieCharacterMap
    {
        /// <summary>
        /// ID of Character
        /// </summary>
        public int CharacterId { get; set; }
        /// <summary>
        /// ID of Movie
        /// </summary>
        public int MovieID { get; set; }
        /// <summary>
        /// Order of Character appears in Movie
        /// </summary>
        public int CreditOrder { get; set; }
        /// <summary>
        /// TMDB field related to Seasons and Episodes
        /// </summary>
        public int CastID { get; set; }

        public String CreditID { get; set; }
        /// <summary>
        /// ID of Person in this Role
        /// </summary>
        public int PersonID { get; set; }

    }
}
