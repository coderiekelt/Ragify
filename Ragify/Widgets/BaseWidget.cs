using Rage;
using System.Collections.Generic;
using System.Drawing;

namespace Ragify.Widgets
{
    public class BaseWidget
    {
        float ScreenWidth;
        float ScreenHeight;

        PointF Position;
        SizeF Size;

        private Dictionary<string, PointF> LocalPoints;
        private Dictionary<string, SizeF> LocalSizes;
        private Dictionary<string, string> LocalStrings;

        public BaseWidget(float x, float y, float width, float height)
        {
            LocalPoints = new Dictionary<string, PointF>();
            LocalSizes = new Dictionary<string, SizeF>();
            LocalStrings = new Dictionary<string, string>();

            ScreenHeight = Rage.Game.Resolution.Height;
            ScreenWidth = Rage.Game.Resolution.Width;

            Position = new PointF(x, y);
            Size = new SizeF(width, height);

            SetMappedPoint("Base", Position);
            SetMappedSize("Base", Size);
        }

        public virtual void Initialize()
        {

        }

        public virtual void Think()
        {

        }

        public virtual void Draw(object sender, GraphicsEventArgs args)
        {
            Think();
        }

        // Points methods
        public virtual void SetMappedPoint(string name, PointF point)
        {
            if (!LocalPoints.ContainsKey(name))
            {
                LocalPoints.Add(name, point);
                return;
            }

            LocalPoints[name] = point;
        }

        public virtual PointF GetMappedPoint(string name)
        {
            return LocalPoints[name];
        }

        // Sizes methods
        public virtual void SetMappedSize(string name, SizeF point)
        {
            if (!LocalSizes.ContainsKey(name))
            {
                LocalSizes.Add(name, point);
                return;
            }

            LocalSizes[name] = point;
        }

        public virtual SizeF GetMappedSize(string name)
        {
            return LocalSizes[name];
        }

        // Strings methods
        public virtual void SetMappedString(string name, string content)
        {
            if (!LocalStrings.ContainsKey(name))
            {
                LocalStrings.Add(name, content);
                return;
            }

            LocalStrings[name] = content;
        }

        public virtual string GetMappedString(string name)
        {
            return LocalStrings[name];
        }

        // Positioning voids
        public virtual void SetPositionFromBottomLeftCorner(PointF position)
        {
            SetMappedPoint("Base", new PointF(position.X, ScreenHeight - position.Y));
        }

        public virtual void SetPositionFromBottomRightCorner(PointF position)
        {
            SetMappedPoint("Base", new PointF(ScreenWidth - position.X, ScreenHeight - position.Y));
        }

        public virtual void SetPositionFromTopRightCorner(PointF position)
        {
            SetMappedPoint("Base", new PointF(ScreenWidth - position.X, position.Y));
        }

        public virtual void SetPositionFromTopLeftCorner(PointF position)
        {
            SetMappedPoint("Base", new PointF(position.X, position.Y));
        }
    }
}
