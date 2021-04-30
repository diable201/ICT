using System.Collections.Generic;
using System.Drawing;

namespace Example2
{
    class Utils
    {
        static void Step(
            int x,
            int y,
            int w,
            int h,
            Bitmap bmp,
            Color OriginColor,
            Color fillColor,
            Queue<Point> q)
        {
            if (x >= 0 && y >= 0 && x < w && y < h)
            {
                if (bmp.GetPixel(x, y) == OriginColor)
                {
                    bmp.SetPixel(x, y, fillColor);
                    q.Enqueue(new Point(x, y));
                }
            }
        }
        public static Bitmap Fill(
        Bitmap bitmap,
        Point originPoint,
        Color originColor,
        Color FillColor)
        {
            Queue<Point> q = new Queue<Point>();
            bitmap.SetPixel(originPoint.X, originPoint.Y, FillColor);
            q.Enqueue(originPoint);
            while (q.Count != 0)
            {
                Point cur = q.Dequeue();
                Step(cur.X + 1, cur.Y, bitmap.Width, bitmap.Height, bitmap, originColor, FillColor, q);
                Step(cur.X - 1, cur.Y, bitmap.Width, bitmap.Height, bitmap, originColor, FillColor, q);
                Step(cur.X, cur.Y + 1, bitmap.Width, bitmap.Height, bitmap, originColor, FillColor, q);
                Step(cur.X, cur.Y - 1, bitmap.Width, bitmap.Height, bitmap, originColor, FillColor, q);
            }
            return bitmap;
        }
    }
   
}
