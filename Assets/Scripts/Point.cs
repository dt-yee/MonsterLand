using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public class EqualityComparer : IEqualityComparer<Point>
    {

        public bool Equals(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public int GetHashCode(Point obj)
        {
            return (obj.X + 653) ^ (obj.Y + 257);
        }
    }
}
