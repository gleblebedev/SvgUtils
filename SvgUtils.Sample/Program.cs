using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using Svg;

namespace SvgUtils
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //var sampleDoc = SvgDocument.Open(@"C:\github\SvgUtils\Samples\tick.svg");
            //var layer = sampleDoc.Children.GetSvgElementOf<SvgGroup>();
            //foreach (var child in layer.Children)
            //{
            //    var id = child.ID;
            //    var path = new Path(child.Children.FindSvgElementOf<SvgPath>());
            //    path = path.ScaleToFit(new Rect(Vector2.One)).EliminateControlPoints();
            //}
            //return;

            var config = new UIConfiguration();
            var items = new List<SvgAtlasItem>()
            {
                new ButtonBuilder(config, Color.DodgerBlue, ButtonState.Normal, "blue-button"),
                new ButtonBuilder(config, Color.DodgerBlue, ButtonState.Selected, "blue-button"),
                new ButtonBuilder(config, Color.DodgerBlue, ButtonState.Pressed, "blue-button"),
                new ButtonBuilder(config, Color.DodgerBlue, ButtonState.Disabled, "blue-button"),
                new ButtonBuilder(config, Color.Orange, ButtonState.Normal, "orange-button"),
                new ButtonBuilder(config, Color.Orange, ButtonState.Selected, "orange-button"),
                new ButtonBuilder(config, Color.Orange, ButtonState.Pressed, "orange-button"),
                new ButtonBuilder(config, Color.Orange, ButtonState.Disabled, "orange-button"),
                new PanelBuilder(config, Color.FromArgb(255,20,22,36), config.InnerPanelCornerRadius, "gray-inner-panel"),
                new PanelBuilder(config, Color.FromArgb(255,20,22,36), config.OuterPanelCornerRadius, "gray-outer-panel"),
                new ScrollBuilder(config, Color.Gray, Color.DarkGray, config.ButtonCornerRadius*0.5f),
                new CheckboxBuilder(config, Color.DodgerBlue, false, ButtonState.Normal,"blue-checkbox"),
                new CheckboxBuilder(config, Color.DodgerBlue, false, ButtonState.Selected,"blue-checkbox"),
                new CheckboxBuilder(config, Color.DodgerBlue, false, ButtonState.Pressed,"blue-checkbox"),
                new CheckboxBuilder(config, Color.DodgerBlue, false, ButtonState.Disabled,"blue-checkbox"),
                new CheckboxBuilder(config, Color.DodgerBlue, true, ButtonState.Normal,"blue-checkbox"),
                new CheckboxBuilder(config, Color.DodgerBlue, true, ButtonState.Selected,"blue-checkbox"),
                new CheckboxBuilder(config, Color.DodgerBlue, true, ButtonState.Pressed,"blue-checkbox"),
                new CheckboxBuilder(config, Color.DodgerBlue, true, ButtonState.Disabled,"blue-checkbox"),
                new ScrollBuilder(config, Color.DodgerBlue, Color.DodgerBlue, config.ButtonCornerRadius, "blue-slider"),
                new SelectArrowBuilder(config),
                new IconBuilder(config, Color.DodgerBlue, IconBuilder.Settings, "icon-settings"),
                new IconBuilder(config, Color.DodgerBlue, IconBuilder.XBox, "icon-xbox"),
            };
            var imageSize = items.BuildAtlas();

            var theme = new ThemeBuilder(imageSize.Size+imageSize.Position);
            foreach (var item in items)
            {
                item.Build(theme);
            }

            theme.Save();
        }
    }
}
