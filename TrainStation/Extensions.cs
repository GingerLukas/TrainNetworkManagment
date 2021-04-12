using System;
using System.Drawing;
using System.Windows.Forms;

namespace TrainStation
{
    public static class Extensions
    {
        public static float Distance(this Point a, Point b)
        {
            float x = a.X - b.X;
            x *= x;
            float y = a.Y - b.Y;
            y *= y;
            return (float) Math.Sqrt(x + y);
        }

        public static void Invoke(this Control control, Action action)
        {
            control.Invoke((Delegate) action);
        }
    }
}