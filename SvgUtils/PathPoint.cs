using System;
using System.Drawing;
using System.Globalization;
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
        if (Out.HasValue && target.In.HasValue)
        {
            return new SvgCubicCurveSegment(true, (Out.Value - Point).ToPointF(), (target.In.Value - Point).ToPointF(),
                (target.Point - Point).ToPointF());
        }
        if (target.In.HasValue)
        {
            return new SvgQuadraticCurveSegment(true, (target.In.Value - Point).ToPointF(), (target.Point - Point).ToPointF());
        }
        if (Out.HasValue)
        {
            return new SvgQuadraticCurveSegment(true, (Out.Value - Point).ToPointF(), (target.Point - Point).ToPointF());
        }

        var offset = (target.Point - Point);
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

    public override string ToString()
    {
        var val = $@"new Vector2({Point.X.ToString(CultureInfo.InvariantCulture)}f, {Point.Y.ToString(CultureInfo.InvariantCulture)}f)";
        if (!In.HasValue && !Out.HasValue)
        {
            return $@"new PathPoint({val})";
        }
        var inVal = In.HasValue ? $@"new Vector2({In.Value.X.ToString(CultureInfo.InvariantCulture)}f, {In.Value.Y.ToString(CultureInfo.InvariantCulture)}f)" : "null";
        var outVal = Out.HasValue ? $@"new Vector2({Out.Value.X.ToString(CultureInfo.InvariantCulture)}f, {Out.Value.Y.ToString(CultureInfo.InvariantCulture)}f)" : "null";
        return $@"new PathPoint({inVal}, {val}, {outVal})";
    }
}