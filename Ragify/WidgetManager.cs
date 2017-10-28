using Ragify.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragify
{
    public static class WidgetManager
    {
        public static Dictionary<string, BaseWidget> Registered;
        public static Dictionary<string, bool> Drawn;

        public static void Initialize()
        {
            Registered = new Dictionary<string, BaseWidget>();
            Drawn = new Dictionary<string, bool>();
        }

        public static void Register(string name, BaseWidget widget)
        {
            widget.Initialize();

            Rage.Game.FrameRender += widget.Draw;

            Drawn.Add(name, true);
            Registered.Add(name, widget);
        }

        public static BaseWidget GetWidget(string name)
        {
            return Registered[name];
        }

        public static void DisableWidget(string name)
        {
            Drawn[name] = false;
        }

        public static void EnableWidget(string name)
        {
            Drawn[name] = true;
        }
    }
}
