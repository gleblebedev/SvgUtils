using Svg;
using System.Numerics;
using System.Xml.Linq;

namespace SvgUtils;

public class SelectArrowBuilder : SvgAtlasItem
{
    private readonly UIConfiguration _config;

    private PathSet _arrowPath = new PathSet(new Path( new PathPoint[]
    {
        new PathPoint(new Vector2(0f, 0.052992344f)), new PathPoint(new Vector2(1f, 0.052992344f)),
        new PathPoint(new Vector2(0.5f, 0.94700766f))
    }){Closed = true});

    public SelectArrowBuilder(UIConfiguration config)
    {
        _config = config;
        AreaRect = new RectHierarchyNode(new Padding(config.FontSize * 0.5f));
        AtlasRect = new RectHierarchyNode(new Padding(config.AtlasPadding.Y, config.AtlasPadding.X,
            config.AtlasPadding.Y, config.AtlasPadding.X), AreaRect);
        AtlasRect.EvaluateRects();
        Rect = AtlasRect.Rect;
    }

    public RectHierarchyNode AtlasRect { get; set; }

    public RectHierarchyNode AreaRect { get; set; }


    public override void Build(ThemeBuilder themeBuilder)
    {
        AtlasRect.MoveBy(Rect.Position);

        {
            SvgPath path = _arrowPath.ScaleToFit(AtlasRect.Rect);
            path.WithFill(_config.FontColor);
            themeBuilder.Add(path);
        }

        var spriteName = "selectarrow";
        themeBuilder.AddThemeSprite(spriteName, AreaRect.Rect);

        themeBuilder.WriteCssLine("select selectarrow");
        themeBuilder.WriteCssLine("{");
        themeBuilder.WriteCssLine($"    decorator: image( {spriteName} );");
        themeBuilder.WriteCssLine($"    width: {AreaRect.Rect.Size.X}px;");
        themeBuilder.WriteCssLine($"    height: {AreaRect.Rect.Size.Y}px;");
        themeBuilder.WriteCssLine("}");
    }
}