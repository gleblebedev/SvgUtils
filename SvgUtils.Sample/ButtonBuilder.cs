using System;
using System.Numerics;
using Svg;
using Color = System.Drawing.Color;

namespace SvgUtils;

public class ButtonBuilder: SvgAtlasItem
{
    private readonly UIConfiguration _config;
    private readonly Color _color;
    private readonly ButtonState _state;
    private readonly string _name;

    private readonly ButtonLayout _layout;

    public ButtonBuilder(UIConfiguration config, Color color, ButtonState state = ButtonState.Normal, string name = "button")
    {
        _config = config;
        _color = color;
        _state = state;
        _name = name;

        _layout = new ButtonLayout(config, state);
        Rect = _layout.AtlasRect.Rect;
    }

    public override void Build(ThemeBuilder themeBuilder)
    {
        _layout.AtlasRect.MoveBy(Rect.Position);

        var darkShadowColor = (_color.ToHSV() * new Vector3(1.0f, 1.0f, 0.8f)).FromHSV();
        var hightlightColor = (_color.ToHSV() * new Vector3(1.0f, 0.8f, 1.1f)).FromHSV();

        {
            SvgPath path = Path.Rectangle(_layout.ShadowRect.Rect, _config.ButtonCornerRadius);
            path.Fill = Color.Black.ToSvg();
            path.FillOpacity = 0.5f;
            themeBuilder.Add(path);
        }
        {
            SvgPath path = Path.Rectangle(_layout.StrokeRect.Rect, _config.ButtonCornerRadius);
            path.WithFill(_color).WithStroke(Color.Black, _config.StrokeWidth);
            themeBuilder.Add(path);
        }
        {
            SvgPath path = Path.Rectangle(_layout.BottomShadowRect.Rect, _config.ButtonCornerRadius);
            path.WithFill(darkShadowColor);
            themeBuilder.Add(path);
        }
        if (_state != ButtonState.Disabled)
        {
            {
                SvgPath path = Path.Rectangle(_layout.HighlightRect.Rect, _config.ButtonCornerRadius);
                path.WithFill(hightlightColor);
                themeBuilder.Add(path);
            }
            {
                SvgPath path = Path.Rectangle(_layout.RoundCornersOffset.Rect, _config.ButtonCornerRadius);
                path.WithFill(_color);
                themeBuilder.Add(path);
            }
        }

        var spriteName = _name;
        var className = _name;
        switch (_state)
        {
            case ButtonState.Disabled:
                spriteName += "-disabled";
                className += ":disabled";
                break;
            case ButtonState.Selected:
                spriteName += "-hover";
                className += ":hover";
                break;
            case ButtonState.Pressed:
                spriteName += "-active";
                className += ":active";
                break;
        }
        themeBuilder.AddThemeSprite(spriteName, _layout.SpriteRect.Rect);
        themeBuilder.AddThemeSprite(spriteName + "-inner", _layout.InnerAreaRect.Rect);

        themeBuilder.WriteCssLine($"input.{className}");
        themeBuilder.WriteCssLine("{");
        themeBuilder.WriteCssLine($"    decorator: ninepatch( {spriteName}, {spriteName}-inner, 1.0 );");
        if (_state == ButtonState.Normal)
        {
            themeBuilder.WriteCssLine("    box-sizing: border-box;");
            themeBuilder.WriteCssLine("    vertical-align: center;");
            themeBuilder.WriteCssLine("    overflow: auto;");
            themeBuilder.WriteCssLine("    display: inline-block;");
            themeBuilder.WriteCssLine($"    min-width: {_layout.SpriteRect.Rect.Size.X}px;");
            themeBuilder.WriteCssLine($"    min-height: {_layout.SpriteRect.Rect.Size.Y}px;");
        }

        if (_state != ButtonState.Disabled)
        {
            themeBuilder.WriteCssLine("    font-effect: outline(1px #333), shadow(0px 3px #333);");
            themeBuilder.WriteCssLine("    font-weight: bold;");
        }
        var center = _layout.StrokeRect.Rect.Shrink(_config.ButtonCornerRadius * 2);

        var normalLayout = new ButtonLayout(_config, ButtonState.Normal);
        normalLayout.AtlasRect.MoveBy(Rect.Position);
        var normalCenter = normalLayout.StrokeRect.Rect.Shrink(_config.ButtonCornerRadius * 2);

        center.Size = normalCenter.Size;

        var padding = new Padding(_layout.SpriteRect.Rect, center);
        themeBuilder.WriteCssLine($"    padding-top: {padding.Top}px;");
        themeBuilder.WriteCssLine($"    padding-right: {padding.Right}px;");
        themeBuilder.WriteCssLine($"    padding-bottom: {padding.Bottom}px;");
        themeBuilder.WriteCssLine($"    padding-left: {padding.Left}px;");

        themeBuilder.WriteCssLine("}");

    }
}