using Rage;
using System.Drawing;

namespace Ragify.Widgets
{
    public class TrackWidget : BaseWidget
    {
        string Name;
        string Artist;

        public TrackWidget(float x, float y, float width, float height) : base(x, y, width, height)
        {
        }

        public override void Initialize()
        {
            Name = "Unknown";
            Artist = "Unknown";
        }

        public override void Think()
        {
            SetMappedPoint("Name", new PointF(GetMappedPoint("Base").X + 10, GetMappedPoint("Base").Y + 10));
            SetMappedPoint("Artist", new PointF(GetMappedPoint("Base").X + 10, GetMappedPoint("Base").Y + 30));

            if (Context.CurrentTrack != null)
            {
                Name = Context.CurrentTrack.TrackResource.Name;
                Artist = Context.CurrentTrack.ArtistResource.Name;
            }
        }

        public override void Draw(object sender, GraphicsEventArgs args)
        {
            if (!WidgetManager.Drawn["Track"])
            {
                return;
            }

            base.Draw(sender, args);

            Rage.Graphics Device = args.Graphics;

            Device.DrawRectangle(new RectangleF(GetMappedPoint("Base"), GetMappedSize("Base")), Color.FromArgb(200, 40, 40, 40));

            Device.DrawText(Name, "Arial", 17.0F, GetMappedPoint("Name"), Color.FromArgb(255, 255, 255));
            Device.DrawText(Artist, "Arial", 12.0F, GetMappedPoint("Artist"), Color.FromArgb(255, 255, 255));
        }
    }
}
