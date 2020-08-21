using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args)
        {
            Point3D point3D = default;
            {
                point3D.X = 3;
                point3D.Y = 3;
                point3D.Z = 3;
            };
            Program program = new Program();
            Point3D point3D1 = new Point3D()
            {
                X = 4,
                Y = 4,
                Z = 4,
            };

            Console.WriteLine(point3D.Distance);
            Console.WriteLine(point3D.ToString());

            Console.WriteLine(point3D1.Distance);
            Console.WriteLine(point3D1.ToString());
            Console.ReadKey();

        }
}

public struct Point3D
{
    public Point3D(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public double X { readonly get; set; }
    public double Y { readonly get; set; }
    public double Z { readonly get; set; }

    public readonly double Distance => Math.Sqrt(X * X + Y * Y + Z * Z);

    public readonly override string ToString() => $"{X}, {Y}, {Z}";
}
}
