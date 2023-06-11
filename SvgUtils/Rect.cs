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

    public Rect(Vector2 size)
    {
        Position = Vector2.Zero;
        Size = size;
    }
    public Rect(float width, float height)
    {
        Position = Vector2.Zero;
        Size = new Vector2(width, height);
    }

    public float Area => Size.X * Size.Y;

    public Rect Shrink(Vector2 offset)
    {
        return Shrink(offset, new Vector2(0.5f, 0.5f));
    }
    public Rect Grow(Vector2 offset)
    {
        return Shrink(-offset);
    }
    public Rect Shrink(Vector2 offset, Vector2 pivot)
    {
        return new Rect(Position + offset * pivot, Vector2.Max(Vector2.Zero, Size - offset));
    }
    public Rect Shrink(float offset)
    {
        return Shrink(new Vector2(offset, offset));
    }
    public Rect Shrink(float offsetX, float offsetY)
    {
        return Shrink(new Vector2(offsetX, offsetY));
    }
    public Rect Grow(float offset)
    {
        return Shrink(new Vector2(-offset, -offset));
    }
    public Rect Shrink(float offset, Vector2 pivot)
    {
        return Shrink(new Vector2(offset, offset), pivot);
    }

    public Rect AlignIn(Rect buttonRect, Vector2 vector2)
    {
        return new Rect(buttonRect.Position + (buttonRect.Size - Size)* vector2, Size);
    }

    public Rect MoveBy(Vector2 offset)
    {
        return new Rect(Position + offset, Size);
    }

    public bool FitsInto(Rect valueRect)
    {
        return Size.X <= valueRect.Size.X + float.Epsilon && Size.Y <= valueRect.Size.Y + float.Epsilon;
    }

    public override string ToString()
    {
        return $"x:{Position.X}, y:{Position.Y}, w:{Size.X}, h:{Size.Y}";
    }
}