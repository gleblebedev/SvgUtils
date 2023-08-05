using System;

namespace SvgUtils;

public class CheckboxLayout
{
    public CheckboxLayout(UIConfiguration config, ButtonState state)
    {
        float shadowSize = config.MediumPadding;

        switch (state)
        {
            case ButtonState.Selected:
                shadowSize = config.LargePadding;
                break;
            case ButtonState.Pressed:
                shadowSize = config.SmallPadding;
                break;
            case ButtonState.Disabled:
                shadowSize = 0;
                break;
        }
        InnerAreaRect = new RectHierarchyNode(new Padding(config.FontSize*0.5f));
        var roundOffset = MathF.Sqrt(2.0f) - 1.0f;
        RoundCornersOffset = new RectHierarchyNode(InnerAreaRect.Padding * roundOffset, InnerAreaRect);
        HighlightRect = new RectHierarchyNode(new Padding(config.MediumPadding, 0, 0, 0), RoundCornersOffset);
        BottomShadowRect = new RectHierarchyNode(new Padding(0, 0, shadowSize, 0), HighlightRect);
        StrokeRect = new RectHierarchyNode(new Padding(config.StrokeWidth * 0.5f), BottomShadowRect);
        ShadowRect = new RectHierarchyNode(new Padding(config.StrokeWidth * 0.5f) + new Padding(0, 0, shadowSize, 0), StrokeRect);
        SpriteRect = new RectHierarchyNode(new Padding(config.LargePadding - shadowSize, 0, config.LargePadding - shadowSize, 0), ShadowRect);
        AtlasRect = new RectHierarchyNode(new Padding(config.AtlasPadding.Y, config.AtlasPadding.X,
            config.AtlasPadding.Y, config.AtlasPadding.X), SpriteRect);
        AtlasRect.EvaluateRects();
    }

    public RectHierarchyNode InnerAreaRect { get; set; }

    public RectHierarchyNode RoundCornersOffset { get; set; }

    public RectHierarchyNode BottomShadowRect { get; set; }

    public RectHierarchyNode HighlightRect { get; set; }

    public RectHierarchyNode StrokeRect { get; set; }

    public RectHierarchyNode ShadowRect { get; }

    public RectHierarchyNode SpriteRect { get; set; }

    public RectHierarchyNode AtlasRect { get; }
}