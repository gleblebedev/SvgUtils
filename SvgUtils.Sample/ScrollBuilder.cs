using System.Drawing;
using System.Numerics;

namespace SvgUtils;

public class ScrollBuilder : SvgAtlasItem
{
    private readonly UIConfiguration _config;
    private readonly Color _backgroundColor;
    private readonly Color _sliderColor;
    private readonly float _radius;
    private readonly string _name;
    private readonly PanelLayout _slidertrack;
    private readonly PanelLayout _sliderbar;

    public ScrollBuilder(UIConfiguration config, Color backgroundColor, Color sliderColor, float radius, string name = null)
    {
        _config = config;
        _backgroundColor = backgroundColor;
        _sliderColor = sliderColor;
        _radius = radius;
        _name = name;
        _slidertrack = new PanelLayout(radius, -config.MediumPadding, 0.0f, config.AtlasPadding);
        _sliderbar = new PanelLayout(radius, config.MediumPadding, 1.0f, config.AtlasPadding);
        _sliderbar.AtlasRect.MoveBy(new Vector2(_slidertrack.AtlasRect.Rect.Size.X, 0));
        Rect = _slidertrack.AtlasRect.Rect.Union(_sliderbar.AtlasRect.Rect);
    }

    public override void Build(ThemeBuilder themeBuilder)
    {
        _slidertrack.AtlasRect.MoveBy(Rect.Position);
        _sliderbar.AtlasRect.MoveBy(Rect.Position);

        _slidertrack.Render(themeBuilder, _config, _backgroundColor, _radius);
        _sliderbar.Render(themeBuilder, _config, _sliderColor, _radius);
        var spritePrefix = (_name == null) ? "" : $"{_name}-";
        themeBuilder.AddThemeSprite(spritePrefix+"slidertrack", _slidertrack.ShadowRect.Rect);
        themeBuilder.AddThemeSprite(spritePrefix+"slidertrack-inner", _slidertrack.InnerRect.Rect);

        themeBuilder.AddThemeSprite(spritePrefix+"sliderbar", _sliderbar.ShadowRect.Rect);
        themeBuilder.AddThemeSprite(spritePrefix+"sliderbar-inner", _sliderbar.InnerRect.Rect);

        var stylePrefix = (_name == null) ? "" : $"input.{_name} ";
        if (_name == null)
        {
            themeBuilder.WriteCssLine("scrollbarvertical");
            themeBuilder.WriteCssLine("{");
            themeBuilder.WriteCssLine($"    width: {_slidertrack.ShadowRect.Rect.Size.X}px;");
            themeBuilder.WriteCssLine("}");
            themeBuilder.WriteCssLine("scrollbarhorizontal");
            themeBuilder.WriteCssLine("{");
            themeBuilder.WriteCssLine($"    height: {_slidertrack.ShadowRect.Rect.Size.Y}px;");
            themeBuilder.WriteCssLine("}");
        }
        themeBuilder.WriteCssLine(stylePrefix + "slidertrack");
        themeBuilder.WriteCssLine("{");
        if (_name != null)
        {
            themeBuilder.WriteCssLine($"    height: {_sliderbar.ShadowRect.Rect.Size.Y*0.5f}px;");
        }
        themeBuilder.WriteCssLine($"    decorator: ninepatch( {spritePrefix}slidertrack, {spritePrefix}slidertrack-inner, 1.0 );");
        themeBuilder.WriteCssLine("}");
        themeBuilder.WriteCssLine(stylePrefix + "sliderbar");
        themeBuilder.WriteCssLine("{");
        if (_name != null)
        {
            themeBuilder.WriteCssLine($"    margin-top: {-_sliderbar.ShadowRect.Rect.Size.Y*0.25f}px;");
            themeBuilder.WriteCssLine($"    width: {_sliderbar.ShadowRect.Rect.Size.X}px;");
            themeBuilder.WriteCssLine($"    height: {_sliderbar.ShadowRect.Rect.Size.Y}px;");
        }
        themeBuilder.WriteCssLine($"    decorator: ninepatch( {spritePrefix}sliderbar, {spritePrefix}sliderbar-inner, 1.0 );");
        themeBuilder.WriteCssLine("}");

        //themeBuilder.WriteCssLine("    box-sizing: border-box;");
        //themeBuilder.WriteCssLine("    vertical-align: center;");
        //themeBuilder.WriteCssLine("    overflow: auto;");
        //themeBuilder.WriteCssLine("    display: inline-block;");
        //themeBuilder.WriteCssLine($"    min-width: {_layout.ShadowRect.Rect.Size.X}px;");
        //themeBuilder.WriteCssLine($"    min-height: {_layout.ShadowRect.Rect.Size.Y}px;");

        //var center = _layout.StrokeRect.Rect.Shrink(_radius * 2);
        //var padding = new Padding(_layout.ShadowRect.Rect, center);
        //themeBuilder.WriteCssLine($"    padding-top: {padding.Top}px;");
        //themeBuilder.WriteCssLine($"    padding-right: {padding.Right}px;");
        //themeBuilder.WriteCssLine($"    padding-bottom: {padding.Bottom}px;");
        //themeBuilder.WriteCssLine($"    padding-left: {padding.Left}px;");


    }
}