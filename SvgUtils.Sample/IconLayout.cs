namespace SvgUtils;

public class IconLayout
{
    public RectHierarchyNode InnerAreaRect { get; set; }
    public RectHierarchyNode AtlasRect { get; }

    public IconLayout(UIConfiguration config)
    {
        InnerAreaRect = new RectHierarchyNode(new Padding(config.FontSize * 0.5f));
        AtlasRect = new RectHierarchyNode(new Padding(config.AtlasPadding.Y, config.AtlasPadding.X,
            config.AtlasPadding.Y, config.AtlasPadding.X), InnerAreaRect);
        AtlasRect.EvaluateRects();
    }
}