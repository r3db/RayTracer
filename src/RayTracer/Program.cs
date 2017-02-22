using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;

namespace RayTracer
{
    internal static class Program
    {
        private static void Main()
        {
            const int width  = 4 * 1920;
            const int height = 4 * 1080;

            var spheres = Enumerable.Range(0, 100).Select(_ => SphereBuilder.Begin
                .WithinX(0, width)
                .WithinY(0, height)
                .WithinZ(0, 600)
                .WithinRadius(100, 500)
                .Build()).ToArray();

            var raytracer = new RayTracer(spheres);

            for (var w = 0; w < 4; w++)
            {
                var sw = Stopwatch.StartNew();
                var bmp = raytracer.Trace(width, height);
                Console.WriteLine("{0} ms", sw.ElapsedMilliseconds);

                bmp.Save($"result-{w}.png", ImageFormat.Png);
            }
        }
    }
}