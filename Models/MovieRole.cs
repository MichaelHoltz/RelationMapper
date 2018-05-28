using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// Entity to represent a Role in a movie from Character to Producer
    /// </summary>
    public class MovieRole:Hashable
    {
        public int Id { get; set; }

        public HashSet<String> Aliases { get; set; }

        public MovieRole()
        {
            Aliases = new HashSet<string>();
        }
    }
}
