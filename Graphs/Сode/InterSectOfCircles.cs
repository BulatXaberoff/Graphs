using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Сode
{
    public enum InterSect
    {
        NoPoint, OnePoint, TwoPoint, Same
    }
    class GPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public GPoint() { }
        public GPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
        public void Show()
        {
            Console.WriteLine($"X:{X}\tY:{Y}");
        }
        static public GPoint operator -(GPoint p1, GPoint p2)
        {
            return new GPoint(p1.X - p2.X, p1.Y - p2.Y);
        }
        static public GPoint operator +(GPoint p1, GPoint p2)
        {
            return new GPoint(p1.X + p2.X, p1.Y + p2.Y);
        }
        static public GPoint operator +(GPoint p1, double value)
        {
            return new GPoint(p1.X + value, p1.Y + value);
        }
        static public GPoint operator +(double value, GPoint p1)
        {
            return new GPoint(p1.X + value, p1.Y + value);
        }
        static public GPoint operator *(double value, GPoint point)
        {
            return new GPoint(value * point.X, value * point.Y);
        }
        static public GPoint operator *(GPoint point, double value)
        {
            return new GPoint(value * point.X, value * point.Y);
        }
        static public GPoint operator /(GPoint point, double value)
        {
            return new GPoint(point.X / value, point.Y / value);
        }
        static public bool operator ==(GPoint ob1, GPoint ob2)
        {

            return ob1.X == ob2.X && ob1.Y == ob2.Y;
        }
        static public bool operator !=(GPoint ob1, GPoint ob2)
        {

            return ob1.X == ob2.X && ob1.Y == ob2.Y ? false : true;
        }
        public double Distance(GPoint point)
        {
            return Math.Sqrt(Math.Pow(X - point.X, 2) + Math.Pow(Y - point.Y, 2));
        }
        public static bool operator ==(object o, GPoint t)
        {
            return true;
        }
        public static bool operator !=(object o, GPoint t)
        {
            return true;
        }
        public override bool Equals(object o)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
    class Circle : GPoint
    {
        public double Radius { get; private set; }
        public double Area
        {
            get; private set;
        }
        GPoint Center;
        const double PI = Math.PI;
        public Circle() { }
        public Circle(GPoint center, double radius)
        {
            Center = center;
            Radius = radius;
            Area = Math.Pow(Radius, 2) * PI;
        }

        public Circle(double x, double y, double r) : this(new GPoint(), r)
        {
        }

        public void ShowArea()
        {
            Console.WriteLine(Area);
        }
        static public InterSect operator *(Circle ob1, Circle ob2)
        {
            InterSect interSect = new InterSect();
            var Distance = ob1.Center.Distance(ob2.Center);
            if (ob1.Center == ob2.Center && ob1.Radius == ob2.Radius)
            {
                interSect = InterSect.Same;
                Console.WriteLine("Кол-во точек пересечения бесконечно");
            }
            else if (Distance > ob1.Radius + ob2.Radius)
            {
                interSect = InterSect.NoPoint;
                Console.WriteLine("Нет точек пересечения:");
            }
            else if (Distance < Math.Abs(ob1.Radius - ob2.Radius))
            {
                interSect = InterSect.NoPoint;
                Console.WriteLine("Нет точек пересечения:");
            }
            else if (Distance == ob1.Radius + ob2.Radius || Distance == Math.Abs(ob1.Radius - ob2.Radius))
            {
                GPoint point = new GPoint((ob1.Center.X + ob2.Center.X) / 2, (ob1.Center.Y + ob2.Center.Y) / 2);
                interSect = InterSect.OnePoint;
                Console.WriteLine("Точка пересечения:");
                point.Show();
            }
            else
            {
                Foo(ob1, ob2);
                interSect = InterSect.TwoPoint;
            }
            return interSect;
        }
        public static Circle Parse(string str)
        {
            double x, y, r;
            x = str[0];
            y = str[2];
            r = str[4];
            return new Circle(x, y, r);
        }
        static public implicit operator string(Circle ob)
        {
            string res = $"{ob.Center.X} {ob.Center.Y} {ob.Radius} {ob.Area}";

            return res;
        }
        static public void Foo(Circle c1, Circle c2)
        {
            GPoint p0, p3, p4;
            GPoint p1 = c1.Center, p2 = c2.Center;
            double a, b;
            double distant = c1.Center.Distance(c2.Center);
            b = ((Math.Pow(c2.Radius, 2) - Math.Pow(c1.Radius, 2) + Math.Pow(distant, 2)) / (2 * distant));
            a = distant - b;
            double h = Math.Sqrt(Math.Abs(Math.Pow(c1.Radius, 2) - Math.Pow(a, 2)));
            p0 = c1.Center + a / distant * (c2.Center - c1.Center);
            p3 = new GPoint(p0.X + (p2.Y - p1.Y) / distant * h, p0.Y - (p2.X - p1.X) / distant * h);
            p4 = new GPoint(p0.X - (p2.Y - p1.Y) / distant * h, p0.Y + (p2.X - p1.X) / distant * h);
            Console.WriteLine($"Две точки пересечения:\nПервая точка:{p3.X}\t{p3.Y}\nВторая точка:{p4.X}\t{p4.Y}");
        }
        ~Circle() { Console.WriteLine("Уничтожение объекта"); }
    }
}
