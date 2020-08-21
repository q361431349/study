using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            double side = 4.0;
            List<object> list = new List<object>()
            {
                 new Square(0),
                 new Circle(0),
                 //null,
                 new Rectangle(0,0),
                 new Square(side),
                 new Circle(side),
                 new Rectangle(side,side)
                //new Triangle(side,side)
            };


            foreach (var item in list)
            {
                var result = ComputeAreaModernIs(item);
                Console.WriteLine(result);
            }

            foreach (var item in list)
            {
                var result = ComputeAreaModernSwitch(item);
                Console.WriteLine(result);
            }

            Console.ReadKey();
        }
        public static double ComputeAreaModernIs(object shape)
        {
            if (shape is Square s)
                return s.Side * s.Side;
            else if (shape is Circle c)
                return c.Radius * c.Radius * Math.PI;
            else if (shape is Rectangle r)
                return r.Height * r.Length;
            // elided
            throw new ArgumentException(
                message: "shape is not a recognized shape",
                paramName: nameof(shape));
        }

        public static double ComputeAreaModernSwitch(object shape)
        {
            switch (shape)
            {
                case Square s when s.Side == 0:
                case Circle c when c.Radius == 0:
                case Triangle t when t.Base == 0 || t.Height == 0:
                case Rectangle r when r.Length == 0 || r.Height == 0:
                    return 0;

                case Square s:
                    return s.Side * s.Side;
                case Circle c:
                    return c.Radius * c.Radius * Math.PI;
                case Triangle t:
                    return t.Base * t.Height / 2;
                case Rectangle r:
                    return r.Length * r.Height;
                case null:
                    throw new ArgumentNullException(paramName: nameof(shape), message: "Shape must not be null");
                default:
                    throw new ArgumentException(
                        message: "shape is not a recognized shape",
                        paramName: nameof(shape));
            }
        }
    }



    public class Square
    {
        public double Side { get; }

        public Square(double side)
        {
            Side = side;
        }
    }
    public class Circle
    {
        public double Radius { get; }

        public Circle(double radius)
        {
            Radius = radius;
        }
    }
    public struct Rectangle
    {
        public double Length { get; }
        public double Height { get; }

        public Rectangle(double length, double height)
        {
            Length = length;
            Height = height;
        }
    }
    public class Triangle
    {
        public double Base { get; }
        public double Height { get; }

        public Triangle(double _base, double height)
        {
            Base = _base;
            Height = height;
        }
    }
}
