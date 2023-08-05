using Svg;
using System;
using System.Drawing;
using System.Numerics;

namespace SvgUtils;

public class PanelLayout
{
    public RectHierarchyNode InnerRect { get; }
    public RectHierarchyNode RoundCornersOffset { get; }
    public RectHierarchyNode HighlightRect { get; }
    public RectHierarchyNode BottomShadowRect { get; }
    public RectHierarchyNode StrokeRect { get; }
    public RectHierarchyNode ShadowRect { get; }
    public RectHierarchyNode AtlasRect { get; }

    public PanelLayout(float cornerRadius, float shadowWidth, float strokeWidth, Vector2 atlasPadding)
    {
        var radius = cornerRadius;
        InnerRect = new RectHierarchyNode(new Padding(1));
        RoundCornersOffset = new RectHierarchyNode(new Padding(radius), InnerRect);
        if (shadowWidth > 0)
        {
            HighlightRect = new RectHierarchyNode(new Padding(shadowWidth, 0, 0, 0), RoundCornersOffset);
            BottomShadowRect = new RectHierarchyNode(new Padding(0, 0, shadowWidth, 0), HighlightRect);
        }
        else
        {
            HighlightRect = new RectHierarchyNode(new Padding(0, 0, -shadowWidth, 0), RoundCornersOffset);
            BottomShadowRect = new RectHierarchyNode(new Padding(-shadowWidth, 0, 0, 0), HighlightRect);
        }
        StrokeRect = new RectHierarchyNode(new Padding(strokeWidth * 0.5f), BottomShadowRect);
        ShadowRect = new RectHierarchyNode(new Padding(strokeWidth * 0.5f) + new Padding(0, 0,  MathF.Max(0.0f, shadowWidth), 0), StrokeRect);
        AtlasRect = new RectHierarchyNode(new Padding(atlasPadding.Y, atlasPadding.X,
            atlasPadding.Y, atlasPadding.X), ShadowRect);
        AtlasRect.EvaluateRects();
    }

    public void Render(ThemeBuilder themeBuilder, UIConfiguration config, Color color, float radius)
    {
        var darkShadowColor = (color.ToHSV() * new Vector3(1.0f, 1.0f, 0.8f)).FromHSV();
        var hightlightColor = (color.ToHSV() * new Vector3(1.0f, 0.8f, 1.1f)).FromHSV();

        {
            SvgPath path = Path.Rectangle(ShadowRect.Rect, radius);
            path.Fill = Color.Black.ToSvg();
            path.FillOpacity = 0.5f;
            themeBuilder.Add(path);
        }
        {
            SvgPath path = Path.Rectangle(StrokeRect.Rect, radius);
            path.WithFill(color).WithStroke(Color.Black, config.StrokeWidth);
            themeBuilder.Add(path);
        }
        {
            SvgPath path = Path.Rectangle(BottomShadowRect.Rect, radius);
            path.WithFill(darkShadowColor);
            themeBuilder.Add(path);
        }
        {
            SvgPath path = Path.Rectangle(HighlightRect.Rect, radius);
            path.WithFill(hightlightColor);
            themeBuilder.Add(path);
        }
        {
            SvgPath path = Path.Rectangle(RoundCornersOffset.Rect, radius);
            path.WithFill(color);
            themeBuilder.Add(path);
        }
    }
}