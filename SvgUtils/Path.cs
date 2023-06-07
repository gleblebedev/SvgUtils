using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Svg;
using Svg.Pathing;

namespace SvgUtils;

public class Path: IReadOnlyList<PathPoint>
{
    private List<PathPoint> _points = new List<PathPoint>();

    public Path(params PathPoint[] points)
    {
        _points.AddRange(points);
    }

    public Path(IEnumerable<PathPoint> points)
    {
        _points.AddRange(points);
    }

    public Path()
    {
    }

    public bool Closed { get; set; }

    public static implicit operator SvgPath(Path path)
    {
        var data = new SvgPathSegmentList { new SvgMoveToSegment(true, path[0].Point.ToPointF()) };

        for (int index = 0; index < path.Count-1; ++index)
        {
            data.Add(path[index].BuildSegmentTo(path[index + 1]));
        }

        if (path.Closed)
        {
            var lastSegment = path[path.Count-1].BuildSegmentTo(path[0]);
            if (!(lastSegment is SvgLineSegment))
            {
                data.Add(lastSegment);
            }
            data.Add(new SvgClosePathSegment(true));
        }
        return new SvgPath(){PathData = data };
    }

    public IEnumerator<PathPoint> GetEnumerator()
    {
        return _points.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_points).GetEnumerator();
    }

    public int Count => _points.Count;

    public PathPoint this[int index] => _points[index];

    public static Path Rectangle(Rect rect)
    {
        return new Path(rect.Position, rect.Position + new Vector2(rect.Size.X, 0), rect.Position + rect.Size, rect.Position + new Vector2(0, rect.Size.Y)) {Closed = true};
    }

    public static Path Rectangle(Rect rect, float radius)
    {
        if (radius < float.Epsilon)
            return Rectangle(rect);

        var rx = new Vector2(radius, 0);
        var ry = new Vector2(0, radius);
        var cx = new Vector2(radius * 0.551915944f, 0);
        var cy = new Vector2(0, radius * 0.551915944f);
        var w = new Vector2(rect.Size.X, 0);
        var h = new Vector2(0, rect.Size.Y);
        var points = new PathPoint[8]
        {
            new PathPoint(-cx, rect.Position + rx, null),
            new PathPoint(null, rect.Position + w - rx, cx),

            new PathPoint(-cy, rect.Position + w + ry, null),
            new PathPoint(null, rect.Position + rect.Size - ry, cy),

            new PathPoint(cx,rect.Position + rect.Size - rx, null),
            new PathPoint(null, rect.Position + h + rx, -cx),

            new PathPoint(cy,rect.Position + h - ry, null),
            new PathPoint(null, rect.Position + ry, -cy),
        };

        return new Path(points) { Closed = true };
    }
}