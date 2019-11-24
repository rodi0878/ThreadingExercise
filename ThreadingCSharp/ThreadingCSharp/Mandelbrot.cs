using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingCSharp
{
    class Mandelbrot : AlgorithmBase
    {
        private const double mandelXMinimum = -2.5;
        private const double mandelXMaximum = 1;
        private const double mandelYMinimum = -1;
        private const double mandelYMaximum = 1;

        private Color[] pallette;

        public Mandelbrot()
        {
            pallette = new Color[]{
                Color.White, Color.Black
            };
        }

        public static Color Interpolate(Color f, Color t, double p)
        {
            int red = (int)Math.Round(p * f.R + (1 - p) * t.R);
            int green = (int)Math.Round(p * f.G + (1 - p) * t.G);
            int blue = (int)Math.Round(p * f.B + (1 - p) * t.B);

            red = Math.Max(0, Math.Min(255, red));
            green = Math.Max(0, Math.Min(255, green));
            blue = Math.Max(0, Math.Min(255, blue));

            return Color.FromArgb(red, green, blue);
        }

        public override void SolvePoint(int xp, int yp)
        {
            double x0 = (((double)xp) / Width) * (mandelXMaximum - mandelXMinimum) + mandelXMinimum;
            double y0 = (((double)yp) / Height) * (mandelYMaximum - mandelYMinimum) + mandelYMinimum;

            double x = 0.0;
            double y = 0.0;
            double iteration = 0;
            int maxIterations = 1000;

            while (x * x + y * y < (1 << 16) && iteration < maxIterations)
            {
                double xtemp = x * x - y * y + x0;
                y = 2 * x * y + y0;
                x = xtemp;
                iteration++;
            }
            if (iteration < maxIterations)
            {
                double logzn = Math.Log10(x * x + y * y) / 2;
                double nu = Math.Log10(logzn / Math.Log10(2)) / Math.Log10(2);
                iteration = iteration + 1 - nu;
            }

            Color c1 = pallette[((int)Math.Floor(iteration)) % pallette.Length];
            Color c2 = pallette[((int)Math.Floor(iteration) + 1) % pallette.Length];

            double t = iteration - Math.Floor(iteration);
            Color c = Interpolate(c1, c2, t);
            bmp.SetPixel(xp, yp, c);
        }

    }
}
