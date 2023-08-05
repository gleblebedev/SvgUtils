using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        var states = new List<string>();

        switch (_state)
        {
            case ButtonState.Normal:
                states.Add($"input.{_name}");
                states.Add($"select.{_name}");
                states.Add($"select.{_name} selectbox");
                break;
            case ButtonState.Disabled:
                spriteName += "-disabled";
                states.Add($"input.{_name}:disabled");
                states.Add($"select.{_name} selectbox");
                break;
            case ButtonState.Selected:
                spriteName += "-hover";
                states.Add($"input.{_name}:hover");
                states.Add($"input.{_name}:navigated");
                states.Add($"select.{_name}:hover");
                states.Add($"select.{_name}:navigated");
                break;
            case ButtonState.Pressed:
                spriteName += "-active";
                states.Add($"input.{_name}:active");
                states.Add($"input.{_name}:pressed");
                states.Add($"select.{_name}:active");
                states.Add($"select.{_name}:pressed");
                break;
        }
        themeBuilder.AddThemeSprite(spriteName, _layout.SpriteRect.Rect);
        themeBuilder.AddThemeSprite(spriteName + "-inner", _layout.InnerAreaRect.Rect);

        for (var index = 0; index < states.Count; index++)
        {
            var state = states[index];
            var comma = (index == states.Count - 1) ? "" : ",";
            themeBuilder.WriteCssLine($"{state}{comma}");
        }

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
            themeBuilder.WriteCssLine("    text-align: center;");
            themeBuilder.WriteCssLine("    font-effect: outline(1px #333), shadow(0px 3px #333);");
            //themeBuilder.WriteCssLine("    font-weight: bold;");
        }

        if (_state == ButtonState.Disabled)
        {
            themeBuilder.WriteCssLine("    font-effect: none;");
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

        themeBuilder.WriteCssLine(string.Join(","+Environment.NewLine, states.Where(_=>_.StartsWith("select."))));
        var halfFont = _config.FontSize * 0.5f;
        themeBuilder.WriteCssLine("{");
        themeBuilder.WriteCssLine("    text-align: left;");
        themeBuilder.WriteCssLine($"    padding-top: {padding.Top}px;");
        themeBuilder.WriteCssLine($"    padding-right: {padding.Right}px;");
        themeBuilder.WriteCssLine($"    padding-bottom: {padding.Bottom + _config.FontSize}px;");
        themeBuilder.WriteCssLine($"    padding-left: {padding.Left }px;");
        themeBuilder.WriteCssLine("}");

        if (_state == ButtonState.Normal)
        {
            themeBuilder.WriteCssLine($"select.{_name} selectvalue");
            themeBuilder.WriteCssLine("{");
            themeBuilder.WriteCssLine($"    padding-right: {_config.FontSize*3}px;");
            themeBuilder.WriteCssLine("}");
            themeBuilder.WriteCssLine($"select.{_name} selectbox option:hover");
            themeBuilder.WriteCssLine("{");
            themeBuilder.WriteCssLine("    font-effect: outline(1px #333), shadow(0px 3px #333);");
            themeBuilder.WriteCssLine($"    background: #{_color.R:X2}{_color.G:X2}{_color.B:X2};");
            themeBuilder.WriteCssLine("}");
        }
    }
}