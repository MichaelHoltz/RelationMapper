using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// Crew object for Movie Credits
    /// </summary>
    public class Crew
    {
        // GENERAL Crew Identifier
        // "department": "Production",
        // "job": "Producer",

        // Person Attributes
        // "id": 10850,
        // "name": "Kevin Feige",
        // "gender": 2,
        // "profile_path": "/AewbqQQT0FbcE358rcbopZ3zgDV.jpg"

        // Specific for movie    (Movie Crew Map)
        // "credit_id": "544fe93dc3a36802360024f4",


        /// <summary>
        /// Assigned ID for a Crew Member (App Generated)
        /// </summary>
        public int CrewID { get; set; }
        public String Job { get; set; }
        public String Department { get; set; }

        public Crew()
        {
        }
        public Crew(String department, String job)
        {
            Department = department;
            Job = job;
        }
        #region Overrides
        /// <summary>
        /// Returns this instance ToString
        /// </summary>
        public override string ToString()
        {
            return Department + ": " + Job;
        }
        #endregion
        #region HashCodes / Object Identification
        //TODO - use / include the "correct" id..
        private int _hashCode = 0;
        public int HashCode
        {
            get
            {
                return _hashCode == 0 ? generateHashCode() : _hashCode;
            }
            //Need set for persistence to restore 
            set
            {
                _hashCode = value;
            }
        }
        private int generateHashCode()
        {
            //THis is expensive and should be done only once since it will not be changing
            //TODO - use / include the "correct" id..
            String key = this.GetType().Name + Department + Job;
            //Google: "disable fips mode" if the line below fails
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(key));
            int ivalue = BitConverter.ToInt32(hashed, 0);
            return ivalue;

        }
        public override int GetHashCode()
        {
            return HashCode;
        }
        public override bool Equals(object obj)
        {
            return obj.GetHashCode().Equals(HashCode); // == this.GetHashCode();
        }
        #endregion

    }
}
