using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Svg;
using Svg.Pathing;

namespace SvgUtils;

public class PathSet: IEnumerable<Path>
{
    private List<Path> _paths = new List<Path>();

    public PathSet(params Path[] paths)
    {
        _paths.AddRange(paths);
    }

    public PathSet(IEnumerable<Path> paths)
    {
        _paths.AddRange(paths);
    }
    public PathSet(params PathPoint[] points)
    {
        _paths.Add(new Path(points));
    }

    public PathSet(IEnumerable<PathPoint> points)
    {
        _paths.Add(new Path(points));
    }

    public PathSet()
    {
    }

    public PathSet(SvgPath? svgPath)
    {
        if (svgPath == null)
            return;

        Vector2 lastKnownPoint = Vector2.Zero;
        Path path = new Path();
        _paths.Add(path);
        foreach (var pathElement in svgPath.PathData)
        {
            if (pathElement is SvgMoveToSegment moveToSegment)
            {
                if (path.Count > 0)
                {
                    path = new Path();
                    _paths.Add(path);
                }

                var end = FixNaNs(moveToSegment.End.ToVector2(), lastKnownPoint, moveToSegment.IsRelative);
                if (moveToSegment.IsRelative)
                {
                    end += lastKnownPoint;
                }

                path.Add(new PathPoint(end));
                lastKnownPoint = end;
            }
            else if (pathElement is SvgLineSegment lineSegment)
            {
                var end = FixNaNs(lineSegment.End.ToVector2(), lastKnownPoint, lineSegment.IsRelative);
                if (lineSegment.IsRelative)
                {
                    end += lastKnownPoint;
                }
                path.Add(new PathPoint(end));
                lastKnownPoint = end;
            }
            else if (pathElement is SvgQuadraticCurveSegment quadraticCurveSegment)
            {
                var controlPoint = FixNaNs(quadraticCurveSegment.ControlPoint.ToVector2(), lastKnownPoint, quadraticCurveSegment.IsRelative);
                var end = FixNaNs(quadraticCurveSegment.End.ToVector2(), lastKnownPoint, quadraticCurveSegment.IsRelative);
                if (quadraticCurveSegment.IsRelative)
                {
                    controlPoint += lastKnownPoint;
                    end += lastKnownPoint;
                }
                path.AddQuadraticCurveSegment(controlPoint, end);
                lastKnownPoint = end;
            }
            else if (pathElement is SvgCubicCurveSegment cubicCurveSegment)
            {
                var controlPoint1 = FixNaNs(cubicCurveSegment.FirstControlPoint.ToVector2(), lastKnownPoint, cubicCurveSegment.IsRelative);
                var controlPoint2 = FixNaNs(cubicCurveSegment.SecondControlPoint.ToVector2(), lastKnownPoint, cubicCurveSegment.IsRelative);
                var end = FixNaNs(cubicCurveSegment.End.ToVector2(), lastKnownPoint, cubicCurveSegment.IsRelative);
                if (cubicCurveSegment.IsRelative)
                {
                    controlPoint1 += lastKnownPoint;
                    controlPoint2 += lastKnownPoint;
                    end += lastKnownPoint;
                }
                path.AddCubicCurveSegment(controlPoint1, controlPoint2, end);
                lastKnownPoint = end;
            }
            else if (pathElement is SvgClosePathSegment)
            {
                if (path.Count > 1)
                {
                    var first = path[0];
                    var last = path[path.Count - 1];
                    if (first.Point == last.Point)
                    {
                        path[0] = new PathPoint(last.In, first.Point, first.Out);
                        path.RemoveAt(path.Count - 1);
                    }
                }
                path.Closed = true;
                lastKnownPoint = path.First().Point;
            }
            else
            {
                throw new NotImplementedException($"Segments of type {pathElement.GetType().Name} not supported yet");
            }
        }
    }

    public int Count => _paths.Count;

    public PathSet EliminateControlPoints()
    {
        return new PathSet(_paths.Select(_=>_.EliminateControlPoints()));
    }

    public static implicit operator SvgPath(PathSet path)
    {
        var data = new SvgPathSegmentList();

        for (var i = 0; i < path._paths.Count; i++)
        {
            var pathPath = path._paths[i];
            if (pathPath.Count == 0)
                break;

            //if (i > 0 && path._paths[i - 1].Count > 0)
            //{
            //    data.Add(new SvgMoveToSegment(true, (pathPath[0].Point - path._paths[i - 1].Last().Point).ToPointF()));
            //}
            //else
            {
                data.Add(new SvgMoveToSegment(false, pathPath[0].Point.ToPointF()));
            }

            for (int index = 0; index < pathPath.Count - 1; ++index)
            {
                data.Add(pathPath[index].BuildSegmentTo(pathPath[index + 1]));
            }

            if (pathPath.Closed)
            {
                var lastSegment = pathPath[pathPath.Count - 1].BuildSegmentTo(pathPath[0]);
                if (!(lastSegment is SvgLineSegment))
                {
                    data.Add(lastSegment);
                }

                data.Add(new SvgClosePathSegment(true));
            }
        }

        return new SvgPath() { PathData = data };
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

    public Rect EvaluateBounds(int sampleSteps = 3)
    {
        Rect? rect = null;

        foreach (var path in _paths)
        {
            if (path.Count > 0)
            {
                rect = new Rect(path[0].Point, Vector2.Zero);
                break;
            }
        }

        if (!rect.HasValue)
            return new Rect();

        foreach (var path in _paths)
        {
            for (var index = 0; index < path.Count; index++)
            {
                var pathPoint = path[index];
                rect = rect.Value.Union(pathPoint.Point);
                //TODO: Sample curves.
            }
        }

        return rect.Value;
    }

    public PathSet ScaleToFit(Rect rect)
    {
        if (_paths.Count == 0)
        {
            return new PathSet();
        }

        var bounds = EvaluateBounds();
        bounds = bounds.GrowToAspectRatio(1.0f);
        var scale = rect.Size / bounds.Size;
        var offset = rect.Position - bounds.Position * scale;
        var result = new PathSet();

        //var transform = Matrix3x2.CreateTranslation(offset) * Matrix3x2.CreateScale(scale);
        var transform = new Matrix3x2(scale.X, 0.0f, 0.0f, scale.Y, offset.X, offset.Y);
        foreach (var path in _paths)
        {
            result.Add(path.Transform(transform));
        }

        return result;
    }

    private void Add(Path path)
    {
        _paths.Add(path);
    }

    public IEnumerator<Path> GetEnumerator()
    {
        return _paths.GetEnumerator();
    }

    public override string ToString()
    {
        return $"new PathSet(new Path[]{{{string.Join(", ", _paths)}}})";
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void AddRange(IEnumerable<Path> paths)
    {
        _paths.AddRange(paths);
    }
}