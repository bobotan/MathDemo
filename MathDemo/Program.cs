using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            //var x1 = 450.801;
            //var y1 = 5.54;
            //var x2 = 737.369;
            //var y2 = 209.031;
            //var x3 = 738.9599;
            //var y3 = 210.24243;
            //Point point1 = new Point() { X = x1, Y = y1 };
            //Point point2 = new Point() { X = x2, Y = y2 };
            //Point point3 = new Point() { X = x3, Y = y3 };
            //double ang = Angle(point2, point1, point3);
            ////角度转弧度
            //double huduzhi = (Math.PI/180)*ang/2;
            //double l = Math.Sin(huduzhi)*1;
            ////计算与需移动的点弧度
            //return;
            ////求中心点（三端点的算术平均值）：
            //var centerX = (x1 + x2 + x3) / 3;
            //var centerY = (y1 + y2 + y3) / 3;
            ////坐标点到中心点的距离；
            //var tmp = Math.Pow(centerX - x2, 2) + Math.Pow(centerY - y2, 2);
            //var L = Math.Sqrt(tmp);
            //var e = 1.0;
            //var x = e * Math.Abs(centerX - x1) / L + x1;
            //var y = e * Math.Abs(centerY - y1) / L + y1;
            //return;
            var x = 20;
            var y = 1532;
            var a = -2064.1631;
            var b = 1615.6284;
            var tmp = Math.Pow(x - a, 2) + Math.Pow(y - b, 2);
            var r = Math.Sqrt(tmp);
            var aAng = 1 / r;
            ////绕点(20,1532)顺时针旋转
            //Points P = new Points(20, 1532);

            //Points s = RotatePoint(20, 1532, -2064.1631, 1615.6284, aAng);
            //Console.WriteLine(s.x + "," + s.y);
            List<Point> points = LinePoint11(1520, 600, 1463.4464, 676.4870, 1440, 600, 1, true);
            ////  List<Point> points = LinePoint(20, 1532, 10.1320, 1396.47, -2064.1631, 1615.6284, 1, false);
            Console.WriteLine("点数：" + points.Count);
            foreach (var point in points)
            {
                Console.WriteLine("X:" + point.X + ",Y:" + point.Y);
            }
            Console.ReadKey();
        }
        /// <summary>
        /// 计算点P(x,y)与X轴正方向的夹角
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <returns>夹角弧度</returns>
        private static double RadPox(double x, double y)
        {
            //P在(0,0)的情况
            if (x == 0 && y == 0) return 0;
            //P在四个坐标轴上的情况：x正、x负、y正、y负
            if (y == 0 && x > 0) return 0;
            if (y == 0 && x < 0) return Math.PI;
            if (x == 0 && y > 0) return Math.PI / 2;
            if (x == 0 && y < 0) return Math.PI / 2 * 3;
            //点在第一、二、三、四象限时的情况
            if (x > 0 && y > 0) return Math.Atan(y / x);
            if (x < 0 && y > 0) return Math.PI - Math.Atan(y / -x);
            if (x < 0 && y < 0) return Math.PI + Math.Atan(-y / -x);
            if (x > 0 && y < 0) return Math.PI * 2 - Math.Atan(-y / x);
            return 0;
        }

        /// <summary>
        /// 求三点坐标的夹角
        /// </summary>
        /// <param name="cen"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static double Angle(Point cen, Point first, Point second)
        {
            //const double M_PI = 3.1415926535897;

            double ma_x = first.X - cen.X;
            double ma_y = first.Y - cen.Y;
            double mb_x = second.X - cen.X;
            double mb_y = second.Y - cen.Y;
            double v1 = (ma_x * mb_x) + (ma_y * mb_y);
            double ma_val = Math.Sqrt(ma_x * ma_x + ma_y * ma_y);
            double mb_val = Math.Sqrt(mb_x * mb_x + mb_y * mb_y);
            double cosM = v1 / (ma_val * mb_val);
            double angleAMB = Math.Acos(cosM) * 180 / Math.PI;
             
            
            return angleAMB;
        }

        /// <summary>
        /// 返回点P围绕点A旋转弧度rad后的坐标
        /// </summary>
        /// <param name="P">待旋转点坐标</param>
        /// <param name="A">旋转中心坐标</param>
        /// <param name="rad">旋转弧度</param>
        /// <param name="isClockwise">true:顺时针/false:逆时针</param>
        /// <returns>旋转后坐标</returns>
        private static Points RotatePoint(double x, double y, double a, double b, double rad, bool isClockwise = true)
        {
            //计算圆点与（x,y）坐标的斜率
            Points Temp1 = new Points(x - a, y - b);
            double angT1Ox = RadPox(Temp1.x, Temp1.y);

            //计算圆的半径
            double r1 = Math.Sqrt((x - a) * (x - a) + (y - b) * (y - b));
            //double r = Temp1.DistanceTo(new Points(0, 0));
            //∠T1OX弧度

            //∠T2OX弧度（T2为T1以O为圆心旋转弧度rad）
            double angT2OX = angT1Ox - (isClockwise ? 1 : -1) * rad;
            //点Temp2
            Points Temp2 = new Points(r1 * Math.Cos(angT2OX), r1 * Math.Sin(angT2OX));
            //点Q
            return new Points(Temp2.x + a, Temp2.y + b);
        }


        private static List<Point> LinePoint11(double x, double y, double x1, double y1, double a, double b, double c, bool isccw)
        {
            var points = new List<Point>();
            // 计算圆的半径
            var tmp = Math.Pow(x - a, 2) + Math.Pow(y - b, 2);
            var r = Math.Sqrt(tmp);

            //圆上任意2点之间的距离
            var tmp1 = Math.Pow(x - x1, 2) + Math.Pow(y - y1, 2);
            var h = Math.Sqrt(tmp1);

            //圆上任意(x,y),(x1,y1)2点之间的弧度
            var hudu = 2 * Math.Asin(h / (2 * r));

            //弧长计算公式:L=α（弧度）× r(半径) （弧度制）
            //计算弧长为1的弧度
            var rad = 1 / r*0.9;

            //计算A,B两点之间需要输出多少个点坐标
            int pointCount = (int)Math.Ceiling(hudu / rad);

            //∠T1OX弧度
            double angT1Ox = RadPox(x - a, y - b);
            var ang = angT1Ox;
            for (var i = 0; i < pointCount; i++)
            {
                if (isccw)
                {
                    //∠T2OX弧度（T2为T1以O为圆心旋转弧度rad）
                    double angT2Ox = ang - rad;
                    var point = new Point();
                    point.X = (r * Math.Cos(angT2Ox) + a);
                    point.Y = (r * Math.Sin(angT2Ox) + b);
                    points.Add(point);
                    ang += rad;
                }
                else
                {
                    //∠T2OX弧度（T2为T1以O为圆心旋转弧度rad）
                    double angT2Ox = ang + rad;
                    var point = new Point();
                    point.X = (r * Math.Cos(angT2Ox) + a);
                    point.Y = (r * Math.Sin(angT2Ox) + b);
                    points.Add(point);
                    ang -= rad;
                }

            }
            return points;
        }
        private static List<Point> LinePoint(double x, double y, double x1, double y1, double a, double b, double c, bool isccw)
        {
            var points = new List<Point>();
            var r = Radial(x, y, a, b);//计算圆的半径
            var cAng = (c * Math.PI) / (Math.PI * r); //计算弧长为1的弧度
            var ang0 = 0.0;
            var ang1 = 0.0;
            var xAng = 0.0;
            if (x > a && y == b)
            {
                ang0 = 0;
            }
            else if (x == a && y > b)
            {
                ang0 = Math.PI / 2;
            }
            else if (x < a && y == b)
            {
                ang0 = Math.PI;
            }
            else if (x == a && y < b)
            {
                ang0 = 3 * Math.PI / 2;
            }
            else
            {
                if (y < y1 && y1 > b)
                {
                    if (x > a && y < b)
                    {
                        ang0 = Math.Atan((y - b) / (x - a));
                    }
                    else if (x < a && y < b)
                    {
                        ang0 = Math.Atan((y - b) / (x - a)) + Math.PI;
                    }
                    else if (x < a && y > b)
                    {
                        ang0 = Math.Atan((y - b) / (x - a)) + Math.PI;
                    }
                    else if (x > a && y > b)
                    {
                        ang0 = Math.Atan((y - b) / (x - a)) + 2 * Math.PI;
                    }
                }
                else
                {
                    if (x > a && y > b)
                    {
                        ang0 = Math.Atan((y - b) / (x - a));
                    }
                    else if (x > a && y < b)
                    {
                        ang0 = Math.Atan((y - b) / (x - a)) + 2 * Math.PI;
                    }
                    else if (x < a && y > b)
                    {
                        ang0 = Math.Atan((y - b) / (x - a)) + Math.PI;
                    }
                    else if (x < a && y < b)
                    {
                        ang0 = Math.Atan((y - b) / (x - a)) + Math.PI;
                    }
                }
            }

            if (x1 > a && y1 == b)
            {
                ang1 = 0;
            }
            else if (x1 == a && y1 > b)
            {
                ang1 = Math.PI / 2;
            }
            else if (x1 < a && y1 == b)
            {
                ang1 = Math.PI;
            }
            else if (x1 == a && y1 < b)
            {
                ang1 = 3 * Math.PI / 2;
            }
            else
            {
                if (x1 > a && y1 > b)
                {
                    ang1 = Math.Atan((y1 - b) / (x1 - a));
                }
                else if (x1 < a && y1 > b)
                {
                    ang1 = Math.Atan((y1 - b) / (x1 - a)) + Math.PI;
                }
                else if (x1 < a && y1 < b)
                {
                    ang1 = Math.Atan((y1 - b) / (x1 - a)) + Math.PI;
                }
                else if (x1 > a && y1 < b && x < x1 && y > y1)
                {
                    ang1 = Math.Atan((y1 - b) / (x1 - a));
                }
                else if (x1 > a && y1 < b)
                {
                    ang1 = Math.Atan((y1 - b) / (x1 - a)) + 2 * Math.PI;
                }
            }

            if (isccw)
            {
                if (ang1 < ang0)
                {
                    ang1 = 2 * Math.PI;
                }
                xAng = ang0;
                while (xAng <= ang1)
                {
                    var point = new Point();
                    if (xAng == 0)
                    {
                        point.X = (r + a);
                        point.Y = b;
                    }
                    else if (xAng == Math.PI / 2)
                    {
                        point.X = (a);
                        point.Y = (r + b);
                    }
                    else if (xAng == Math.PI)
                    {
                        point.X = (-r + a);
                        point.Y = (b);
                    }
                    else if (xAng == 3 * Math.PI / 2)
                    {
                        point.X = (a);
                        point.Y = (-r + b);
                    }
                    else
                    {
                        point.X = (r * Math.Cos(xAng) + a);
                        point.Y = (r * Math.Sin(xAng) + b);
                    }
                    points.Add(point);
                    xAng += cAng;
                }
            }
            if (!isccw)
            {
                if (ang1 > ang0)
                {
                    ang0 = 2 * Math.PI;
                }
                xAng = ang0;
                while (xAng >= ang1)
                {
                    var point = new Point();
                    if (xAng == 0)
                    {
                        point.X = (r + a);
                        point.Y = (b);
                    }
                    else if (xAng == Math.PI / 2)
                    {
                        point.X = (a);
                        point.Y = (r + b);
                    }
                    else if (xAng == Math.PI)
                    {
                        point.X = (-r + a);
                        point.Y = (b);
                    }
                    else if (xAng == 3 * Math.PI / 2)
                    {
                        point.X = (a);
                        point.Y = (-r + b);
                    }
                    else
                    {
                        point.X = (r * Math.Cos(xAng) + a);
                        point.Y = (r * Math.Sin(xAng) + b);
                    }
                    points.Add(point);
                    xAng -= cAng;
                }
            }

            return points;
        }
        static double Radial(double x, double y, double a, double b)
        {
            var tmp = Math.Pow(x - a, 2) + Math.Pow(y - b, 2);

            return Math.Sqrt(tmp);
        }
    }

    /// <summary>
    /// 结构：表示一个点
    /// </summary>
    struct Points
    {
        //横、纵坐标
        public double x, y;
        //构造函数
        public Points(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        //该点到指定点pTarget的距离
        public double DistanceTo(Points p)
        {
            return Math.Sqrt((p.x - x) * (p.x - x) + (p.y - y) * (p.y - y));
        }
        //重写ToString方法
        public override string ToString()
        {
            return string.Concat("Point (",
             this.x.ToString("#0.000"), ',',
             this.y.ToString("#0.000"), ')');
        }
    }
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
