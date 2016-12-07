using DotSpatial.Data;
using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotspatial_tests.Examples.Data
{
    /// <summary>
    /// UX features, not included in this module:
    /// 
    /// Showing Multiple images on one layer C# 
    /// https://dotspatial.codeplex.com/wikipage?title=Multiple%20images%20on%20one%20layer&referringTitle=Desktop_SampleCode
    /// 
    /// Create a layer of random Points, then move them C# VB.Net
    /// https://dotspatial.codeplex.com/wikipage?title=RandomPoints&referringTitle=Desktop_SampleCode
    /// 
    /// </summary>
    class FeatureSets
    {
        public static void CreatingAPointInFeatureSet()
        {
            Coordinate[] c = new Coordinate[50];
            Random rnd = new Random();
            Feature f = new Feature();
            FeatureSet fs = new FeatureSet(f.FeatureType);
            for (int i = 0; i < 50; i++)
            {
                c[i] = new Coordinate((rnd.Next(0, 50) + 360) - 90, (rnd.NextDouble() * 360) - 180);
                fs.Features.Add(c[i]);
            }
            fs.SaveAs("C:\\Temp\\test.shp", true);
        }

        public static void CreatingALineInFeatureSet()
        {
            Random rnd = new Random();
            Feature f = new Feature();
            FeatureSet fs = new FeatureSet(f.FeatureType);
            for (int ii = 0; ii < 40; ii++)
            {
                Coordinate[] coord = new Coordinate[36];
                for (int i = 0; i < 36; i++)
                {
                    coord[i] = new Coordinate((rnd.NextDouble() * 360) - 180, (rnd.NextDouble() * 180) - 90);
                }
                LineString ls = new LineString(coord);
                f = new Feature(ls);
                fs.Features.Add(f);
            }
            fs.SaveAs("C:\\Temp\\test.shp", true);
        }

        public static void CreatingAPolygonInFeatureSet()
        {
            Random rnd = new Random();
            Polygon[] pg = new Polygon[100];
            Feature f = new Feature();
            FeatureSet fs = new FeatureSet(f.FeatureType);
            for (int i = 0; i < 100; i++)
            {
                Coordinate center = new Coordinate((rnd.Next(50) * 360) - 180, (rnd.Next(60) * 180) - 90);
                Coordinate[] coord = new Coordinate[50];
                for (int ii = 0; ii < 50; ii++)
                {
                    coord[ii] = new Coordinate(center.X + Math.Cos((ii * 10) * Math.PI / 10), center.Y + (ii * 10) * Math.PI / 10);
                }
                coord[49] = new Coordinate(coord[0].X, coord[0].Y);
                pg[i] = new Polygon(coord);
                fs.Features.Add(pg[i]);
            }
            fs.SaveAs("C:\\Temp\\test.shp", true);
        }

        public static void CreatePolygonFeatureSetAnd2AttributeColumns()
        { 
            // See comments below this code for an updated version.

            // define the feature type for this file
            FeatureSet fs = new FeatureSet(FeatureType.Polygon);


            // Add Some Columns
            fs.DataTable.Columns.Add(new DataColumn("ID", typeof(int)));
            fs.DataTable.Columns.Add(new DataColumn("Text", typeof(string)));

            // create a geometry (square polygon)
            List<Coordinate> vertices = new List<Coordinate>();

            vertices.Add(new Coordinate(0, 0));
            vertices.Add(new Coordinate(0, 100));
            vertices.Add(new Coordinate(100, 100));
            vertices.Add(new Coordinate(100, 0));
            Polygon geom = new Polygon(vertices);

            // add the geometry to the featureset. 
            IFeature feature = fs.AddFeature(geom);

            // now the resulting features knows what columns it has
            // add values for the columns
            feature.DataRow.BeginEdit();
            feature.DataRow["ID"] = 1;
            feature.DataRow["Text"] = "Hello World";
            feature.DataRow.EndEdit();


            // save the feature set
            fs.SaveAs("d:\\test.shp", true);

        }

        /// <summary>
        /// Presuming that you are creating a FeatureSet from scratch, you will have to add the features to the featureset. 
        /// However, when this FeatureSet is part of a layer, already, then it will initiate a re-draw every time a new feature is added. 
        /// This is useful for cases where you add one feature to an existing layer and you expect it to appear. 
        /// It can be a problem for performance, however, if you add a large number of features at once. 
        /// The example below illustrates how to suspend the events on the FeatureSet temporarily, allowing you to add the features more quickly. 
        /// When you are done, a single event will be fired during the resume events section, allowing the map to update.
        /// </summary>
        public static void AddFeaturesToFeatureSet(Feature[] features)
        {
            FeatureSet fs = new FeatureSet();
            fs.Features.SuspendEvents();
            for (int i = 0; i < features.Length; i++)
            {
                fs.Features.Add(features[i]);
            }
            fs.Features.ResumeEvents();
        }
    }
}
