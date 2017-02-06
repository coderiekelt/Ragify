using Rage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Ragify
{
    public static class Config
    {
        public static Dictionary<string, Keys> Mappings;

        public static void MapKey(string slot, string key)
        {
            if (!Mappings.ContainsKey(slot))
            {
                Mappings.Add(slot, (Keys)Enum.Parse(typeof(Keys), key, true));
                return;
            }

            Mappings[slot] = (Keys)Enum.Parse(typeof(Keys), key, true);
        }

        public static Keys GetKey(string slot)
        {
            return Mappings[slot];
        }

        public static void Load()
        {
            Mappings = new Dictionary<string, Keys>();

            InitializationFile IniFile = new InitializationFile("Plugins/Ragify.ini");

            MapKey("nextTrack", IniFile.ReadString("Ragify", "nextTrack", "Numpad3"));
            MapKey("previousTrack", IniFile.ReadString("Ragify", "previousTrack", "Numpad1"));
            MapKey("togglePlayback", IniFile.ReadString("Ragify", "togglePlayback", "Numpad0"));
            MapKey("volumeUp", IniFile.ReadString("Ragify", "volumeUp", "Numpad5"));
            MapKey("volumeDown", IniFile.ReadString("Ragify", "volumeDown", "Numpad2"));
            MapKey("toggleDisplay", IniFile.ReadString("Ragify", "toggleDisplay", "Numpad8"));
        }
    }
}
