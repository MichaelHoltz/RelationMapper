using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// Character ID to Alias ID map
    /// </summary>
    public class CharacterAliasMap
    {
        /// <summary>
        /// Assigned ID of Character 
        /// </summary>
        public int CharacterID { get; set; }
        /// <summary>
        /// Assigned ID of Alias
        /// </summary>
        public int AliasID { get; set; }
    }
}
