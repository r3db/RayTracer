using System;
using System.Drawing;
using Alea;
using Alea.Parallel;

namespace RayTracer
{
    internal sealed class RayTracer
    {
        [GpuParam]
        private readonly GlobalArraySymbol<Sphere> _spheres;

        internal RayTracer(Sphere[] spheres)
        {
            _spheres = Gpu.DefineConstantArraySymbol<Sphere>(spheres.Length);
            Gpu.Default.Copy(spheres, _spheres);
        }

        internal Image Trace(int width, int height)
        {
            var resultMemory = Gpu.Default.AllocateDevice<ColorRaw>(width * height);
            var resultDevPtr = new deviceptr<ColorRaw>(resultMemory.Handle);

            Gpu.Default.For(0, width * height, i => TraceKernel(i, resultDevPtr, width));

            return BitmapUtility.FromColorArray(Gpu.CopyToHost(resultMemory), width, height);
        }

        private void TraceKernel(int index, deviceptr<ColorRaw> image, int width)
        {
            var x = index % width;
            var y = index / width;

            var color = ColorRaw.FromRgb(0xed, 0x95, 0x64);
            var maxDepth = float.MinValue;

            for (var i = 0; i < _spheres.Length; i++)
            {
                // Todo: Get rid of 'n' until we have a way to properly shade!
                float n;
                var sphere = _spheres[i];
                var depth = sphere.GetIntersection(x, y, out n);

                if (depth > maxDepth)
                {
                    color = ColorRaw.FromRgb(
                        (byte)(sphere.Color.R * n),
                        (byte)(sphere.Color.G * n),
                        (byte)(sphere.Color.B * n));

                    maxDepth = depth;
                }
            }

            image[index] = color;
        }
    }
}