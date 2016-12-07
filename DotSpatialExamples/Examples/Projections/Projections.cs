using DotSpatial.Data;
using DotSpatial.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotspatial_tests.Examples.Projections
{
    public static class Projections
    {
        //Sample code that will conduct a reprojection by reading in the ESRI.prj file
        public static void ReadingEsriPrjFiles()
        {
            //declares a new ProjectionInfo for the startind and ending coordinate systems
            //sets the start GCS to WGS_1984
            ProjectionInfo pStart = KnownCoordinateSystems.Geographic.World.WGS1984;
            ProjectionInfo pESRIEnd = ProjectionInfo.Open("C:\\Program Files\\ArcGIS\\Coordinate Systems\\Projected Coordinate Systems\\UTM\\WGS 1984\\WGS 1984 UTM Zone 1N.prj");
            //declares the point(s) that will be reprojected 
            double[] xy = new double[2];
            double[] z = new double[1];
            //calls the reprojection function 
            Reproject.ReprojectPoints(xy, z, pStart, pESRIEnd, 0, 1);

            Console.WriteLine("Points have been projected successfully");
        }

        //Code that allows the user to input a Proj4 string and reproject a WGS 1984 GCS to a Proj4 PCS
        public static void ReadingProj4Settings()
        {
            //Declares a new ProjectionInfo and sets it to GCS_WGS_1984
            ProjectionInfo pStart = new ProjectionInfo();
            pStart = KnownCoordinateSystems.Geographic.World.WGS1984;
            //Declares a new ProjectionInfo and allows the user to directly input a Proj4 string
            ProjectionInfo pEnd = ProjectionInfo.FromEsriString("+proj=aea +lat_1=20 +lat_2=-23 +lat_0=0 +lon_0=25 +x_0=0 +y_0=0 +ellps=WGS84 +datum=WGS84 +units=m +no_defs ");
            //Declares the point to be projected, starts out as 0,0
            double[] xy = new double[2];
            double[] z = new double[1];
            //calls the reproject function and reprojects the points
            Reproject.ReprojectPoints(xy, z, pStart, pEnd, 0, 1);

            Console.WriteLine("Reprojection is complete.");
        }

        public static void ReprojectPointUsingKnownCoordinateSystems()
        {
            //Sets up a array to contain the x and y coordinates
            double[] xy = new double[2];
            xy[0] = 0;
            xy[1] = 0;
            //An array for the z coordinate
            double[] z = new double[1];
            z[0] = 1;
            //Defines the starting coordiante system
            ProjectionInfo pStart = KnownCoordinateSystems.Geographic.World.WGS1984;
            //Defines the ending coordiante system
            ProjectionInfo pEnd = KnownCoordinateSystems.Projected.NorthAmerica.USAContiguousLambertConformalConic;
            //Calls the reproject function that will transform the input location to the output locaiton
            Reproject.ReprojectPoints(xy, z, pStart, pEnd, 0, 1);
            Console.WriteLine("The points have been reporjected.");
        }

        //Code for defining a geographic coordinate system for a feature set
        public static void AddingACoordinateSystemToFeatureSet(FeatureSet fs)
        {
            FeatureSet CopyFS = new FeatureSet();
            ProjectionInfo dest = default(ProjectionInfo);

            //Copies the selected layer to a new feature set
            //Prevents the original file from being edited
            CopyFS.CopyFeatures(fs, true);

            dest = KnownCoordinateSystems.Geographic.World.WGS1984;
            //Adds the geographic coordinate system to the feature set
            CopyFS.Projection = dest;
            //Saves the feature set
            CopyFS.SaveAs("C:\\Temp\\US_Cities_GCS_WGS1984.shp", true);

            Console.WriteLine("The feature was successfully been projected.");
        }
    }
}
