using System;
using System.IO;
using Newtonsoft.Json;

namespace RelationMap.Models
{
    public class PersistanceBase
    {
        public static T Load<T>(string CurrentFile, String DefaultFile = null) where T : new()
        {
            JsonSerializerSettings jss = new JsonSerializerSettings();
            jss.CheckAdditionalContent = true;
            jss.TypeNameHandling = TypeNameHandling.Auto;
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

        public static void Save(String CurrentFile, object obj)
        {
            JsonSerializerSettings jss = new JsonSerializerSettings();
            jss.CheckAdditionalContent = true;
            jss.TypeNameHandling = TypeNameHandling.Auto;
            String json = JsonConvert.SerializeObject(obj, obj.GetType(), Formatting.Indented, jss);

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
            return typeof(PersistanceBase);
        }
    }
}
