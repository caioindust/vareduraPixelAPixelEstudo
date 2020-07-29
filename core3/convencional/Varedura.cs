using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace VareduraTestes.Convencional
{
    public class Varedura
    {
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
            
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {                    
                    var pixel = bmp.GetPixel(x, y);
                    var hex = ColorTranslator.ToHtml(pixel);

                    if (colors.ContainsKey(hex))
                        colors[hex]++;
                    else
                        colors.Add(hex, 1);
                }
            }

            stopWatch.Stop();
            var ts = stopWatch.Elapsed;

            //Console.WriteLine(string.Join(Environment.NewLine, colors.Where(c => c.Value > 1000).OrderByDescending(c => c.Value).Select(c => $"{c.Key}:{c.Value}").ToArray()));
            Console.WriteLine($"Tempo de execução Convencional {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{(ts.Milliseconds / 10):00}");

            return ts;
        }        
    }
}
