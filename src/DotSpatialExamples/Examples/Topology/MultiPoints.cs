using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotspatial_tests.Examples.Topology
{
    public static class MultiPoints
    {
        // Sample that demonstrates how to create a multipoint from randomly generated coordinates
        public static void CreatingNewMultipoint()
        {
            Coordinate[] c = new Coordinate[36];
            Random rnd = new Random();
            for (int i = 0; i < 36; i++)
            {
                c[i] = new Coordinate((rnd.NextDouble() + 360) - 180, (rnd.NextDouble() * 180) - 90);
            }
            MultiPoint Mps = new MultiPoint(c);
        }

        // Sample code that demonstrates how to create a multilinestring from randomly generated coordinates.
        public static void CreatingNewMultilinestring()
        {
            Random rnd = new Random();
            MultiLineString Mls = new MultiLineString();
            LineString[] ls = new LineString[40];
            for (int ii = 0; ii < 40; ii++)
            {
                Coordinate[] coord = new Coordinate[36];
                for (int i = 0; i < 36; i++)
                {
                    coord[i] = new Coordinate((rnd.NextDouble() * 360) - 180, (rnd.NextDouble() * 180) - 90);
                }
                ls[ii] = new LineString(coord);
            }
            Mls = new MultiLineString(ls);
        }

        // Sample code that demonstrates how to create a new mulitpolygon from randomly generated coordinates
        public static void CreatingNewMultipolygon()
        {
            Random rnd = new Random();
            Polygon[] pg = new Polygon[50];
            for (int i = 0; i< 50; i++)
            {
                Coordinate center = new Coordinate((rnd.NextDouble() * 360) - 180, (rnd.NextDouble() * 180) - 90);
                Coordinate[] coord = new Coordinate[36];
                for (int ii = 0; ii< 36; ii++)
                {
                    coord[ii] = new Coordinate(center.X + Math.Cos((ii* 10) * Math.PI / 10), center.Y + (ii* 10) * Math.PI / 10);
                }
                coord[35] = new Coordinate(coord[0].X, coord[0].Y);
                pg[i] = new Polygon(coord);
            }
            MultiPolygon mpg = new MultiPolygon(pg);
        }

    }
}
