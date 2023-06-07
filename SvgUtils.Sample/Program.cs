using System;
using System.Drawing;
using System.Numerics;
using Svg;

namespace SvgUtils
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            float cornerRadius = 12;
            float padding = 8;
            float strokeWidth = 1;
            Vector2 buttonSize = new Vector2(64, 64);
            Vector2 imagePadding = new Vector2(8, 8);
            Vector2 shadowDirection = new Vector2(0, padding);
            Vector2 imageSize = buttonSize + imagePadding * 2;

            //var buttonColor = Color.DodgerBlue;
            var buttonColor = Color.Orange;

            var darkShadowColor = (buttonColor.ToHSV() * new Vector3(1.0f, 1.0f, 0.8f)).FromHSV();
            var hightlightColor = (buttonColor.ToHSV() * new Vector3(1.0f, 0.8f, 1.1f)).FromHSV();
            var bottomColor = (buttonColor.ToHSV() * new Vector3(1.0f, 1.0f, 0.9f)).FromHSV();

            var doc = new SvgDocument()
            {
                Width = imageSize.X,
                Height = imageSize.Y,
                ViewBox = new SvgViewBox(0,0, imageSize.X, imageSize.Y)
            };

            var definitionList = new SvgDefinitionList();
            var group = new SvgGroup();
            doc.Children.Add(definitionList);
            doc.Children.Add(group);

            var buttonRect = new Rect(imagePadding, buttonSize);
            SvgPath shadow =  Path.Rectangle(buttonRect, cornerRadius);
            shadow.Fill = Color.Black.ToSvg();
            shadow.FillOpacity = 0.5f;
            group.Children.Add(shadow);

            buttonRect = new Rect(buttonRect.Position, buttonRect.Size - new Vector2(0, padding)).Shrink(strokeWidth);
            SvgPath outer = Path.Rectangle(buttonRect, cornerRadius);
            outer.Fill = darkShadowColor.ToSvg();
            outer.StrokeWidth = strokeWidth;
            outer.Stroke = Color.Black.ToSvg();
            group.Children.Add(outer);


            buttonRect = new Rect(buttonRect.Position, buttonRect.Size - new Vector2(0, padding)).Shrink(strokeWidth);
            SvgPath inner = Path.Rectangle(buttonRect, cornerRadius);
            inner.Fill = hightlightColor.ToSvg();
            group.Children.Add(inner);

            definitionList.Children.Add(new SvgClipPath(){ID = "innerClip", Children = { inner }});

            {
                var bottomRect = buttonRect.Shrink(new Vector2(padding * 2, 0));
                bottomRect = bottomRect.AlignIn(buttonRect, new Vector2(0.5f, 1.0f))
                    .MoveBy(new Vector2(0, bottomRect.Size.Y * 0.5f));
                var rect = bottomRect;
                var radius = cornerRadius;
                var rx = new Vector2(radius, 0);
                var ry = new Vector2(0, radius);
                var cx = new Vector2(radius * 0.551915944f, 0);
                var cy = new Vector2(0, radius * 0.551915944f);
                var w = new Vector2(rect.Size.X, 0);
                var h = new Vector2(0, rect.Size.Y);
                var points = new PathPoint[]
                {
                    new PathPoint(-cx, rect.Position + rx, null),
                    new PathPoint(null, rect.Position + w - rx, cx),

                    new PathPoint(cy, rect.Position + w - ry, null),
                    new PathPoint(null, rect.Position + w - h, null),
                    new PathPoint(null, rect.Position + w*2 - h, null),
                    new PathPoint(null, rect.Position + w*2 + h, null),
                    new PathPoint(null, rect.Position + h, null),

                    new PathPoint(null, rect.Position + ry, -cy),
                };

                var bottomPath = new Path(points) { Closed = true };
                SvgPath bottom = bottomPath;
                bottom.Fill = bottomColor.ToSvg();
                bottom.ClipPath = new Uri("url(#innerClip)", UriKind.Relative);
                group.Children.Add(bottom);
            }


            doc.Write("button.svg");
            doc.Draw().Save("button.png");
        }


    }
}
