using System;
using System.Threading;

namespace RayTracer
{
    internal sealed class SphereBuilder
    {
        private static readonly ThreadLocal<Random> _random = new ThreadLocal<Random>(() => new Random());

        private float _minX = 010;
        private float _maxX = 100;

        private float _minY = 010;
        private float _maxY = 100;

        private float _minZ = 010;
        private float _maxZ = 100;

        private float _minRadius = 010;
        private float _maxRadius = 100;

        internal static SphereBuilder Begin => new SphereBuilder();

        internal SphereBuilder WithinX(float minX, float maxX)
        {
            _minX = minX;
            _maxX = maxX;
            return this;
        }

        internal SphereBuilder WithinY(float minY, float maxY)
        {
            _minY = minY;
            _maxY = maxY;
            return this;
        }

        internal SphereBuilder WithinZ(float minZ, float maxZ)
        {
            _minZ = minZ;
            _maxZ = maxZ;
            return this;
        }

        public SphereBuilder WithinRadius(float minRadius, float maxRadius)
        {
            _minRadius = minRadius;
            _maxRadius = maxRadius;
            return this;
        }

        internal Sphere Build()
        {
            var rnd = _random.Value;

            return new Sphere
            {
                Origin = new Vector
                {
                    X = rnd.Next((int)_minX, (int)_maxX),
                    Y = rnd.Next((int)_minY, (int)_maxY),
                    Z = rnd.Next((int)_minZ, (int)_maxZ),
                },
                Color = new ColorRaw
                {
                    R = (byte)rnd.Next(0, byte.MaxValue),
                    G = (byte)rnd.Next(0, byte.MaxValue),
                    B = (byte)rnd.Next(0, byte.MaxValue),
                },
                Radius = rnd.Next((int)_minRadius, (int)_maxRadius),
            };
        }
    }
}