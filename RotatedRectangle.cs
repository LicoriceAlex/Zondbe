using Microsoft.Xna.Framework;
using System;

namespace AiFirst
{
    public class RotatedRectangle
    {
        public Vector2 Center;
        public float Width, Height;
        public float Rotation;

        public RotatedRectangle(Vector2 center, float width, float height, float rotation)
        {
            Center = center;
            Width = width;
            Height = height;
            Rotation = rotation;
        }

        public Vector2[] GetVertices()
        {
            var vertices = new Vector2[4];
            float cosine = (float)Math.Cos(Rotation);
            float sine = (float)Math.Sin(Rotation);
            vertices[0] = new Vector2(-Width / 2, -Height / 2);
            vertices[1] = new Vector2(Width / 2, -Height / 2);
            vertices[2] = new Vector2(Width / 2, Height / 2);
            vertices[3] = new Vector2(-Width / 2, Height / 2);

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 rotatedVertex = new Vector2(
                    vertices[i].X * cosine - vertices[i].Y * sine,
                    vertices[i].X * sine + vertices[i].Y * cosine
                );
                vertices[i] = rotatedVertex + Center;
            }
            return vertices;
        }
    }
}
