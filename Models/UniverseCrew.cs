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
    public partial class Universe
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
        public Crew AddGetCrewRole(string department, string job)
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
        /// <summary>
        /// Add A crew member to the movie 
        /// credits.Crew - Adds significant numbers of People and data Entry
        /// </summary>
        public void AddCrewMember(TmdbWrapper.Movies.CrewPerson crewPerson, Movie selectedMovie)
        {
            Person p = new Person(crewPerson.Name);
            p.Id = crewPerson.Id;
            p.ProfilePath = crewPerson.ProfilePath;
            Crew c = AddGetCrewRole(crewPerson.Department, crewPerson.Job);
            AddPerson(p); // Will ONly add new people based on Name and id.
            AddMovieCrewMapping(selectedMovie.TmdbId, crewPerson.Id, c.CrewID, crewPerson.CreditId);
        }

        /// <summary>
        /// Add the mapping of a movie to crew / person / credit 
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="personId"></param>
        /// <param name="crewId"></param>
        /// <param name="creditId"></param>
        /// <returns></returns>
        public Boolean AddMovieCrewMapping(int movieId, int personId, int crewId, string creditId)
        {
            MovieCrewMap mcm = new MovieCrewMap(movieId, personId, crewId, creditId);
            return MovieCrewMap.Add(mcm); // Adding Directly to HashSet false if failed to add
        }

        /// <summary>
        /// Not implemented - Want Person who produced movie as example.
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="movie"></param>
        public void GetRoleInMovie(string roleName, int movie)
        {

        }

    }
}
