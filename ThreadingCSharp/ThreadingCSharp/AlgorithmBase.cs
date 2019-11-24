using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ThreadingCSharp
{
    abstract class AlgorithmBase
    {
        public int Width { get; set; } = 600;
        public int Height { get; set; } = 600;

        protected Bitmap bmp;

        DateTime startTime;
        DateTime stopTime;

        public double Duration => (stopTime - startTime).TotalSeconds;

        public void StartTimeMeasurement()
        {
            startTime = DateTime.Now;
        }

        public void StopTimeMeasurement()
        {
            stopTime = DateTime.Now;
        }


        public BitmapSource GetWPFBitmapSource()
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()); ;
        }

        public virtual void Prepare()
        {
            bmp = new Bitmap(Width, Height);
        }

        public void Solve()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    SolvePoint(i, j);
                }
            }
        }
        public void SolveRow(int row)
        {
            int i = row;
            for (int j = 0; j < Width; j++)
            {
                SolvePoint(j, i);
            }
        }

        public void SolvePart(int part, int totalNumOfParts)
        {
            int w = Height / totalNumOfParts;
            int row = part * w;

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    SolvePoint(j, row);
                }
                row++;
            }
        }

        public abstract void SolvePoint(int i, int j);
    }
}
