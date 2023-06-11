using Svg;
using System.Drawing;
using System.Numerics;

namespace SvgUtils;

public class PanelBuilder : SvgAtlasItem
{
    private readonly UIConfiguration _config;
    private readonly Color _color;
    private readonly ButtonState _state;
    private readonly string _name;
    private readonly RectHierarchyNode _innerRect;
    private readonly RectHierarchyNode _roundCornersOffset;
    private readonly RectHierarchyNode _highlightRect;
    private readonly RectHierarchyNode _bottomShadowRect;
    private readonly RectHierarchyNode _strokeRect;
    private readonly RectHierarchyNode _shadowRect;
    private readonly RectHierarchyNode _atlasRect;
    private readonly float _radius;

    public PanelBuilder(UIConfiguration config, Color color, float radius, string name = "panel")
    {
        _config = config;
        _color = color;
        _name = name;
        _radius = radius;

        _innerRect = new RectHierarchyNode(new Padding(1));
        _roundCornersOffset = new RectHierarchyNode(new Padding(radius), _innerRect);
        _highlightRect = new RectHierarchyNode(new Padding(config.SmallPadding, 0, 0, 0), _roundCornersOffset);
        _bottomShadowRect = new RectHierarchyNode(new Padding(0, 0, config.SmallPadding, 0), _highlightRect);
        _strokeRect = new RectHierarchyNode(new Padding(config.StrokeWidth * 0.5f), _bottomShadowRect);
        _shadowRect = new RectHierarchyNode(new Padding(config.StrokeWidth * 0.5f) + new Padding(0, 0, config.SmallPadding, 0), _strokeRect);
        _atlasRect = new RectHierarchyNode(new Padding(config.AtlasPadding.Y, config.AtlasPadding.X,
            config.AtlasPadding.Y, config.AtlasPadding.X), _shadowRect);

        _atlasRect.EvaluateRects();
        Rect = _atlasRect.Rect;
    }
    public override void Build(ThemeBuilder themeBuilder)
    {
        _atlasRect.MoveBy(Rect.Position);

        var darkShadowColor = (_color.ToHSV() * new Vector3(1.0f, 1.0f, 0.8f)).FromHSV();
        var hightlightColor = (_color.ToHSV() * new Vector3(1.0f, 0.8f, 1.1f)).FromHSV();

        {
            SvgPath path = Path.Rectangle(_shadowRect.Rect, _radius);
            path.Fill = Color.Black.ToSvg();
            path.FillOpacity = 0.5f;
            themeBuilder.Add(path);
        }
        {
            SvgPath path = Path.Rectangle(_strokeRect.Rect, _radius);
            path.WithFill(_color).WithStroke(Color.Black, _config.StrokeWidth);
            themeBuilder.Add(path);
        }
        {
            SvgPath path = Path.Rectangle(_bottomShadowRect.Rect, _radius);
            path.WithFill(darkShadowColor);
            themeBuilder.Add(path);
        }
        if (_state != ButtonState.Disabled)
        {
            {
                SvgPath path = Path.Rectangle(_highlightRect.Rect, _radius);
                path.WithFill(hightlightColor);
                themeBuilder.Add(path);
            }
            {
                SvgPath path = Path.Rectangle(_roundCornersOffset.Rect, _radius);
                path.WithFill(_color);
                themeBuilder.Add(path);
            }
        }

        var spriteName = _name;
        var className = _name;
        themeBuilder.AddThemeSprite(spriteName, _shadowRect.Rect);
        themeBuilder.AddThemeSprite(spriteName + "-inner", _innerRect.Rect);

        themeBuilder.WriteCssLine($".{className}");
        themeBuilder.WriteCssLine("{");
        themeBuilder.WriteCssLine($"    decorator: ninepatch( {spriteName}, {spriteName}-inner, 1.0 );");

        themeBuilder.WriteCssLine("    box-sizing: border-box;");
        themeBuilder.WriteCssLine("    vertical-align: center;");
        themeBuilder.WriteCssLine("    overflow: auto;");
        themeBuilder.WriteCssLine("    display: inline-block;");
        themeBuilder.WriteCssLine($"    min-width: {_shadowRect.Rect.Size.X}px;");
        themeBuilder.WriteCssLine($"    min-height: {_shadowRect.Rect.Size.Y}px;");

        var center = _strokeRect.Rect.Shrink(_radius * 2);
        var padding = new Padding(_shadowRect.Rect, center);
        themeBuilder.WriteCssLine($"    padding-top: {padding.Top}px;");
        themeBuilder.WriteCssLine($"    padding-right: {padding.Right}px;");
        themeBuilder.WriteCssLine($"    padding-bottom: {padding.Bottom}px;");
        themeBuilder.WriteCssLine($"    padding-left: {padding.Left}px;");

        themeBuilder.WriteCssLine("}");

    }
}