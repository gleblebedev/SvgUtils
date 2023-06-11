using System.Numerics;
using ExCSS;

namespace SvgUtils;

public struct Padding
{
    public Padding(float padding)
    {
        Top = Bottom = Left = Right = padding;
    }
    
    public Padding(Rect outerRect, Rect innerRect)
    {
        Top = innerRect.Position.Y - outerRect.Position.Y;
        Left = innerRect.Position.X - outerRect.Position.X;
        var rightBottomDiff = outerRect.Position + outerRect.Size - (innerRect.Position + innerRect.Size);
        Bottom = rightBottomDiff.Y;
        Right = rightBottomDiff.X;
    }

    public Padding(float top, float right, float bottom, float left)
    {
        Top = top;
        Right = right;
        Bottom = bottom;
        Left = left;
    }

    public Vector2 LeftTop => new Vector2( Left, Top);
    public Vector2 RightBottom => new Vector2( Right, Bottom);

    public float Top;
    public float Right;
    public float Bottom;
    public float Left;

    public static Padding operator +(Padding a, Padding b)
    {
        return new Padding(a.Top + b.Top, a.Right + b.Right, a.Bottom + b.Bottom, a.Left + b.Left);
    }

    public override string ToString()
    {
        return $"{Top}px {Right}px {Bottom}px {Left}px";
    }
}
