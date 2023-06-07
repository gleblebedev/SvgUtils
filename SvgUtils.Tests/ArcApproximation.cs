using System.Numerics;
using Xunit.Abstractions;

namespace SvgUtils.Tests
{
    public class ArcApproximation
    {
        private readonly ITestOutputHelper _testOutput;

        public ArcApproximation(ITestOutputHelper testOutput)
        {
            _testOutput = testOutput;
        }
        [Fact]
        public void Approximate90Deg()
        {
            var offset = new Vector2(0.0f, 0.5f);
            //var steps = Enumerable.Range(0, 16)
            //    .Select(_ => new Vector2(MathF.Cos(_ / 8.0f * MathF.PI), MathF.Sin(_ / 8.0f * MathF.PI))).ToArray();
            var steps = new Vector2[] { new Vector2(0, -1), new Vector2(0, 1) };
            var dist = 0.1f;
            float bestErr = float.MaxValue;

            for (int stepIndex=0; stepIndex<100; ++stepIndex)
            {
                int bestStep = -1;
                bestErr = EvaluateError(offset, MathF.PI * 0.5f);
                for (int i = 0; i < steps.Length; i++)
                {
                    var err = EvaluateError(offset + steps[i] * dist);
                    if (err < bestErr)
                    {
                        bestStep = i;
                        bestErr = err;
                    }
                }

                if (bestStep >= 0)
                {
                    offset += steps[bestStep] * dist;
                }
                else
                {
                    dist *= 0.5f;
                }
            }

            _testOutput.WriteLine($"Offset {offset} gives error {bestErr}");
        }

        private static float EvaluateError(Vector2 offset, float arch = MathF.PI * 0.5f)
        {
            var rot = Matrix3x2.CreateRotation(arch);
            var A = new Vector2(1, 0);
            var B = A + offset;
            var D = Vector2.Transform(A, rot);
            var C = D + Vector2.Transform(new Vector2(offset.X, -offset.Y), rot);
            float error = 0.0f;
            int steps = 40;
            for (int i = 0; i <= steps; i++)
            {
                float t = i / (float)steps;
                float _t = 1.0f - t;
                var p = A * _t * _t * _t + B * 3.0f * t * _t * _t + C * 3.0f * t * t * _t + D * t * t * t;
                var d = p;
                var e = MathF.Abs(d.LengthSquared() - 1.0f);
                if (e > error)
                    error = e;
            }

            return error;
        }
    }
}