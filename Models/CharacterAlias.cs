using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// Alias of a Character
    /// </summary>
    public class CharacterAlias
    {
        /// <summary>
        /// ID of alias
        /// </summary>
        public int AliasID { get; set; }
        /// <summary>
        /// Name of Alias
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Path to Profile Image (TBD web or local Cache)
        /// </summary>
        public String ProfilePath { get; set; }
        /// <summary>
        /// Path to ThumbNail Image (TBD web or local Cache)
        /// </summary>
        public String ThumbNailPath { get; set; }
    }
}
