using System.Collections.Generic;
using System.Drawing;

namespace SvgUtils
{
    public static class Program
    {
        public static void Main(string[] args)
        {
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
