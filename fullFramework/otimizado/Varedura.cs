using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace VareduraTestes.Otimizado
{
    public class Varedura
    {
        private const int RGBA_ARRAY_LENGTH = 4;
        private const int RGBA_ARRAY_INDEX_B = 0;
        private const int RGBA_ARRAY_INDEX_G = 1;
        private const int RGBA_ARRAY_INDEX_R = 2;
        private const int RGBA_ARRAY_INDEX_A = 3;

        private readonly Image _bmp;
        public Varedura(Image bmp)
        {
            _bmp = bmp;
        }

        public TimeSpan Run()
        {
            var bmp = (_bmp as Bitmap) ?? new Bitmap(_bmp);
            var colors = new Dictionary<string, int>();

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var bmpRead = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bitmapLength = bmpRead.Stride * bmpRead.Height;
            var bitmapBGRA = new byte[bitmapLength];
            Marshal.Copy(bmpRead.Scan0, bitmapBGRA, 0, bitmapLength);
            bmp.UnlockBits(bmpRead);

            for (int i = 0; i < bitmapLength; i += RGBA_ARRAY_LENGTH)
            {
                var A = bitmapBGRA[i + RGBA_ARRAY_INDEX_A];
                var R = bitmapBGRA[i + RGBA_ARRAY_INDEX_R];
                var G = bitmapBGRA[i + RGBA_ARRAY_INDEX_G];
                var B = bitmapBGRA[i + RGBA_ARRAY_INDEX_B];

                var pixel = Color.FromArgb(A, R, G, B);
                var hex = ColorTranslator.ToHtml(pixel);

                if (colors.ContainsKey(hex))
                    colors[hex]++;
                else
                    colors.Add(hex, 0);
            }

            stopWatch.Stop();
            var ts = stopWatch.Elapsed;

            //Console.WriteLine(string.Join(Environment.NewLine, colors.Where(c=> c.Value > 1000).OrderByDescending(c => c.Value).Select(c => $"{c.Key}:{c.Value}").ToArray()));
            Console.WriteLine($"Tempo de execução Otimizado {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{(ts.Milliseconds / 10):00}");

            return ts;
        }
    }
}
