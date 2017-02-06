using Rage;
using SpotifyAPI.Local.Models;
using System;
using System.Drawing;

namespace Ragify
{
    public class Widget
    {
        private Rage.Graphics Gfx;

        string Name = "Unknown";
        string Artist = "Unknown";
        int ScreenHeight;
        int ScreenWidth;
        int BaseWidth;
        int BaseHeight;
        PointF BasePoint;
        SizeF BaseSize;
        Color BaseColor;
        PointF InfoNamePoint;
        PointF InfoArtistPoint;
        PointF ProgressPoint;
        SizeF ProgressSize;
        float Progress;
        PointF VolumeBackgroundPoint;
        SizeF VolumeBackgroundSize;
        PointF VolumePoint;
        SizeF VolumeSize;
        public bool ShouldDraw;

        public Widget()
        {
            ShouldDraw = true;
            ScreenHeight = Game.Resolution.Height;
            ScreenWidth = Game.Resolution.Width;
            BaseWidth = ScreenWidth - 300;
            BaseHeight = ScreenHeight - 60;
            BasePoint = new PointF(BaseWidth, BaseHeight);
            BaseSize = new SizeF(300, 60);
            BaseColor = Color.FromArgb(200, 40, 40, 40);
            InfoNamePoint = new PointF(BaseWidth + 10, BaseHeight + 10);
            InfoArtistPoint = new PointF(BaseWidth + 10, BaseHeight + 30);
            ProgressPoint = new PointF(BaseWidth, BaseHeight + 55);
            VolumePoint = new PointF(ScreenWidth - 75, BaseHeight + 30);
        }

        public void Draw(object sender, GraphicsEventArgs args)
        {
            if (!ShouldDraw)
            {
                return;
            }

            this.Gfx = args.Graphics;

            // Base Triangle
            Gfx.DrawRectangle(new RectangleF(BasePoint, BaseSize), BaseColor);

            // Meta Info
            if (Context.CurrentTrack != null)
            {
                Name = Context.CurrentTrack.TrackResource.Name;
                Artist = Context.CurrentTrack.ArtistResource.Name;
            }

            Gfx.DrawText(Name, "Arial", 17.0F, InfoNamePoint, Color.FromArgb(255, 255, 255));
            Gfx.DrawText(Artist, "Arial", 12.0F, InfoArtistPoint, Color.FromArgb(255, 255, 255));

            // Track Progress
            if (Context.CurrentTrack != null) {
                Progress = (float)(Context.TrackTime / Context.CurrentTrack.Length) * 300.0F;
                ProgressSize = new SizeF(Progress, 5);
                Gfx.DrawRectangle(new RectangleF(ProgressPoint, ProgressSize), Color.FromArgb(27, 216, 94));
            }
        }
    }
}
