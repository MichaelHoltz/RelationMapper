using System;
using System.Threading.Tasks;
using TmdbWrapper.Persons;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Movies
{
    /// <summary>
    /// A member of the cast in a movie.
    /// </summary>
    public class CastPerson : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Id of the person.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Name of the person.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Name of the character that is played.
        /// </summary>
        public string Character { get; private set; }
        /// <summary>
        /// Order of the character in the credits
        /// </summary>
        public int Order { get; private set; }
        /// <summary>
        /// Path of the profile picture of the Person (Not Character)
        /// </summary>
        public string ProfilePath { get; private set; }
        #region Added / Missing
        /// <summary>
        /// Not sure what this is at this time but appears to be tied to character
        /// </summary>
        public int CastId { get; private set; }
        /// <summary>
        /// Appears to be a hash Code for something.. ??
        /// </summary>
        public string CreditId { get; private set; }
        public int Gender { get; private set; }
        #endregion
        #endregion

        #region overrides
        /// <summary>
        /// Returns this instances ToString
        /// </summary>        
        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            Name = jsonObject.GetSafeString("name");
            Character = jsonObject.GetSafeString("character");
            Order = (int)jsonObject.GetSafeNumber("order");
            ProfilePath = jsonObject.GetSafeString("profile_path");
            CastId = (int)jsonObject.GetSafeNumber("cast_id");
            CreditId = jsonObject.GetSafeString("credit_id");
            Gender = (int)jsonObject.GetSafeNumber("gender");
        }
        #endregion

        #region image uri's
        /// <summary>
        /// Uri to the profile image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(ProfileSize size)
        {
            return Extensions.MakeImageUri(size.ToString(), ProfilePath);
        }
        #endregion

        #region navigation properties
        /// <summary>
        /// Retrieves the associated person
        /// </summary>
        public async Task<Person> PersonAsync()
        {
            return await TheMovieDb.GetPersonAsync(Id);
        }
        #endregion
    }
}
