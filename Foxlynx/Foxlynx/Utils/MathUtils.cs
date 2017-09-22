using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxlynx
{
    public class MathUtils
    {

        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0f;
        }

        public static double RadianToDegree(double angle)
        {
            return angle * (180.0f / Math.PI);
        }

        public static double Direction(double x1, double y1, double x2, double y2)
        {
            double angle = RadianToDegree((Math.Atan2(y2 - y1, x2 - x1)));
            if (angle < 0)
            {
                angle += 360;
            }
            return angle;
        }

        public static float Distance(float x1, float y1, float x2, float y2)
        {
            float dX = x1 - x2;
            float dY = y1 - y2;
            return (float)Math.Sqrt((dX * dX) + (dY * dY));
        }

        public static double getLengthdirX(double length, double direction)
        {
            return Math.Cos(direction * (Math.PI / 180.0)) * length;
        }

        public static double getLengthdirY(double length, double direction)
        {
            return Math.Sin(direction * (Math.PI / 180.0)) * length;
        }

        public static float Lerp(float p1, float p2, float alpha)
        {
            return p1 + alpha * (p2 - p1);
        }

    }
}
