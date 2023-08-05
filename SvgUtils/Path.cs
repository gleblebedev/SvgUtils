using System;
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

    public Path(SvgPath? svgPath)
    {
        if (svgPath == null)
            return;

        Vector2 lastKnownPoint = Vector2.Zero;
        foreach (var pathElement in svgPath.PathData)
        {
            if (pathElement is SvgMoveToSegment moveToSegment)
            {
                if (_points.Count > 0)
                {
                    throw new NotImplementedException($"Can't move cursor in the middle of a path.");
                }

                var end = FixNaNs(moveToSegment.End.ToVector2(), lastKnownPoint, moveToSegment.IsRelative);
                if (moveToSegment.IsRelative)
                {
                    end += lastKnownPoint;
                }

                _points.Add(new PathPoint(end));
                lastKnownPoint = end;
            }
            else if (pathElement is SvgLineSegment lineSegment)
            {
                var end = FixNaNs(lineSegment.End.ToVector2(), lastKnownPoint, lineSegment.IsRelative);
                if (lineSegment.IsRelative)
                {
                    end += lastKnownPoint;
                }
                _points.Add(new PathPoint(end));
                lastKnownPoint = end;
            }
            else if (pathElement is SvgQuadraticCurveSegment quadraticCurveSegment)
            {
                var controlPoint = quadraticCurveSegment.ControlPoint.ToVector2();
                var end = FixNaNs(quadraticCurveSegment.End.ToVector2(), lastKnownPoint, quadraticCurveSegment.IsRelative);
                if (quadraticCurveSegment.IsRelative)
                {
                    controlPoint += lastKnownPoint;
                    end += lastKnownPoint;
                }
                AddQuadraticCurveSegment(controlPoint, end);
                lastKnownPoint = end;
            }
            else if (pathElement is SvgCubicCurveSegment cubicCurveSegment)
            {
                var controlPoint1 = cubicCurveSegment.FirstControlPoint.ToVector2();
                var controlPoint2 = cubicCurveSegment.SecondControlPoint.ToVector2();
                var end = FixNaNs(cubicCurveSegment.End.ToVector2(), lastKnownPoint, cubicCurveSegment.IsRelative);
                if (cubicCurveSegment.IsRelative)
                {
                    controlPoint1 += lastKnownPoint;
                    controlPoint2 += lastKnownPoint;
                    end += lastKnownPoint;
                }
                AddCubicCurveSegment(controlPoint1, controlPoint2, end);
                lastKnownPoint = end;
            }
            else if (pathElement is SvgClosePathSegment)
            {
                if (_points.Count > 1)
                {
                    var first = _points[0];
                    var last = _points[_points.Count-1];
                    if (first.Point == last.Point)
                    {
                        _points[0] = new PathPoint(last.In, first.Point, first.Out);
                        _points.RemoveAt(_points.Count-1);
                    }
                }
                Closed = true;
            }
            else
            {
                throw new NotImplementedException($"Segments of type {pathElement.GetType().Name} not supported yet");
            }
        }
    }

    private Vector2 FixNaNs(Vector2 point, Vector2 lastKnownGood, bool isRelative = false)
    {
        Vector2 res = point;
        if (float.IsNaN(point.X))
        {
            res.X = isRelative ? 0.0f : lastKnownGood.X;
        }
        if (float.IsNaN(point.Y))
        {
            res.Y = isRelative ? 0.0f : lastKnownGood.Y;
        }

        return res;
    }

    private void AddQuadraticCurveSegment(Vector2 controlPoint, Vector2 end)
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

    private void AddCubicCurveSegment(Vector2 firstControlPoint, Vector2 secondControlPoint, Vector2 end)
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
    public Rect EvaluateBounds(int sampleSteps = 3)
    {
        if (_points.Count == 0)
            return new Rect();

        var rect = new Rect(_points[0].Point, Vector2.Zero);
        for (var index = 0; index < _points.Count; index++)
        {
            var pathPoint = _points[index];
            rect = rect.Union(pathPoint.Point);
            //TODO: Sample curves.
        }

        return rect;
    }

    public Path ScaleToFit(Rect rect)
    {
        if (_points.Count == 0)
        {
            return new Path();
        }

        var bounds = EvaluateBounds();
        bounds = bounds.GrowToAspectRatio(1.0f);
        var scale = rect.Size / bounds.Size;
        var offset = rect.Position - bounds.Position * scale;
        var result = new Path();

        //var transform = Matrix3x2.CreateTranslation(offset) * Matrix3x2.CreateScale(scale);
        var transform = new Matrix3x2(scale.X, 0.0f, 0.0f, scale.Y, offset.X, offset.Y);
        foreach (var pathPoint in _points)
        {
            result._points.Add(new PathPoint(Transform(pathPoint.In, transform), Vector2.Transform(pathPoint.Point, transform), (Transform(pathPoint.Out, transform))));
        }

        return result;
    }

    private Vector2? Transform(Vector2? point, Matrix3x2 transform)
    {
        return point.HasValue ? Vector2.Transform(point.Value, transform) : null;
    }

    public override string ToString()
    {
        return $"new Path(new PathPoint[]{{{string.Join(", ", _points)}}})";
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
}