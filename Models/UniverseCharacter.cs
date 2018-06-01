using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelationMap.Models
{
    /// <summary>
    /// UniverseCharacter - Code dealing with Character Relationships
    /// </summary>
    public partial class Universe
    {
        public int NextCharacterIndex { get; set; }
        
        /// <summary>
        /// All characters in all movies - (Seems could be role like Character or Crew - Producer etc.)
        /// </summary>
        public HashSet<Character> Characters { get; set; }
        public HashSet<MovieCharacterMap> MovieCharacterMap { get; set; }

        public int NextAliasIndex { get; set; }
        public HashSet<CharacterAlias> CharacterAliases { get; set; }
        public HashSet<CharacterAliasMap> CharacterAliasMap { get; set; }
        private String[] getSplitName(String name, Char delimiter)
        {
            return name.Split(delimiter);
        }
        private String getNicName(String name)
        {
            String retVal = String.Empty; // Default to no NicName
            String[] substrings = getSplitName(name, ' ');
            foreach (String namePart in substrings)
            {
                if (namePart == "~")
                {
                    break;
                }
                if (namePart.StartsWith("'") && namePart.EndsWith("'"))
                {
                    retVal = namePart.Substring(1, namePart.Length - 2) + " "; // Just the name without single quotes and a space
                    continue; // Skip the rest of the loop
                }
                //Add the remaining nameParts to comprise nick name
                if (retVal.Length > 0) 
                {
                    retVal += namePart + " ";
                }
            }
            return retVal.Trim();
        }
        private String removeIllegalCharacters(String name)
        {
            //Character issues:
            // /\:*?"<>| are a problem if used for filename
            name = name.Replace('\"', '\''); //Replace Double Quote with Single Quote before they get in the system.
            name = name.Replace('/', '~'); //Replace Slash with tilde before they get in the system.
            return name;

        }
        /// <summary>
        /// Function to Auto Alias
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private String addAutoAliases(String name)
        {
            String newCharacterName = name;
            if (name.Contains("(uncredited)"))
            {
                //Make up regular character name and alias the uncredited
                newCharacterName = name.Replace("(uncredited)", "").Trim();
                name = newCharacterName + " ~ " + name;
            }
            ////Need to auto Alias quoted Name.. Ex. Virginia 'Pepper' Potts -> Pepper Potts or Lt. Col. James 'Rhodey' Rodes ~ War Machine -> Rhodey Rodes.
            //if (name.)
            return newCharacterName;
        }

        private Character matchByRemovingFirst(String fullName)
        {
            Character c = null;
            String[] substrings = getSplitName(fullName, ' ');
            
            //Try ends with (Removes the first substring)
            //if (substrings.Count() > 2)
            if (substrings.Count() > 1)
            {
                //Hack to fix bad match for "Actor x" vs "x"
                if (substrings[0] == "Actor")
                {
                    return c;
                }
                String endswith = String.Empty;
                for (int i = 1; i < substrings.Count(); i++)
                {
                    endswith += substrings[i] + " ";
                }
                endswith = endswith.Trim();
                c = GetCharacter(endswith); // Look up Direct match against fullName ends with
                if (c != null)
                {
                    if (MessageBox.Show("Is " + c.Name + " the same character as " + fullName + "?", "Confirm Character Identity", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        c = null;
                        return c; // Not same Character
                    }
                    else
                    {
                        bool result = addAlias(endswith, c);
                    }

                    
                    //Prompt to update Character..
                    if (MessageBox.Show("Update " + c.Name + " to " + fullName + "?", "Confirm Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        c.Name = fullName;
                    }
                }
            }
            else
            {
                c = getCharacterEndingWith(fullName);
                if (c != null)
                {
                    if (MessageBox.Show("Is " + c.Name + " the same character as " + fullName + "?", "Confirm Character Identity", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        c = null;// Not same Character
                        //return c; // Not same Character
                    }
                }
            }
            return c;
        }
        private Character matchByRemovingLast(String fullName)
        {
            Character c = null;
            String[] substrings = getSplitName(fullName, ' ');
            if (substrings.Count() > 1) // Don't know why I'm using substrings but it's working so I don't want to mess with it.
            {
                c = getCharacterEndingWith(fullName); // Look up Direct match against ends with
                if (c != null)
                {
                    if (MessageBox.Show("Is " + c.Name + " the same character as " + fullName + "?", "Confirm Character Identity", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        c = null;
                        return c; // Not same Character
                    }
                    else
                    {
                        bool result = addAlias(fullName, c);
                    }
                    
                    //Prompt to update Character..
                    if (c.Name.Length < fullName.Length)
                    {
                        String newFullName = c.Name + fullName.Replace(fullName + " ~", " ~");
                        if (MessageBox.Show("Update " + c.Name + " to " + fullName + "?", "Confirm Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            c.Name = fullName;
                        }
                    }
                }

            }
            else
            {

                c = getCharacterStartingWith(fullName);
                if (c != null)
                {
                    if (MessageBox.Show("Is " + c.Name + " the same character as " + fullName + "?", "Confirm Character Identity", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        c = null;// Not same Character
                        //return c; // Not same Character
                    }
                }
            }

            return c;
        }

        public Character AddCharacter(String characterName, int movieID, int personID, string creditID, int castID, int creditOrder)
        {
            //Save what was passed in - mostly for debugging
            String originalCharacterName = characterName; //Not to be modified

            ////Debugging
            //if (characterName.Contains("Soldier"))
            //{
            //    //Walk through code
            //}
            //Standardize on what characters can be in the name
            characterName = removeIllegalCharacters(characterName);
            //Save OriginalCharacterName for use with Credit Mapping
            String originalCleanCharacterName = characterName; //Not to be modified


            //Tag awareness!!
            //(voice)  - 
            //(uncredited) characters should be just the character and have an alias ..
            //However the Original characterName must be preserved for the mapping

            String nicName = getNicName(characterName);
            String newCharacterName = characterName;
            if (characterName.Contains("(uncredited)"))
            {
                //Make up regular character name and alias the uncredited
                newCharacterName = characterName.Replace("(uncredited)", "").Trim();
                characterName = newCharacterName + " ~ " + characterName; 
            }
            if (nicName.Length > 0)
            {
                characterName += " ~ " + nicName;
            }


            //Try Direct Exact Match
            Character c = GetCharacter(characterName);

            //No Direct Match try direct match to Alias
            if (c==null)
            {
                //See if there are Aliases to Search.
                HashSet<CharacterAlias> cas = findOrAddAliases(characterName, newCharacterName);
                //In the case of Bruce Banner ~ The Hulk vs Bruce Banner ~ Hulk 
                //Bruce Banner already existed (as does The Hulk) so we want to use Bruce Banner and Add the new Alias
                //Also Possibly update the Character
                foreach (CharacterAlias ca in cas)
                {
                    CharacterAliasMap cam = CharacterAliasMap.FirstOrDefault(o => o.AliasID == ca.AliasId);
                    if (cam != null)
                    {
                        c = GetCharacter(cam.CharacterID);
                        if (c != null)
                        {
                            //BUG = way out of hand on prompts here.. 
                            //Prompt to update Character..
                            //Merge old and new 
                            // characterName = characterName.Replace(c.Name, "");
                            //String newFullName = c.Name + characterName.Replace(newCharacterName + " ~", " ~");
                            if (c.Name != newCharacterName) // No Point in asking if identical!
                            {
                                if (MessageBox.Show("Is " + c.Name + " the same character as " + newCharacterName + "?", "Confirm Character Identity", MessageBoxButtons.YesNo) == DialogResult.No)
                                {
                                    c = null;// Not same Character
                                    continue;
                                    //return c; // Not same Character
                                }

                                if (c.Name.Length < newCharacterName.Length)
                                {
                                    if (MessageBox.Show("Update " + c.Name + " to " + newCharacterName + "?", "Confirm Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        c.Name = newCharacterName;
                                    }
                                }
                            }
                            break;
                        }
                    }
                    else //Character Alias Map is null
                    {
                        //If an Alias was added in this movie but the base character didn't have any aliases then either
                        //the Character Alias Map should have individual character names in it to find the Character or Original Character Name
                        //from MovieCharacterMap map.
                        //Very iffy for Generic names 
                        MovieCharacterMap mcmt = MovieCharacterMap.FirstOrDefault(o => o.OriginalCharacterName == ca.Name);
                        if (mcmt != null)
                        {
                            c = GetCharacter(mcmt.CharacterId);
                            if (c != null)
                            {
                                if (MessageBox.Show("Is " + c.Name + " the same character as " + newCharacterName + "?", "Confirm Character Identity", MessageBoxButtons.YesNo) == DialogResult.No)
                                {
                                    c = null;// Not same Character
                                    continue;
                                    //return c; // Not same Character
                                }
                                //Prompt to update Character..
                                if (!characterName.Contains("uncredited"))
                                {
                                    String newFullName = c.Name + characterName.Replace(newCharacterName + " ~", " ~");
                                    if (MessageBox.Show("Update " + c.Name + " to " + characterName + "?", "Confirm Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        c.Name = characterName;
                                    }
                                }
                                break;
                            }
                        }
                    }
                }

                //Try to see if it's a case of Maria Hill vs Agent Maria Hill
                if (c == null)
                {
                    c = matchByRemovingFirst(characterName);
                    if (c!= null && MessageBox.Show("Is " + c.Name + " the same character as " + characterName + "?", "Confirm Character Identity", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        c = null;// Not same Character
                    }
                    if (c == null)
                    {
                        c = matchByRemovingLast(characterName);
                        if (c != null && MessageBox.Show("Is " + c.Name + " the same character as " + characterName + "?", "Confirm Character Identity", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            c = null;// Not same Character
                        }
                    }
                }

                //Try Direct Match to characterName parts (Alias)
                if (c == null)
                {
                    //Try to match to characterName parts (Alias)
                    String[] substrings = getSplitName(characterName, '~');
                    if (substrings.Count() > 1)
                    {
                        foreach (String item in substrings)
                        {
                            c = GetCharacter(item.Trim());
                            if (c != null)
                            {
                                if (MessageBox.Show("Is " + c.Name + " the same character as " + characterName + "?", "Confirm Character Identity", MessageBoxButtons.YesNo) == DialogResult.No)
                                {
                                    c = null;// Not same Character
                                    continue;
                                }
                                if (MessageBox.Show("Update " + c.Name + " to " + characterName + "?", "Confirm Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    c.Name = characterName; // Update
                                }
                                
                                cas = findOrAddAliases(characterName, newCharacterName);
                                break;
                            }
                        }
                    }
                }
                //If c is still null - Add a new Character..
                if (c == null)
                {
                    c = new Character();
                    c.Name = newCharacterName; // Want Clean Name if one exists
                    c.Id = NextCharacterIndex++;
                    Characters.Add(c);
                }
                //Need a Character Before Alias mapping can be done
                if (cas.Count > 0 && c != null)
                {
                    //Already have Aliases but need to map them..
                    foreach (CharacterAlias ca in cas)
                    {
                        if (ca.Name == c.Name)
                        {
                            continue; // Don't make and alias to Actual Character Name
                        }
                        CharacterAliasMap cam = new Models.CharacterAliasMap();
                        cam.AliasID = ca.AliasId;
                        cam.CharacterID = c.Id;
                        bool result = CharacterAliasMap.Add(cam);
                    }
                }

            }

            //Add Mapping of Character / Actor / Role
            //Mapping to cleaned up Character Name but preserving originalCharacterName for Character Movie relation.
            MovieCharacterMap mcm = new MovieCharacterMap(c.Id, originalCleanCharacterName, movieID, creditOrder, castID, creditID, personID);
            MovieCharacterMap.Add(mcm); // Add to Universe Map.

            return c;
        }
        private Boolean addAlias(String aliasName, ref CharacterAlias ca)
        {
            ca = new CharacterAlias();
            ca.Name = aliasName;
            ca.AliasId = NextAliasIndex++;
            //Don't have the extended properties available from TMDB
            return CharacterAliases.Add(ca); //Try to add to CharacterAliases

        }
        private Boolean addAlias(String aliasName, Character c)
        {
            CharacterAlias ca = new CharacterAlias();
            bool result1 = addAlias(aliasName, ref ca);
            bool result2 = false;
            //Add Alias to map need to map them..
            if (result1)
            {
                CharacterAliasMap cam = new Models.CharacterAliasMap();
                cam.AliasID = ca.AliasId;
                cam.CharacterID = c.Id;
                result2 = CharacterAliasMap.Add(cam);
            }
            return result1 && result2;
        }
        /// <summary>
        /// Function to split names into aliases
        /// </summary>
        /// <param name="characterName"></param>
        private HashSet<CharacterAlias> findOrAddAliases(String characterName, String newCharacterName)
        {
            HashSet<CharacterAlias> retVal = new HashSet<CharacterAlias>(); // Default to empty set.
            //Name = characterName;
            String[] substrings = getSplitName(characterName, '~');
            if (substrings.Count() > 1)
            {
                foreach (String item in substrings)
                {
                    string aliasName = item.Trim(); // Trim before Lookup
                    //Find Exact Match on Alias by Name Only
                    CharacterAlias ca = CharacterAliases.FirstOrDefault(o => o.Name == aliasName);
                    bool success = false;
                    if (ca == null)
                    {
                        if (aliasName != newCharacterName)
                        {
                            ca = new CharacterAlias();
                            ca.Name = aliasName;
                            ca.AliasId = NextAliasIndex++;
                            success = addAlias(aliasName, ref ca); //Try to add to CharacterAliases
                            if (success)
                            {
                                retVal.Add(ca);
                            }
                        }
                    }
                    else
                    {
                        retVal.Add(ca);
                    }
                    //if (success)
                    //{
                    //    retVal.Add(ca);
                    //}
                    //else
                    //{
                    //    Console.WriteLine("Debug!! findOrAddAliases: " + ca.Name + " failed to be added to CharacterAliases");
                    //}
                }
            }
            else
            { //At least part of this is needed to match "Rhodey Rodes" to full name
                CharacterAlias ca = CharacterAliases.FirstOrDefault(o => o.Name == characterName);
                bool success = false;
                if (ca == null)
                {
                    ca = new CharacterAlias();
                    ca.Name = characterName;
                    ca.AliasId = NextAliasIndex++;

                    //success = addAlias(characterName, ref ca); //Try to add to CharacterAliases (Creates an Alias for a single character and that is not desired.)
                    success = true; //Debug - Seems to work for War Machine.
                }
                else
                {
                    success = true;
                }
                if (success)
                {
                    retVal.Add(ca);
                }
                else
                {
                    Console.WriteLine("Debug!! findOrAddAliases: " + ca.Name + " failed to be added to CharacterAliases");
                }

            }
            return retVal;
        }
        private String identifyCharacterFromTMDBAliasList(String aliasList)
        {
            //aliasList is Slash separated list
            HashSet<String> Aliases = new HashSet<string>();
            if (aliasList != null)
            {
                String[] substrings = getSplitName(aliasList, '~');
                foreach (String item in substrings)
                {
                    Aliases.Add(item.Trim());
                }
            }
            return aliasList;
        }
//        private void AddAlias(String characterAliasName, int CharacterID)
//        {

//        }
        /// <summary>
        /// Get Character by exact match on name
        /// </summary>
        /// <param name="characterName"></param>
        /// <returns></returns>
        public Character GetCharacter(String characterName)
        {
            return Characters.FirstOrDefault(o => o.Name == characterName); //Direct Exact Match
        }
        /// <summary>
        /// Get Character by Character Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Character GetCharacter(int id)
        {
            return Characters.FirstOrDefault(o => o.Id == id);
        }
        private Character getCharacterStartingWith(String startsWithName)
        {
            return Characters.FirstOrDefault(o => o.Name.StartsWith(startsWithName)); // Look up Direct match against starts with
        }
        private Character getCharacterEndingWith(String endsWithName)
        {
            return Characters.FirstOrDefault(o => o.Name.EndsWith(endsWithName)); // Look up Direct match against ends with
        }


        public HashSet<Character> GetCharactersPlayedByActor(int actorId)
        {
            HashSet<Character> cs = new HashSet<Character>();
            foreach (MovieCharacterMap mcm in MovieCharacterMap.Where(o => o.PersonId == actorId))
            {
                Character mc = Characters.FirstOrDefault(o => o.Id == mcm.CharacterId);
                if(mc != null)
                    cs.Add(mc);
            }
            return cs;
        }
        public HashSet<Character> GetCharactersPlayedByActor(int actorId, int movieId)
        {
            HashSet<Character> cs = new HashSet<Character>();
            foreach (MovieCharacterMap mcm in MovieCharacterMap.Where(o => o.PersonId == actorId && o.MovieId == movieId))
            {
                String ocn = mcm.OriginalCharacterName;
                Character mc = Characters.FirstOrDefault(o => o.Id == mcm.CharacterId);
                if (mc != null)
                {
                    mc.OriginalCharacterName = ocn;
                    cs.Add(mc);
                }
            }
            return cs;
        }
        private class cMCM
        {
            public int MovieId { get; set; }
            public int CharacterId { get; set; }
            //public int PersonId { get; set; }
        }
        public List<Character> GetTopCharacters(int limit=20)
        {
            List<Character> retVal = new List<Character>();
            var consolidatedMCM =
                                from c in MovieCharacterMap
                                group c by new
                                {
                                    c.MovieId,
                                    c.CharacterId,
                                    //c.PersonId,
                                } into gcs
                                select new cMCM()
                                {
                                    MovieId = gcs.Key.MovieId,
                                    CharacterId = gcs.Key.CharacterId,
                                    //PersonId = gcs.Key.PersonId
                                };
            //var step1 = MovieCharacterMap.OrderByDescending(o => o.CreditOrder).ThenBy(o => o.MovieId).Where(o => o.CreditOrder < 20).GroupBy(o => o.CharacterId);
            var step1 = consolidatedMCM.OrderBy(o => o.MovieId).GroupBy(o => o.CharacterId);
            //Who appears in the most movies..

            var step2 = step1.OrderByDescending(g => g.Count());
            var step3 = step2.SelectMany(g => g).Select(o=>o.CharacterId);
            var step4 = step3.Distinct().ToList();

            //Have a Distinct List of Character ID's 
            foreach (int id in step4)
            {
                if (limit-- < 0)
                    break;
                retVal.Add(GetCharacter(id));
            }
            return retVal;
        }

    }
}
