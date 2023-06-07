using System;
using System.Drawing;
using System.Numerics;
using Svg.Pathing;

namespace SvgUtils;

public struct PathPoint
{
    public Vector2? In { get; set; }
    public Vector2 Point { get; set; }
    public Vector2? Out { get; set; }

    public PathPoint(Vector2 point)
    {
        In = default;
        Point = point;
        Out = default;
    }
    public PathPoint(Vector2? inControl, Vector2 point, Vector2? outControl)
    {
        In = inControl;
        Point = point;
        Out = outControl;
    }

    public static implicit operator PathPoint(Vector2 pos)
    {
        return new PathPoint() { Point = pos };
    }

    public SvgPathSegment BuildSegmentTo(PathPoint target)
    {
        var offset = target.Point - Point;
        if (Out.HasValue && target.In.HasValue)
        {
            return new SvgCubicCurveSegment(true, Out.Value.ToPointF(), (offset + target.In.Value).ToPointF(),
                offset.ToPointF());
        }
        if (target.In.HasValue)
        {
            return new SvgQuadraticCurveSegment(true, (offset + target.In.Value).ToPointF(), offset.ToPointF());
        }
        if (Out.HasValue)
        {
            return new SvgQuadraticCurveSegment(true, Out.Value.ToPointF(), offset.ToPointF());
        }
        if (MathF.Abs(offset.X) < float.Epsilon)
        {
            return new SvgLineSegment(true, new PointF(float.NaN, offset.Y));
        }
        if (MathF.Abs(offset.Y) < float.Epsilon)
        {
            return new SvgLineSegment(true, new PointF(offset.X, float.NaN));
        }
        return new SvgLineSegment(true, offset.ToPointF());
    }
}