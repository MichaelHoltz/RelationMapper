using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Utility
{
    public static class AgeHelper
    {
        public static String GetAgeDelta(DateTime birthDate, DateTime? deathDate = null)
        {
            String retVal = String.Empty;
            int age = CalculateAgeYears(birthDate, deathDate);
            if (age == 1)
            {
                retVal = " (" + age + " Year old)";
            }
            else if (age < 0) // Creepy as that would mean we know when they are going to die
            {
                age = Math.Abs(age);  // remove negative sign
                if (age == 1)
                {
                    retVal = " (will be " + age + " Year old)";
                }
                else
                {
                    retVal = " (will be " + age + " Years old)";
                }
            }
            else
            {
                retVal = " (" + age + " Years old)";
            }
            return retVal;
        }
        /// <summary>
        /// Returns a String that takes singularity and dates in the future into account
        /// </summary>
        /// <param name="releaseDate"></param>
        /// <returns></returns>
        public static String GetReleaseDateDelta(DateTime releaseDate)
        {
            String retVal = String.Empty;
            int age = CalculateAgeYears(releaseDate);
            if (age == 1)
            {
                retVal = " (" + age + " Year ago)";
            }
            else if (age < 0)
            {
                age = Math.Abs(age);  // remove negative sign
                if (age == 1)
                {
                    retVal = " (coming in " + age + " Year)";
                }
                else
                {
                    retVal = " (coming in " + age + " Years)";
                }
            }
            else
            {
                retVal = " (" + age + " Years ago)";
            }
            return retVal;
        }
        /// <summary>
        /// Calculates the number of years between Birthdate and now (returns negative if Birthdate is in the future)
        /// </summary>
        /// <param name="birthDate"></param>
        /// <param name="deathDate">Optional death Date - using Now if null</param>
        /// <returns></returns>
        public static int CalculateAgeYears(DateTime birthDate, DateTime? deathDate = null)
        {
            int yearsPassed = 0;
            if (deathDate.HasValue)
            {
                yearsPassed = deathDate.Value.Year - birthDate.Year;
                // Are we before the birth date this year? If so subtract one year from the mix
                if (deathDate.Value.Month < birthDate.Month || (deathDate.Value.Month == birthDate.Month && deathDate.Value.Day < birthDate.Day))
                {
                    yearsPassed--;
                }
            }
            else
            {
                yearsPassed = DateTime.Now.Year - birthDate.Year;
                // Are we before the birth date this year? If so subtract one year from the mix
                if (DateTime.Now.Month < birthDate.Month || (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day))
                {
                    yearsPassed--;
                }
            }
            

            return yearsPassed;
        }
    }
}
