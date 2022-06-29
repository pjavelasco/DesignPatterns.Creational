using System;

namespace Creational.Factory
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Point(2, 3, CoordinateSystem.Cartesian);
            var p2 = PointFactory.NewCartesianPoint(2, 3);
        }
    }

    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }

    public class Point
    {
        private double x, y;

        protected Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        // Constructor with too generic parameter names
        public Point(double a, double b, CoordinateSystem cs = CoordinateSystem.Cartesian)
        {
            switch (cs)
            {
                case CoordinateSystem.Polar:
                    x = a * Math.Cos(b);
                    y = a * Math.Sin(b);
                    break;
                    
                default:
                    x = a;
                    y = b;
                    break;
            }

            // steps to add a new system
            // 1. Increase CoordinateSystem
            // 2. Change constructor
        }

        // Factory methods
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }
    
    // Factory class
    public static class PointFactory
    {
        public static Point NewCartesianPoint(float x, float y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }

}
