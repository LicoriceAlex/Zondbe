using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AiFirst
{
    public static class SATCollision
    {
        public static bool IsCollision(RotatedRectangle rectA, RotatedRectangle rectB)
        {
            var verticesA = rectA.GetVertices();
            var verticesB = rectB.GetVertices();
            return CheckCollisionOnAxes(verticesA, verticesB) && CheckCollisionOnAxes(verticesB, verticesA);
        }

        private static bool CheckCollisionOnAxes(Vector2[] verticesA, Vector2[] verticesB)
        {
            foreach (var axis in GetEdgeNormals(verticesA))
            {
                if (!IsAxisCollision(verticesA, verticesB, axis))
                {
                    return false;
                }
            }
            return true;
        }

        private static IEnumerable<Vector2> GetEdgeNormals(Vector2[] vertices)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                var edge = vertices[(i + 1) % vertices.Length] - vertices[i];
                yield return Vector2.Normalize(new Vector2(-edge.Y, edge.X));
            }
        }

        private static bool IsAxisCollision(Vector2[] verticesA, Vector2[] verticesB, Vector2 axis)
        {
            var (minA, maxA) = ProjectVertices(verticesA, axis);
            var (minB, maxB) = ProjectVertices(verticesB, axis);
            return maxA >= minB && maxB >= minA;
        }

        private static (float min, float max) ProjectVertices(Vector2[] vertices, Vector2 axis)
        {
            var projections = vertices.Select(vertex => Vector2.Dot(vertex, axis));
            return (projections.Min(), projections.Max());
        }
    }
}
