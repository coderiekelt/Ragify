using Rage;
using System.Drawing;

namespace Ragify.Widgets
{
    public class UpdateWidget : BaseWidget
    {
        public int LastCheck = 0;

        public UpdateWidget(float x, float y, float width, float height) : base(x, y, width, height)
        {
        }

        public override void Think()
        {
            SetMappedPoint("Text", new PointF(GetMappedPoint("Base").X + 10, GetMappedPoint("Base").Y + 5));
        }

        public override void Draw(object sender, GraphicsEventArgs args)
        {
            if (!WidgetManager.Drawn["Update"])
            {
                return;
            }

            if (LastCheck == 0)
            {
                LastCheck = Utils.GetCurrentTimestamp() + 2 * 60;
            }

            if (LastCheck < Utils.GetCurrentTimestamp())
            {
                return;
            }

            base.Draw(sender, args);

            Rage.Graphics Device = args.Graphics;

            Device.DrawRectangle(new RectangleF(GetMappedPoint("Base"), GetMappedSize("Base")), Color.FromArgb(200, 41, 128, 185));

            Device.DrawText("An update is available! V" + GetMappedString("Version"), "Arial", 17.0F, GetMappedPoint("Text"), Color.FromArgb(255, 255, 255));
        }
    }
}
