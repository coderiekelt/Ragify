using Rage;
using System.Drawing;
using SpotifyAPI.Local;
using SpotifyAPI.Local.Models;
using System;

[assembly: Rage.Attributes.Plugin("Ragify", Description = "Control Spotify from GTA V", Author = "Riekelt Brands")]
namespace Ragify
{
    public static class EntryPoint
    {
        public static void Main()
        {
            Widget widget = new Widget();

            bool RenderHooked = false;

            Context.Initialize();
            Config.Load();
            Rage.Game.DisplayHelp("Ragify has successfully loaded. Use the [CTRL] key in combination with a command to control Spotify.");

            while(true)
            {
                if (!Game.IsLoading && !RenderHooked)
                {
                    Game.FrameRender += widget.Draw;
                    RenderHooked = true;
                }

                if (Game.IsControlKeyDownRightNow)
                {
                    if (Game.IsKeyDown(Config.GetKey("toggleDisplay")))
                    {
                        widget.ShouldDraw = !widget.ShouldDraw;
                        Context.Displayed = !Context.Displayed;
                    }

                    if (Game.IsKeyDown(Config.GetKey("togglePlayback")))
                    {
                        if (Context.Playing)
                        {
                            Game.DisplaySubtitle("Playback paused.");
                            Context.Spotify.Pause();
                        } else
                        {
                            Context.Spotify.Play();
                            Game.DisplaySubtitle("Playback resumed.");
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
                        if (volume >= 100.0)
                        {
                            return;
                        }

                        volume += 0.1F;
                        try
                        {
                            Context.Spotify.SetSpotifyVolume(volume);
                        } catch (Exception e) {}
                        Context.HideVolume = Utils.GetCurrentTimestamp() + 4;
                        Context.Volume = volume;
                    }

                    if (Game.IsKeyDownRightNow(Config.GetKey("volumeDown")))
                    {
                        float volume = Context.Spotify.GetSpotifyVolume();

                        if (volume <= 0.0)
                        {
                            return;
                        }

                        volume -= 0.1F;
                        try
                        {
                            Context.Spotify.SetSpotifyVolume(volume);
                        }
                        catch (Exception e) { }
                        Context.HideVolume = Utils.GetCurrentTimestamp() + 4;
                        Context.Volume = volume;
                    }

                }

                Rage.GameFiber.Yield();
            }
        }


    }
}
