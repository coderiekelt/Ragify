using Rage;
using System.Drawing;

namespace Ragify.Widgets
{
    public class ProgressWidget : BaseWidget
    {
        float Progress;

        public ProgressWidget(float x, float y, float width, float height) : base(x, y, width, height)
        {
        }

        public override void Think()
        {
            Progress = (float)(Context.TrackTime / GetMappedSize("Base").Width) * GetMappedSize("Base").Width;
            SetMappedSize("Progress", new SizeF(Progress, GetMappedSize("Base").Height));
        }

        public override void Draw(object sender, GraphicsEventArgs args)
        {
            if (!WidgetManager.Drawn["Progress"])
            {
                return;
            }

            base.Draw(sender, args);

            Rage.Graphics Device = args.Graphics;

            if (Context.CurrentTrack != null)
            {
                Device.DrawRectangle(new RectangleF(GetMappedPoint("Base"), GetMappedSize("Progress")), Color.FromArgb(27, 216, 94));
            }
        }
    }
}
