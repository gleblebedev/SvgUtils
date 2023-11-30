using Svg;
using System.Drawing;
using System.Numerics;

namespace SvgUtils;

public class PanelBuilder : SvgAtlasItem
{
    private readonly UIConfiguration _config;
    private readonly Color _color;
    private readonly string _name;
    private readonly PanelLayout _layout;
    private readonly float _radius;

    public PanelBuilder(UIConfiguration config, Color color, float radius, PanelLayout layout, string name = "panel")
    {
        _config = config;
        _color = color;
        _name = name;
        _radius = radius;
        _layout = layout;

        Rect = _layout.AtlasRect.Rect;
    }

    public PanelBuilder(UIConfiguration config, Color color, float radius, string name = "panel"): this(config, color, radius, new PanelLayout(radius, config.SmallPadding, config.SmallPadding, config.StrokeWidth, config.AtlasPadding), name)
    {
    }

    public override void Build(ThemeBuilder themeBuilder)
    {
        _layout.AtlasRect.MoveBy(Rect.Position);

        _layout.Render(themeBuilder, _config, _color, _radius);

        var spriteName = _name;
        var className = _name;
        themeBuilder.AddThemeSprite(spriteName, _layout.ShadowRect.Rect);
        themeBuilder.AddThemeSprite(spriteName + "-inner", _layout.InnerRect.Rect);

        themeBuilder.WriteCssLine($".{className}");
        themeBuilder.WriteCssLine("{");
        themeBuilder.WriteCssLine($"    decorator: ninepatch( {spriteName}, {spriteName}-inner, 1.0 );");

        themeBuilder.WriteCssLine("    box-sizing: border-box;");
        themeBuilder.WriteCssLine("    vertical-align: center;");
        themeBuilder.WriteCssLine("    overflow: auto;");
        themeBuilder.WriteCssLine("    display: inline-block;");
        themeBuilder.WriteCssLine($"    min-width: {_layout.ShadowRect.Rect.Size.X}px;");
        themeBuilder.WriteCssLine($"    min-height: {_layout.ShadowRect.Rect.Size.Y}px;");

        var center = _layout.StrokeRect.Rect.Shrink(_radius * 2);
        var padding = new Padding(_layout.ShadowRect.Rect, center);
        themeBuilder.WriteCssLine($"    padding-top: {padding.Top}px;");
        themeBuilder.WriteCssLine($"    padding-right: {padding.Right}px;");
        themeBuilder.WriteCssLine($"    padding-bottom: {padding.Bottom}px;");
        themeBuilder.WriteCssLine($"    padding-left: {padding.Left}px;");

        themeBuilder.WriteCssLine("}");

    }
}