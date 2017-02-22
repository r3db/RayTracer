using System;

namespace RayTracer
{
    internal struct Sphere
    {
        internal Vector Origin;
        internal ColorRaw Color;
        internal float Radius;

        internal float GetIntersection(float x, float y, out float n)
        {
            var dx = x - Origin.X;
            var dy = y - Origin.Y;

            n = 0;

            var rs = Radius * Radius;
            var dxs = dx * dx;
            var dys = dy * dy;

            if (dxs + dys >= rs)
            {
                // Note: Does not hit pixel!
                return float.MinValue;
            }

            var dz = (float)Math.Sqrt(rs - dxs - dys);
            n = dz / (float)Math.Sqrt(rs);

            return dz + Origin.Z;
        }
    }
}