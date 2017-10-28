using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ragify
{
    public static class Updates
    {
        public static bool UpdatesFound;
        public static bool Notified;

        public static string UpdateServer = "http://ragify.riekelt.nu";

        public static string CurrentVersion = "1.3.0";

        public static void CheckForUpdates()
        {
            string webVersion = Get(UpdateServer + "/version.txt");

            if (webVersion.Trim() != CurrentVersion.Trim())
            {
                Notified = true;
                WidgetManager.Drawn["Update"] = true;
                WidgetManager.Registered["Update"].SetMappedString("Version", webVersion);
            }
        }

        private static string Get(string uri)
        {
            using (WebResponse wr = WebRequest.Create(uri).GetResponse())
            {
                using (StreamReader sr = new StreamReader(wr.GetResponseStream()))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
