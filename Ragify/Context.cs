using SpotifyAPI.Local.Models;
using SpotifyAPI.Local;

namespace Ragify
{
    public static class Context
    {
        public static SpotifyLocalAPI Spotify = null;
        public static Track CurrentTrack = null;
        public static bool Playing = false;
        public static double TrackTime = 0.00;
        public static float Volume = 0.0F;
        public static int HideVolume = 0;
        public static bool Displayed = true;

        public static void Initialize()
        {
            Spotify = new SpotifyLocalAPI();

            if (!SpotifyLocalAPI.IsSpotifyRunning())
            {
                Rage.Game.DisplaySubtitle("[RAGIFY] Spotify is not running!");
                return;
            }

            Spotify.Connect();
            Spotify.ListenForEvents = true;

            if (Spotify.GetStatus().Track != null)
            {
                CurrentTrack = Spotify.GetStatus().Track;
            }

            Playing = Spotify.GetStatus().Playing;

            Spotify.OnPlayStateChange += EventPlayStateChange;
            Spotify.OnTrackChange += EventTrackChange;
            Spotify.OnTrackTimeChange += EventTrackTimeChange;
        }

        private static void EventPlayStateChange(object sender, PlayStateEventArgs args)
        {
            Playing = args.Playing;
        }

        private static void EventTrackChange(object sender, TrackChangeEventArgs args)
        {
            if (args.NewTrack.IsAd())
            {
                return;
            }

            CurrentTrack = args.NewTrack;

            if (!Displayed)
            {
                Rage.Game.DisplaySubtitle(CurrentTrack.ArtistResource.Name + " - " + CurrentTrack.TrackResource.Name);
            }
        }

        private static void EventTrackTimeChange(object sender, TrackTimeChangeEventArgs args)
        {
            TrackTime = args.TrackTime;
        }
    }
}
