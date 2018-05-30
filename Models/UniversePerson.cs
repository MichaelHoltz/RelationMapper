using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// UniversePerson
    /// </summary>
    public partial class Universe3
    {
        /// <summary>
        /// All Actual People related to movies, TV Shows, and the production of them.
        /// </summary>
        public HashSet<Person> People { get; set; }

        public Person GetPerson(String Name)
        {
            return People.FirstOrDefault(o => o.Name == Name);
        }
    }
}
