using System.Numerics;

namespace SvgUtils;

public class UIConfiguration
{
    /// <summary>
    /// Distance between elements in atlas (to eliminate mip map color bleeding).
    /// </summary>
    public Vector2 AtlasPadding { get; set; } = new Vector2(4, 4);

    /// <summary>
    /// Size of the typical string in pixels.
    /// </summary>
    public float FontSize { get; set; } = 20;

    /// <summary>
    /// Corner radius for the button.
    /// </summary>
    public float ButtonCornerRadius { get; set; } = 12;

    /// <summary>
    /// A base padding all other paddings are evaluated from.
    /// </summary>
    public float MediumPadding { get; set; } = 4;

    public float OuterPanelCornerRadius => OuterPanelCornerRadius * 2;
    public float InnerPanelCornerRadius => ButtonCornerRadius * 2;
    public float LargePadding => MediumPadding * 2;
    public float SmallPadding => MediumPadding / 2;
    public float StrokeWidth { get; set; } = 1;
    

    //public Vector2 MinNinePatchSize { get; set; } = new Vector2(4, 4);
}