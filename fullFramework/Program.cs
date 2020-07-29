using System;
using System.Drawing;

namespace VareduraTestes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"FullFramework 4.7.2");            

            var paths = new string[] {
                @"D:\desenv\caioindust\varedura-testes\img\wallpaper-4k-full-hd-144-800x600.png",
                @"D:\desenv\caioindust\varedura-testes\img\wallpaper-4k-full-hd-001-2880x1800.png",
                @"D:\desenv\caioindust\varedura-testes\img\wallpaper-hitman-blood-money-01-2880x1800.png"
            };

            foreach (var path in paths)
            {                
                using (var bmp = new Bitmap(path))
                {
                    Console.WriteLine($"{Environment.NewLine}Tamanho da imagem {bmp.Width}x{bmp.Height} pixels");

                    var convencionalTime = new Convencional.Varedura(bmp).Run();
                    var otimizadoTime = new Otimizado.Varedura(bmp).Run();

                    var ts = convencionalTime.Subtract(otimizadoTime);
                    Console.WriteLine($"Diferença {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{(ts.Milliseconds / 10):00}");
                }                
            }

            Console.ReadKey();
        }
    }
}
