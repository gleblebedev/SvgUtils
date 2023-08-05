using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Svg;

namespace SvgUtils
{
    public static class ExtensionMethods
    {
        private static readonly Dictionary<Color, SvgColourServer> _colorServers = new Dictionary<Color, SvgColourServer>();

        public static Vector2 ToVector2(this SvgPoint point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Vector2 ToVector2(this PointF point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static SvgPoint ToPoint(this Vector2 point, SvgUnitType type = SvgUnitType.User)
        {
            return new SvgPoint()
            {
                X = new SvgUnit(type, point.X),
                Y = new SvgUnit(type, point.Y),
            };
        }
        public static PointF ToPointF(this Vector2 point)
        {
            return new PointF()
            {
                X = point.X,
                Y = point.Y,
            };
        }

        public static SvgColourServer ToSvg(this System.Drawing.Color color)
        {
            if (_colorServers.TryGetValue(color, out var colorServer))
                return colorServer;
            colorServer = new SvgColourServer(color);
            _colorServers.Add(color, colorServer);
            return colorServer;
        }

        public static SvgElement WithFill(this SvgElement element, Color color)
        {
            element.Fill = color.ToSvg();
            element.FillOpacity = color.A / 255.0f;
            return element;
        }

        public static SvgElement WithStroke(this SvgElement element, Color color, float width)
        {
            element.Stroke = color.ToSvg();
            element.StrokeWidth = width;
            return element;
        }

        public static Vector3 ToHSV(this Color color)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));
            var h = color.GetHue()/360.0f;
            var s = (max == 0) ? 0 : 1f - (1f * min / max);
            var v = max / 255f;
            return new Vector3(h, s, v);
        }
        public static Color FromHSV(this Vector3 hsv)
        {
            hsv = Vector3.Clamp(hsv, Vector3.Zero, Vector3.One);
            int hi = Convert.ToInt32(Math.Floor(hsv.X * 6)) % 6;
            double f = hsv.X * 6 - Math.Floor(hsv.X * 6);

            var value = hsv.Z * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - hsv.Y));
            int q = Convert.ToInt32(value * (1 - f * hsv.Y));
            int t = Convert.ToInt32(value * (1 - (1 - f) * hsv.Y));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        public static Vector3 ToVector3(this Color color)
        {
            return new Vector3(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f);
        }
        public static Color ToColor(this Vector3 color)
        {
            return Color.FromArgb(255, Math.Clamp((int)(color.X*255.0f),0,255), Math.Clamp((int)(color.Y * 255.0f), 0, 255), Math.Clamp((int)(color.Z * 255.0f), 0, 255));
        }
    }
}