﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock.ImageProcessing
{
    internal class Deskew
    {
        // Representation of a line in the image.
        public class HougLine
        {
            //' Count of points in the line.
            public int Count;
            //' Index in Matrix.
            public int Index;
            //' The line is represented as all x,y that solve y*cos(alpha)-x*sin(alpha)=d
            public double Alpha;
            public double d;
        }


        // The Bitmap
        readonly Bitmap cBmp;
        // The range of angles to search for lines
        readonly double cAlphaStart = -20;
        readonly double cAlphaStep = 0.2;
        readonly int cSteps = 40 * 5;
        // Precalculation of sin and cos.
        double[] cSinA;
        double[] cCosA;
        // Range of d
        double cDMin;
        readonly double cDStep = 1;
        // Count of points that fit in a line.
        int[] cHMatrix;

        // calculate the skew angle of the image cBmp

        public double GetSkewAngle()
        {
            HougLine[] hl;
            int i;
            double sum = 0;
            int count = 0;

            //' Hough Transformation
            Calc();
            //' Top 20 of the detected lines in the image.
            hl = GetTop(20);
            //' Average angle of the lines
            for (i = 0; i < 19; i++)
            {
                sum += hl[i].Alpha;
                count += 1;
            }
            return sum / count;
        }

        //    ' Calculate the Count lines in the image with most points.
        private HougLine[] GetTop(int Count)
        {
            HougLine[] hl;
            int j;
            HougLine tmp;
            int AlphaIndex, dIndex;
            hl = new HougLine[Count];
            for (int i = 0; i < Count; i++)
            {
                hl[i] = new HougLine();
            }
            for (int i = 0; i < cHMatrix.Length - 1; i++)
            {
                if (cHMatrix[i] > hl[Count - 1].Count)
                {
                    hl[Count - 1].Count = cHMatrix[i];
                    hl[Count - 1].Index = i;
                    j = Count - 1;
                    while (j > 0 && hl[j].Count > hl[j - 1].Count)
                    {
                        tmp = hl[j];
                        hl[j] = hl[j - 1];
                        hl[j - 1] = tmp;
                        j -= 1;
                    }
                }
            }
            for (int i = 0; i < Count; i++)
            {
                dIndex = hl[i].Index / cSteps;
                AlphaIndex = hl[i].Index - dIndex * cSteps;
                hl[i].Alpha = GetAlpha(AlphaIndex);
                hl[i].d = dIndex + cDMin;
            }
            return hl;
        }

        public Deskew(Bitmap bmp)
        {
            cBmp = bmp;
        }

        //    ' Hough Transforamtion:
        private void Calc()
        {
            int x;
            int y;
            int hMin = cBmp.Height / 4;
            int hMax = cBmp.Height * 3 / 4;
            Init();
            for (y = hMin; y < hMax; y++)
            {
                for (x = 1; x < cBmp.Width - 2; x++)
                {
                    //' Only lower edges are considered.
                    if (IsBlack(x, y) && !IsBlack(x, y + 1))
                    {
                        Calc(x, y);
                    }
                }
            }
        }

        //    ' Calculate all lines through the point (x,y).
        private void Calc(int x, int y)
        {
            double d;
            int dIndex;
            int Index;
            for (int alpha = 0; alpha < cSteps - 1; alpha++)
            {
                d = y * cCosA[alpha] - x * cSinA[alpha];
                dIndex = (int)CalcDIndex(d);
                Index = dIndex * cSteps + alpha;

                try
                {
                    cHMatrix[Index] += 1;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }
        private double CalcDIndex(double d)
        {
            return Convert.ToInt32(d - cDMin);
        }
        private bool IsBlack(int x, int y)
        {
            Color c;
            double luminance;
            c = cBmp.GetPixel(x, y);
            luminance = (c.R * 0.299) + (c.G * 0.587) + (c.B * 0.114);
            return luminance < 140;
        }
        private void Init()
        {
            double angle;
            int cDCount;
            //' Precalculation of sin and cos.
            cSinA = new double[cSteps - 1];
            cCosA = new double[cSteps - 1];
            for (int i = 0; i < cSteps - 1; i++)
            {
                angle = GetAlpha(i) * Math.PI / 180.0;
                cSinA[i] = Math.Sin(angle);
                cCosA[i] = Math.Cos(angle);
            }
            //' Range of d
            cDMin = -cBmp.Width;
            cDCount = (int)(2 * (cBmp.Width + cBmp.Height) / cDStep);
            cHMatrix = new int[cDCount * cSteps];
        }
        public double GetAlpha(int Index)
        {
            return cAlphaStart + Index * cAlphaStep;
        }
        public static Bitmap RotateImage(Bitmap bmp, double angle)
        {
            Graphics g;
            Bitmap tmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppRgb);
            tmp.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
            g = Graphics.FromImage(tmp);
            try
            {
                g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
                g.RotateTransform((float)angle);
                g.DrawImage(bmp, 0, 0);
            }
            finally
            {
                g.Dispose();
            }
            return tmp;
        }
    }
}
