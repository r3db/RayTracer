using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace RayTracer
{
    internal static class BitmapUtility
    {
        private const PixelFormat DefaultPixelFormat = PixelFormat.Format24bppRgb;

        internal static Image FromColorArray(ColorRaw[] data, int width, int height)
        {
            var pinned = GCHandle.Alloc(data, GCHandleType.Pinned);
            var result = new Bitmap(width, height, width * Marshal.SizeOf<ColorRaw>(), DefaultPixelFormat, pinned.AddrOfPinnedObject());
            pinned.Free();

            return result;
        }
    }
}