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
    public partial class Universe
    {
        /// <summary>
        /// All Actual People related to movies, TV Shows, and the production of them.
        /// </summary>
        public HashSet<Person> People { get; set; }
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
        public Person GetPerson(String Name)
        {
            return People.FirstOrDefault(o => o.Name == Name);
        }
    }
}
