using DotSpatial.Data;
using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotspatial_tests.Examples.Data
{
    static class ShapefileAnalysis
    {
        /// <summary>
        /// For all of the significant power that geometries provide, from doing overlay calculations to buffers and convex hulls, 
        /// they consume extra memory and are significantly slower for simple tasks. When all you need is access to the shapes, 
        /// parts and coordinates (as is the case for drawing) or even if you just need to determine if two shapes intersect, 
        /// you can rely on the FeatureSet.ShapeIndices. The code below demonstrates how this works in C#. 
        /// Point and MultiPoint shapes only have one part per shape, but in the case of MultiPart, that part may have more than one vertex. 
        /// For Lines, each part represents the vertices connected by straight lines, while separate parts are not connected. 
        /// For polygons, the parts may be islands or holes. According to the ArcGIS shapefile specification, 
        /// each part of a polygon is a simple polygon and the vertices of the polygons are ordered clockwise. 
        /// (Effectively so that the inside of the shape is on the right side of the line segments.) 
        /// Holes should be ordered counter clockwise. For rendering with MapWindow 6, as long as the winding order 
        /// of a hole is the opposite from the islands it will still draw correctly.
        /// </summary>
        public static void CycleThroughShapesPartsAndVertices(string shapefilePath)
        {
            // The feature set class works directly with vector data.
            // Opening a shapefile from disk loads data in "Index" mode by default.
            IFeatureSet fs = FeatureSet.Open(shapefilePath);

            // The shapes rely on an array of double precision interleaved
            // [X1, Y1, X2, Y2, ... Xn, Yn] coordinates.
            double x1 = fs.Vertex[0];
            double y1 = fs.Vertex[1];

            // The shaperange indexes values based on the coordinate index,
            // not the position in the double array.
            // Do access the coordinate directly from the Vertex, multiply by 2.
            x1 = fs.ShapeIndices[2].StartIndex * 2;
            y1 = fs.ShapeIndices[2].StartIndex * 2 + 1;

            // You can use the startindex and count in order to cycle values manually,
            // but it can be confusing.  To make things simpler, the ShapeIndices support an 
            // enumeration that allows cycling though the shapes, parts and the vertices.
            foreach (ShapeRange shape in fs.ShapeIndices)
            {
                foreach (PartRange part in shape.Parts)
                {
                    foreach (Vertex vertex in part)
                    {
                        if (vertex.X > 0 && vertex.Y > 0)
                        {
                            // do something
                            Console.WriteLine(vertex.X);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Testing for errors in X, Y coordinate positioning by saving using the Net Topology Suite version, while opening using the WkbFeatureReader.
        /// 
        /// FeatureSets exist in two basic "Modes". In one mode, they are "indexed" 
        /// which implies there is a single large vertex array and a series of "ShapeIndices" 
        /// that let you cycle through those vertices. The second mode is to simply have multiple Features in a Feature list, 
        /// and it becomes necessary to cycle through the coordinates of the geometries in order to do anything with the shapes. 
        /// Opening a shapefile automatically opens it into the indexed mode, 
        /// while creating a new shapefile automatically assumes that you want to work with the features list. 
        /// For importing from WKB there are two strategies.
        /// </summary>
        public static FeatureSet DuplicateShapes(string shapefilePath)
        {
            // Open the featureset using standard shapefile format
            IFeatureSet fs = FeatureSet.Open(shapefilePath);

            // The output featureset to be created from wkb
            FeatureSet result = new FeatureSet(fs.FeatureType);

            // Store the WKB representation in a list of arrays, each shape has one array.
            List<byte[]> binaryShapes = new List<byte[]>();

            // This forces the creation of features, and uses the old WKB converter
            // which was the NTS implementation of the standard OGC WKB.
            foreach (IFeature feature in fs.Features)
            {
                binaryShapes.Add(feature.ToBinary());
            }

            // Start by setting up a list of shapes
            List<Shape> shapes = new List<Shape>();

            // Loop through the binary arrays
            foreach (byte[] rawBytes in binaryShapes)
            {
                // read each 
                MemoryStream ms = new MemoryStream(rawBytes);
                shapes.Add(WkbFeatureReader.ReadShape(ms));
            }

            // As of the most recent source commit, this also works if it is false.
            result.IndexMode = true;
            // Adds the shapes to the result in one pass, so that there is less array re-dimensioning
            result.AddShapes(shapes);

            return result;
        }

        public static bool PointInPoligon(IFeatureSet fs, Coordinate c)
        {
            // Set up an index 
            int iShape = 0;

            // Cycle through the shapes
            foreach (ShapeRange shape in fs.ShapeIndices)
            {
                // Test if the coordinate is in the polygon
                if (shape.Intersects(c))
                {
                    // Select the polygon if the the coordinate intersects.
                    return true;
                }
                iShape++;
            }

            return false;
        }

    }
}
