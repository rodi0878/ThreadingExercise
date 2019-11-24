﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows;

namespace ThreadingCSharp
{
    class NewtonSolver : AlgorithmBase
    {
        double xmin = -1.5;
        double xmax = 1.5;
        double ymin = -1.5;
        double ymax = 1.5;

        double xstep;
        double ystep;

        List<ComplexNumber> roots = new List<ComplexNumber>();

        Color[] clrs = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

        Polynome p = new Polynome();
        Polynome pd;

        public override void Prepare()
        {
            bmp = new Bitmap(Width, Height);
            xstep = (xmax - xmin) / Width;
            ystep = (ymax - ymin) / Height;

            p.Coefficients.Add(new ComplexNumber() { Re = 1 });
            p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(new ComplexNumber() { Re = 1 });
            pd = p.Derive();
        }

        public override void SolvePoint(int i, int j)
        {
            // find "world" coordinates of pixel
            double x = xmin + j * xstep;
            double y = ymin + i * ystep;

            ComplexNumber ox = new ComplexNumber()
            {
                Re = x,
                Im = (float)(y)
            };

            if (ox.Re == 0)
                ox.Re = 0.0001;
            if (ox.Im == 0)
                ox.Im = 0.0001f;

            //Console.WriteLine(ox);

            // find solution of equation using newton's iteration
            float it = 0;
            for (int q = 0; q < 30; q++)
            {
                var diff = p.Eval(ox).Divide(pd.Eval(ox));
                ox = ox.Subtract(diff);

                if (Math.Pow(diff.Re, 2) + Math.Pow(diff.Im, 2) >= 0.5)
                {
                    q--;
                }
                it++;
            }

            // find solution root number
            var known = false;
            var id = 0;
            for (int w = 0; w < roots.Count; w++)
            {
                if (Math.Pow(ox.Re - roots[w].Re, 2) + Math.Pow(ox.Im - roots[w].Im, 2) <= 0.01)
                {
                    known = true;
                    id = w;
                }
            }
            if (!known)
            {
                roots.Add(ox);
                id = roots.Count;
            }

            // colorize pixel according to root number
            var vv = clrs[id % clrs.Length];
            vv = Color.FromArgb(vv.R, vv.G, vv.B);
            vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R - (int)it * 2), 255), Math.Min(Math.Max(0, vv.G - (int)it * 2), 255), Math.Min(Math.Max(0, vv.B - (int)it * 2), 255));

            bmp.SetPixel(j, i, vv);
        }
    }
}
