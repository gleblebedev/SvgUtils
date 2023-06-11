using System.Numerics;

namespace SvgUtils;

public class RectHierarchyNode
{
    public RectHierarchyNode(Padding padding, RectHierarchyNode? inner = null)
    {
        Padding = padding;
        InnerRect = inner;
    }

    public Padding Padding { get; set; }

    public Rect Rect { get; private set; }

    public RectHierarchyNode? InnerRect { get; set; }

    public void EvaluateRects()
    {
        EvaluateRectRecursive();
        MoveBy(-Rect.Position);
    }

    private void EvaluateRectRecursive()
    {
        Rect inner = new Rect();
        if (InnerRect != null)
        {
            InnerRect.EvaluateRectRecursive();
            inner = InnerRect.Rect;
        }

        Rect = new Rect(inner.Position - Padding.LeftTop, inner.Size + Padding.LeftTop + Padding.RightBottom);
    }

    public RectHierarchyNode MoveBy(Vector2 offset)
    {
        Rect = Rect.MoveBy(offset);
        InnerRect?.MoveBy(offset);
        return this;
    }
}