using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

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

    public void AddQuadraticCurveSegment(Vector2 controlPoint, Vector2 end)
    {
        if (_points.Count == 0)
        {
            throw new NotImplementedException($"Can't add curve segment if there is no previous point defined");
        }

        var prevPoint = _points[_points.Count - 1];
        var controlPoint1 = prevPoint.Point + (2.0f / 3.0f) * (controlPoint - prevPoint.Point);
        var controlPoint2 = end + (2.0f / 3.0f) * (controlPoint - end);
        AddCubicCurveSegment(controlPoint1, controlPoint2, end);
    }

    public void AddCubicCurveSegment(Vector2 firstControlPoint, Vector2 secondControlPoint, Vector2 end)
    {
        if (_points.Count == 0)
        {
            throw new NotImplementedException($"Can't add curve segment if there is no previous point defined");
        }

        var prevPoint = _points[_points.Count - 1];
        _points[_points.Count - 1] = new PathPoint(prevPoint.In, prevPoint.Point, firstControlPoint);
        _points.Add(new PathPoint(secondControlPoint, end, null));
    }

    public bool Closed { get; set; }

    public IEnumerator<PathPoint> GetEnumerator()
    {
        return _points.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_points).GetEnumerator();
    }

    public int Count => _points.Count;

    public PathPoint this[int index] { get=> _points[index]; set=> _points[index] = value; }

    public static PathSet Rectangle(Rect rect)
    {
        return new PathSet(new Path(rect.Position, rect.Position + new Vector2(rect.Size.X, 0), rect.Position + rect.Size, rect.Position + new Vector2(0, rect.Size.Y)) {Closed = true});
    }

    public static PathSet Rectangle(Rect rect, float radius)
    {
        if (radius < float.Epsilon)
            return Rectangle(rect);

        var rx = new Vector2(radius, 0);
        var ry = new Vector2(0, radius);
        var cx = new Vector2(radius * 0.551915944f, 0);
        var cy = new Vector2(0, radius * 0.551915944f);
        var w = new Vector2(rect.Size.X, 0);
        var h = new Vector2(0, rect.Size.Y);

        var A = rect.Position;
        var B = rect.Position+w;
        var C = rect.Position+rect.Size;
        var D = rect.Position+h;
        var points = new PathPoint[8]
        {
            new PathPoint(A+rx-cx, A+rx, null),
            new PathPoint(null, B-rx, B-rx+cx),

            new PathPoint(B+ry-cy, B+ry, null),
            new PathPoint(null, C-ry, C-ry+cy),

            new PathPoint(C-rx+cx,C-rx, null),
            new PathPoint(null, D+rx, D+rx-cx),

            new PathPoint(D-ry+cy,D-ry, null),
            new PathPoint(null, A+ry, A+ry-cy),
        };

        return new PathSet(new Path(points) { Closed = true } );
    }

    public Path Transform(Matrix3x2 transform)
    {
        var result = new Path() {Closed = Closed};
        foreach (var pathPoint in _points)
        {
            result._points.Add(new PathPoint(Transform(pathPoint.In, transform),
                Vector2.Transform(pathPoint.Point, transform), (Transform(pathPoint.Out, transform))));
        }
        return result;
    }

    public static Vector2? Transform(Vector2? point, Matrix3x2 transform)
    {
        return point.HasValue ? Vector2.Transform(point.Value, transform) : null;
    }

    public override string ToString()
    {
        var closed = Closed ? " { Closed = true }" : "";
        return $"new Path(new PathPoint[]{{{string.Join(", ", _points)}}}){closed}";
    }

    public Path EliminateControlPoints()
    {
        var result = new Path();

        foreach (var pathPoint in _points)
        {
            result._points.Add(new PathPoint(pathPoint.Point));
        }

        return result;
    }

    public void Add(PathPoint pathPoint)
    {
        _points.Add(pathPoint);
    }

    public void RemoveAt(int index)
    {
        _points.RemoveAt(index);
    }
}