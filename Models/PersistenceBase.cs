using System;
using System.IO;
using Newtonsoft.Json;

namespace RelationMap.Models
{
    public class PersistenceBase
    {
        public static T Load<T>(string CurrentFile, String DefaultFile = null) where T : new()
        {
            JsonSerializerSettings jss = new JsonSerializerSettings();
            jss.CheckAdditionalContent = true;
            jss.TypeNameHandling = TypeNameHandling.Auto;
            jss.NullValueHandling = NullValueHandling.Ignore;
            T retval;
            //Load Settings from Specified File if it exists.
            if (File.Exists(CurrentFile))
            {
                String json = File.ReadAllText(CurrentFile);
                retval = JsonConvert.DeserializeObject<T>(json, jss);
            }
            else if (File.Exists(DefaultFile)) //Load Settings from Default File
            {
                String json = File.ReadAllText(DefaultFile);
                retval = JsonConvert.DeserializeObject<T>(json, jss);
            }
            else
            {
                //Have a problem and returning a Null object..
                //retVal =(T)(object)null;

                //Have a problem and returning a New Object
                retval = new T();
            }
            return retval;
        }

        public static void Save(String CurrentFile, object obj, bool compact = false)
        {
            JsonSerializerSettings jss = new JsonSerializerSettings();
            jss.CheckAdditionalContent = true;
            jss.TypeNameHandling = TypeNameHandling.Auto;
            jss.NullValueHandling = NullValueHandling.Ignore;

            String json = null;
            if (compact)
            {
                json = JsonConvert.SerializeObject(obj, obj.GetType(), Formatting.None, jss); // Save Space
            }
            else
            {
                json = JsonConvert.SerializeObject(obj, obj.GetType(), Formatting.Indented, jss); // Readable
            }

            if (Directory.Exists(Path.GetDirectoryName(CurrentFile)))
            {

                File.WriteAllText(CurrentFile, json);
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(CurrentFile));
                File.WriteAllText(CurrentFile, json);
            }
        }
        public static Type GetMyObjectClassType()
        {
            return typeof(PersistenceBase);
        }
    }
}
