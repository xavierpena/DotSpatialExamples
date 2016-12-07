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
    static class Basic
    {
        public static void CreatingLineFeature()
        {
            //Creates a random number generator
            Random rnd = new Random();
            //creates a new coordiante array
            Coordinate[] c = new Coordinate[36];
            //for loop that will generate 36 random numbers
            for (int i = 0; i < 36; i++)
            {
                c[i] = new Coordinate((rnd.NextDouble() * 360) - 180, (rnd.NextDouble() * 180) - 90);
            }
            //creates a linestring from the coordinate array
            LineString ls = new LineString(c);
            //creates a feature from the linestring
            Feature f = new Feature(ls);
            FeatureSet fs = new FeatureSet(f.FeatureType);
            fs.Features.Add(f);

        }

        public static void OpeningFS()
        {
            //Pass in the file path for the standard shapefile that will be opened
            IFeatureSet fs = FeatureSet.Open("C:\\Temp\\roads.shp");
        }

        public static void BufferingAFeatureSet()
        {
            //Declares a new feature set and passes in the file path for the standard 
            //shapefile that will be opened
            IFeatureSet fs = FeatureSet.Open(@"C:\[Your File Path]\Municipalities.shp");
            //Buffers the feature set "fs"
            IFeatureSet bs = fs.Buffer(10, true);
            //Saves the buffered feature set as a new file
            bs.SaveAs(@"C:\[Your File Path]\Municipalities_Buffer.shp", true);
        }

        public static void GettingNumberOfRowsInFeatureSet()
        {
            IFeatureSet fs = FeatureSet.Open(@"C:\[Your File Path]\Municipalities.shp");
            int numRows = fs.NumRows();
        }

        public static void GettingEntireAttributeTable()
        {
            IFeatureSet fs = FeatureSet.Open(@"C:\[Your File Path]\Municipalities.shp");
            fs.FillAttributes();
            DataTable dtOriginal = fs.DataTable;
            for (int row = 0; row < dtOriginal.Rows.Count; row++)
            {
                object[] original = dtOriginal.Rows[row].ItemArray;
            }
        }

        public static void GettingValueFromAttributeTable()
        {
            IFeatureSet fs = FeatureSet.Open(@"C:\[Your File Path\Municipalities.shp");
            fs.FillAttributes();
            DataTable dt = fs.DataTable;
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                object val = dt.Rows[row]["NAME"];
            }
        }

        public static void SavingFeatureSetAsNewShapefile()
        {
            //Declare a new feature set
            IFeatureSet fs = FeatureSet.Open(@"C:\[Your File Path]\Municipalities.shp");

            //Saves the open shapefile
            fs.SaveAs(@"C:\[Your File Path]\Municipalities_Test.shp", true);
        }

        public static void UnionShapes()
        {            
            IFeatureSet fs = FeatureSet.Open(@"C:\[Your File Path]\Centroids.shp");
            IFeatureSet result = fs.UnionShapes(ShapeRelateType.Intersecting);            
            result.SaveAs(@"C:\[Your File Path]\Municipalities_Test.shp", true);
        }
    }
}
