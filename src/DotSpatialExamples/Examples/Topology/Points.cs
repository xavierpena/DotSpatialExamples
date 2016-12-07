using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotspatial_tests.Examples.Topology
{
    public static class Points
    {
        public static void CreatingANewPoint()
        {
            //creates a new coordinate 
            Coordinate c = new Coordinate(2.4, 2.4);
            //passes the coordinate to a new point
            DotSpatial.Topology.Point p = new DotSpatial.Topology.Point(c);
            //displayes the new point's x and y coordiantes
            Console.WriteLine("Point p is: x= " + p.X + " & y= " + p.Y);
        }

        // Creating a new linestring and calculating the length
        public static void CreatingANewLinestring()
        {
            //creates a new coordinate array
            Coordinate[] coords = new Coordinate[36];
            //creates a random point variable
            Random rnd = new Random();
            //a for loop that generates a new random X and Y value and feeds those values into the coordinate array
            for (int i = 0; i < 36; i++)
            {
                coords[i] = new Coordinate((rnd.NextDouble() * 360) - 180, (rnd.NextDouble() * 180) - 90);
            }
            //creates a new linstring from the array of coordinates
            LineString ls = new LineString(coords);
            //new variable for the length of the linstring
            Double length;
            length = ls.Length;
            //Displays the length of the linstring
            Console.WriteLine("The length of the linstring is: " + length);
        }

        // ample code that demonstrates the creation of a new polygon from random points and calculating the area
        public static void CreatingANewPolygon()
        {
            //creates a new coordinate array
            Coordinate[] coords = new Coordinate[10];
            //creates a random point variable
            Random rnd = new Random();
            //Creates the center coordiante for the new polygon
            Coordinate center = new Coordinate((rnd.NextDouble() * 360) - 180, (rnd.NextDouble() * 180) - 90);
            //a for loop that generates a new random X and Y value and feeds those values into the coordinate array
            for (int i = 0; i < 10; i++)
            {
                coords[i] = new Coordinate(center.X + Math.Cos((i * 2) * Math.PI / 18), center.Y + (i * 2) * Math.PI / 18);
            }
            //creates a new polygon from the coordinate array
            coords[9] = new Coordinate(coords[0].X, coords[0].Y);
            Polygon pg = new Polygon(coords);
            //new variable for the area of the polgyon
            Double area;
            area = pg.Area;
            //displays the area of the polygon
            Console.WriteLine("The Area of the polygon is: " + area);
        }

        // Sample code showing how to generate a polygon from random points that contains a hole.
        public static void CreatingANewPolygonWithHoles()
        {
            //Defines a new coordinate array
            Coordinate[] coords = new Coordinate[20];
            //Defines a new random number generator
            Random rnd = new Random();
            //defines a randomly generated center for teh polygon
            Coordinate center = new Coordinate((rnd.NextDouble() * 360) - 180, (rnd.NextDouble() * 180) - 90);
            for (int i = 0; i < 19; i++)
            {
                //generates random coordinates and adds those coordinates to the array
                coords[i] = new Coordinate(center.X + Math.Cos((i * 10) * Math.PI / 10), center.Y + (i * 10) * Math.PI / 10);
            }
            //sets the last coordinate equal to the first, this 'closes' the polygon
            coords[19] = new Coordinate(coords[0].X, coords[0].Y);
            //defines a new LingRing from the coordinates
            LinearRing Ring = new LinearRing(coords);
            //Repeates the process, but generates a LinearRing with a smaller area, this will be the hole in the polgyon
            Coordinate[] coordshole = new Coordinate[20];
            for (int i = 0; i < 20; i++)
            {
                coordshole[i] = new Coordinate(center.X + Math.Cos((i * 10) * Math.PI / 20), center.Y + (i * 10) * Math.PI / 20);
            }
            coordshole[19] = new Coordinate(coordshole[0].X, coordshole[0].Y);
            LinearRing Hole = new LinearRing(coordshole);
            //This steps addes the hole LinerRing to a ILinearRing Array
            //A Polgyon can contain multiple holes, thus a Array of Hole is required
            ILinearRing[] Holes = new ILinearRing[1];
            Holes[0] = Hole;
            //This passes the Ring, the polygon shell, and the Holes Array, the holes
            Polygon pg = new Polygon(Ring, Holes);
        }
    }
}
