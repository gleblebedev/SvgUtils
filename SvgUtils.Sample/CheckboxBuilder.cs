using System;
using Svg;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace SvgUtils;

public class CheckboxBuilder : SvgAtlasItem
{
    private readonly UIConfiguration _config;
    private readonly Color _color;
    private readonly Color _fontColor;
    private readonly bool _isChecked;
    private readonly ButtonState _state;
    private readonly string _name;
    private readonly CheckboxLayout _layout;

    private readonly Path _tickPath = new Path(new PathPoint[] { new PathPoint(new Vector2(0.8521457f, 0.10914159f)), new PathPoint(new Vector2(0.92583966f, 0.18330193f)), new PathPoint(new Vector2(1f, 0.25699615f)), new PathPoint(new Vector2(0.36567163f, 0.8908584f)), new PathPoint(new Vector2(0f, 0.52518654f)), new PathPoint(new Vector2(0.14785457f, 0.37733197f)), new PathPoint(new Vector2(0.36567163f, 0.5951493f)), new PathPoint(new Vector2(0.6091418f, 0.35261178f)) });

    //Path _tickPath = new Path(new PathPoint[] { new PathPoint(new Vector2(0f, 0f)), new PathPoint(new Vector2(1f, 0f)), new PathPoint(new Vector2(0.5f, 1.0f)) });


    public CheckboxBuilder(UIConfiguration config, Color color, bool isChecked, ButtonState state = ButtonState.Normal, string name = "button")
    {
        _config = config;
        _color = color;
        _fontColor = config.FontColor;
        _isChecked = isChecked;
        _state = state;
        _name = name;

        _layout = new CheckboxLayout(config, state);
        Rect = _layout.AtlasRect.Rect;
    }

    public float RectToRadius(Rect rect)
    {
        var minSize = MathF.Min(rect.Size.X, rect.Size.Y);
        return minSize * 0.5f;
    }

    public override void Build(ThemeBuilder themeBuilder)
    {
        _layout.AtlasRect.MoveBy(Rect.Position);

        var darkShadowColor = (_color.ToHSV() * new Vector3(1.0f, 1.0f, 0.8f)).FromHSV();
        var highlightColor = (_color.ToHSV() * new Vector3(1.0f, 0.8f, 1.1f)).FromHSV();

        //{
        //    SvgPath path = Path.Rectangle(_layout.ShadowRect.Rect, RectToRadius(_layout.ShadowRect.Rect));
        //    path.Fill = Color.Black.ToSvg();
        //    path.FillOpacity = 0.5f;
        //    themeBuilder.Add(path);
        //}
        //{
        //    SvgPath path = Path.Rectangle(_layout.StrokeRect.Rect, RectToRadius(_layout.StrokeRect.Rect));
        //    path.WithFill(_color).WithStroke(Color.Black, _config.StrokeWidth);
        //    themeBuilder.Add(path);
        //}
        //{
        //    SvgPath path = Path.Rectangle(_layout.BottomShadowRect.Rect, RectToRadius(_layout.BottomShadowRect.Rect));
        //    path.WithFill(darkShadowColor);
        //    themeBuilder.Add(path);
        //}
        //if (_state != ButtonState.Disabled)
        //{
        //    {
        //        SvgPath path = Path.Rectangle(_layout.HighlightRect.Rect, RectToRadius(_layout.HighlightRect.Rect));
        //        path.WithFill(highlightColor);
        //        themeBuilder.Add(path);
        //    }
        //    {
        //        SvgPath path = Path.Rectangle(_layout.RoundCornersOffset.Rect, RectToRadius(_layout.RoundCornersOffset.Rect));
        //        path.WithFill(_color);
        //        themeBuilder.Add(path);
        //    }
        //}

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
                path.WithFill(highlightColor);
                themeBuilder.Add(path);
            }
            {
                SvgPath path = Path.Rectangle(_layout.RoundCornersOffset.Rect, _config.ButtonCornerRadius);
                path.WithFill(_color);
                themeBuilder.Add(path);
            }
        }

        {
            SvgPath path = _tickPath.ScaleToFit(_layout.InnerAreaRect.Rect);
            path.WithFill(_isChecked?_fontColor: darkShadowColor);
            themeBuilder.Add(path);
        }

        var spriteName = _name;
        var states = new List<string>();

        if (_isChecked)
        {
            switch (_state)
            {
                case ButtonState.Normal:
                    spriteName += "-checked";
                    states.Add($"input.{_name}:checked");
                    break;
                case ButtonState.Disabled:
                    spriteName += "-checked-disabled";
                    states.Add($"input.{_name}:checked:disabled");
                    break;
                case ButtonState.Selected:
                    spriteName += "-checked-hover";
                    states.Add($"input.{_name}:checked:hover");
                    states.Add($"input.{_name}:checked:navigated");
                    break;
                case ButtonState.Pressed:
                    spriteName += "-checked-active";
                    states.Add($"input.{_name}:checked:active");
                    states.Add($"input.{_name}:checked:pressed");
                    break;
            }
        }
        else
        {
            switch (_state)
            {
                case ButtonState.Normal:
                    states.Add($"input.{_name}");
                    break;
                case ButtonState.Disabled:
                    spriteName += "-disabled";
                    states.Add($"input.{_name}:disabled");
                    break;
                case ButtonState.Selected:
                    spriteName += "-hover";
                    states.Add($"input.{_name}:hover");
                    states.Add($"input.{_name}:navigated");
                    break;
                case ButtonState.Pressed:
                    spriteName += "-active";
                    states.Add($"input.{_name}:active");
                    states.Add($"input.{_name}:pressed");
                    break;
            }
        }

        themeBuilder.AddThemeSprite(spriteName, _layout.SpriteRect.Rect);

        for (var index = 0; index < states.Count; index++)
        {
            var state = states[index];
            var comma = (index == states.Count - 1) ? "" : ",";
            themeBuilder.WriteCssLine($"{state}{comma}");
        }

        themeBuilder.WriteCssLine("{");
        themeBuilder.WriteCssLine($"    decorator: image( {spriteName} );");
        if (_state == ButtonState.Normal)
        {
            themeBuilder.WriteCssLine("    box-sizing: border-box;");
            themeBuilder.WriteCssLine("    vertical-align: center;");
            themeBuilder.WriteCssLine("    overflow: auto;");
            themeBuilder.WriteCssLine("    display: inline-block;");
            themeBuilder.WriteCssLine($"    min-width: {_layout.SpriteRect.Rect.Size.X}px;");
            themeBuilder.WriteCssLine($"    min-height: {_layout.SpriteRect.Rect.Size.Y}px;");
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