using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// UniverseCrew
    /// </summary>
    public partial class Universe3
    {
        /// <summary>
        /// Application Generated ID for a Crew Role
        /// </summary>
        public int NextCrewIndex { get; set; }

        /// <summary>
        /// All Generic Crew Roles
        /// </summary>
        public HashSet<Crew> Crew { get; set; }
        public HashSet<MovieCrewMap> MovieCrewMap { get; set; }
        /// <summary>
        /// Gets a crew Role and adds it if it doesn't exist.
        /// </summary>
        /// <param name="department"></param>
        /// <param name="job"></param>
        /// <returns></returns>
        public Crew GetCrew(string department, string job)
        {
            Crew c = new Models.Crew(department, job);
            Crew tc = Crew.Where(o => o.Department == department && o.Job == job).FirstOrDefault();
            if (tc != null)
            {
                c = tc;
            }
            else
            {
                c.CrewID = NextCrewIndex++;
                Crew.Add(c); // Add Crew to Universe
            }
            return c;
        }
        public void GetRoleInMovie(string roleName, int movie)
        {

        }
    }
}
