using System.Numerics;

namespace SvgUtils;

public struct Rect
{
    public Vector2 Position;

    public Vector2 Size;
    public Rect(Vector2 position, Vector2 size)
    {
        Position = position;
        Size = size;
    }

    public Rect Shrink(Vector2 offset)
    {
        return new Rect(Position + offset * 0.5f, Size - offset);
    }
    public Rect Shrink(float offset)
    {
        return Shrink(new Vector2(offset, offset));
    }

    public Rect AlignIn(Rect buttonRect, Vector2 vector2)
    {
        return new Rect(buttonRect.Position + (buttonRect.Size - Size)* vector2, Size);
    }

    public Rect MoveBy(Vector2 offset)
    {
        return new Rect(Position + offset, Size);
    }
}