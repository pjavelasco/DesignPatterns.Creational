using System;
using System.Collections.Generic;
using System.Text;

namespace Creational.Singleton.AmbientStack
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var house = new Building();

            using (new BuildingContext(3000))
            {
                // ground floor
                house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
                house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));

                // first floor
                using (new BuildingContext(3500))
                {
                    house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
                    house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));
                }

                // back to ground again
                house.Walls.Add(new Wall(new Point(5000, 0), new Point(5000, 4000)));
            }

            Console.WriteLine(house);
        }
    }

    // non-thread-safe global context
    public sealed class BuildingContext : IDisposable
    {
        public int WallHeight { get; set; } = 0;
        private static readonly Stack<BuildingContext> _stack = new();

        static BuildingContext()
        {
            // ensure there's at least one state
            _stack.Push(new BuildingContext(0));
        }

        public BuildingContext(int wallHeight)
        {
            WallHeight = wallHeight;
            _stack.Push(this);
        }

        public static BuildingContext Current => _stack.Peek();

        public void Dispose()
        {
            // not strictly necessary
            if (_stack.Count > 1)
            {
                _stack.Pop();                
            }
        }
    }

    public class Building
    {
        public List<Wall> Walls { get; set; } = new();

        public override string ToString()
        {
            var sb = new StringBuilder();
            Walls.ForEach(x => sb.AppendLine(x.ToString()));
            return sb.ToString();
        }
    }

    public struct Point
    {
        private readonly int _x;
        private readonly int _y;

        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override string ToString() => $"X: {_x}, Y: {_y}";
    }

    public class Wall
    {
        public Point Start, End;
        public int Height;

        public Wall(Point start, Point end)
        {
            Start = start;
            End = end;
            Height = BuildingContext.Current.WallHeight;
        }

        public override string ToString()
        {
            return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}, " +
                   $"{nameof(Height)}: {Height}";
        }
    }
}