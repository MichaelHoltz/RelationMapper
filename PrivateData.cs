using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
namespace RelationMap
{
    /// <summary>
    /// Collection of functions used to be able to share the code and not give away private data / API Keys sepecifically 
    /// </summary>
    public static class PrivateData
    {
        /// <summary>
        /// Get the key from area not controlled by GIT
        /// </summary>
        /// <returns></returns>
        public static string GetTMDBApiKey()
        {
            String filePath = GetAppPath() + @"\Private\tmdbAPIKey.txt";
            //File Path 
            try
            {
                return File.ReadAllText(filePath);
            }
            catch
            {
                MessageBox.Show(filePath + " Not Found. You need to provide your own API Key for TMDB");
                return "NoAPIKey";
            }
        }
        public static string GetAppPath()
        {
            string[] s = { "\\bin" };
            string path = Application.StartupPath.Split(s, StringSplitOptions.None)[0];
            return path;
        }
        public static string GetRelativePath(String filePath)
        {
            string[] s = { "\\bin" };
            string path = Application.StartupPath.Split(s, StringSplitOptions.None)[0] + filePath;
            return path;
        }

    }
}
