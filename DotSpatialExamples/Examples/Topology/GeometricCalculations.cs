using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotspatial_tests.Examples.Topology
{

    static class GeometricCalculations
    {

        // Sample code that will generate random an array of coordinates, creates a linestring from the array of coordinates, and calculates the length of the linestring.
        public static void CalculatingLineLength()
        {
            //Creates a coordinate arrary
            Coordinate[] coords = new Coordinate[36];
            //Creates a random number generator
            Random rnd = new Random();
            //A for loop that will generate random coordinate's and add those coordinates to the array
            for (int i = 0; i < 36; i++)
            {
                coords[i] = new Coordinate((rnd.NextDouble() * 360) - 180, (rnd.NextDouble() * 180) - 90);

            }
            //Creates a linestring from the array of coordinates
            LineString ls = new LineString(coords);
            //Calculates the length of the linestring
            Double length;
            length = ls.Length;

        }

        // Sample code that will generate a coordinate array, create a polygon from the coordinate array, and calculate the area of the polygon.
        public static void CalculatePolygonArea()
        {
            //Creates a new array of coordinates
            Coordinate[] coords = new Coordinate[20];
            //Creates a random number generator
            Random rnd = new Random();
            //Createa a center point for the polygon
            Coordinate center = new Coordinate((rnd.NextDouble() * 360) - 180, (rnd.NextDouble() * 180) - 90);
            //A For Loop that will randomly create coordinates and feeds the coordinates into the array of coordiantes
            for (int i = 0; i < 19; i++)
            {
                coords[i] = new Coordinate(center.X + Math.Cos((i * 10) * Math.PI / 10), center.Y + (i * 10) * Math.PI / 10);
            }
            //Set the last coordinate equal to the first coordinate in the array, thus 'closing' the polygon
            coords[19] = new Coordinate(coords[0].X, coords[0].Y);
            //Creates a new polygon from the coordinate array
            Polygon pg = new Polygon(coords);
            //Determines that area of the polygon
            Double area = pg.Area;
        }

        // Sample code that demonstrates how to buffer a randomly generated point.
        public static void BufferingAPoint()
        {
            Coordinate coords = new Coordinate();
            Random rnd = new Random();
            coords = new Coordinate((rnd.NextDouble() * 360) - 180, (rnd.NextDouble() * 180) - 90);
            DotSpatial.Topology.Point p = new DotSpatial.Topology.Point(coords);
            //This will get the area of the buffer.
            double area = p.Buffer(500).Area;
        }

    }
}
