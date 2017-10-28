using Rage;
using System.Drawing;
using SpotifyAPI.Local;
using SpotifyAPI.Local.Models;
using System;
using Ragify.Widgets;

[assembly: Rage.Attributes.Plugin("Ragify", Description = "Control Spotify from GTA V", Author = "Riekelt Brands")]
namespace Ragify
{
    public static class EntryPoint
    {
        public static void Main()
        {
            //Widget widget = new Widget();

            //bool RenderHooked = false;

            Context.Initialize();
            Config.Load();
            WidgetManager.Initialize();

            WidgetManager.Register("Track", new TrackWidget(0, 0, 300, 60));
            WidgetManager.Register("Progress", new ProgressWidget(0, 0, 300, 5));
            WidgetManager.Register("Update", new UpdateWidget(0, 0, 300, 30));

            WidgetManager.Drawn["Update"] = false;

            WidgetManager.GetWidget("Track").SetPositionFromBottomRightCorner(new PointF(300, 60));
            WidgetManager.GetWidget("Update").SetPositionFromBottomRightCorner(new PointF(300, 90));
            WidgetManager.GetWidget("Progress").SetPositionFromBottomRightCorner(new PointF(300, 5));

            Rage.Game.DisplayHelp("Ragify has successfully loaded. Use the [CTRL] key in combination with a command to control Spotify.");

            Updates.CheckForUpdates();

            while(true)
            {
                if (Game.IsControlKeyDownRightNow)
                {
                    if (Game.IsKeyDown(Config.GetKey("toggleDisplay")))
                    {
                        if (WidgetManager.Drawn.ContainsKey("Track") && WidgetManager.Drawn.ContainsKey("Progress"))
                        {
                            WidgetManager.Drawn["Track"] = !WidgetManager.Drawn["Track"];
                            WidgetManager.Drawn["Progress"] = !WidgetManager.Drawn["Progress"];
                            Context.Displayed = !Context.Displayed;
                        }
                    }

                    if (Game.IsKeyDown(Config.GetKey("togglePlayback")))
                    {
                        if (Context.Playing)
                        {
                            Game.DisplaySubtitle("Playback paused");
                            Context.Spotify.Pause();
                        } else
                        {
                            Context.Spotify.Play();
                            Game.DisplaySubtitle("Playback resumed");
                        }
                    }

                    if (Game.IsKeyDown(Config.GetKey("nextTrack")))
                    {
                        Context.Spotify.Skip();
                    }

                    if (Game.IsKeyDown(Config.GetKey("previousTrack")))
                    {
                        Context.Spotify.Previous();
                    }

                    if (Game.IsKeyDownRightNow(Config.GetKey("volumeUp")))
                    {
                        float volume = Context.Spotify.GetSpotifyVolume();
                        if (!(volume > 99.0))
                        {
                            volume++;
                            Context.Spotify.SetSpotifyVolume(volume);
                        }
                    }

                    if (Game.IsKeyDownRightNow(Config.GetKey("volumeDown")))
                    {
                        float volume = Context.Spotify.GetSpotifyVolume();

                        if (!(volume < 1.0))
                        {
                            volume--;
                            Context.Spotify.SetSpotifyVolume(volume);
                        }
                    }

                }

                Rage.GameFiber.Yield();
            }
        }


    }
}
