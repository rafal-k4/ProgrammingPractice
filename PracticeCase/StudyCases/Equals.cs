using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCase.StudyCases
{
    public class Equals : IStudy
    {
        public void Execute()
        {
            PointWithoutOverride pointA = new PointWithoutOverride { X = 1, Y = 2 };
            PointWithoutOverride pointB = new PointWithoutOverride { X = 1, Y = 2 };

            var result = pointA.Equals(pointB); // false (reference equals)
            var result5 = pointA == pointB; // false (reference equals)
            pointA = pointB;

            var result2 = pointA.Equals(pointB); // true (reference equals)
            var result5B = pointA == pointB; // true (reference equals) 

            Point point2dA = new Point { X = 1, Y = 2 };
            Point3D point3dA = new Point3D { X = 1, Y = 2, Z = 3 };
            Point3D point3dB = new Point3D { X = 1, Y = 2, Z = 3 };

            var result3 = point2dA.Equals(point3dA); // true
            var result4 = point3dA.Equals(point3dB); // true

            ;
        }


        private class PointWithoutOverride
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public override bool Equals(object obj)
            {
                if(((Point)obj).X == X && ((Point)obj).Y == Y)
                {
                    return true;
                }

                return false;
            }
        }

        private class Point3D : Point
        {
            public int Z { get; set; }

            public override bool Equals(object obj)
            {
                if (((Point)obj).X == X && ((Point)obj).Y == Y)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
