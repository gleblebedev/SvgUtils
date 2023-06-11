using Svg;

namespace SvgUtils;


public abstract class SvgAtlasItem: IAtlasItem
{
    public abstract void Build(ThemeBuilder themeBuilder);

    public Rect Rect { get; set; }
}