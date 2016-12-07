using DotSpatial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotspatial_tests.Examples.Data
{

    static class Rasters
    {

        /// <summary>
        /// Be sure to install the GDAL extension if you want to view different types of files.
        /// 
        /// Otherwise the only accepted format seems to be .bgd
        /// 
        /// `FileTypeNotSupported` error in the source code:
        /// https://github.com/DotSpatial/DotSpatial/blob/master/Source/DotSpatial.Data/DataManager.cs#L505
        /// </summary>
        public static void OpenRasterAndGetValues(string rasterFilePath)
        {
            IRaster r = DotSpatial.Data.Raster.Open(rasterFilePath);
            r.GetStatistics();
            double value = r.Maximum;
        }

        /// <summary>
        /// Since the easiest way to start working with any raster is to simply open a new raster, 
        /// the question is how can you get access to the strong typed data objects underneath for better performance.  
        /// In the following code, we don’t yet know what file accessor we are using, and we really don’t care, 
        /// as long as there is one.  
        /// This means that you must have used the Raster.New or Raster.Create methods in order to start up the new raster.  
        /// Just instantiating a “New” Raster<int>, for instance would produce a valid data element that 
        /// could not be used to open or read values from a file yet, since it doesn’t actually have any code for file access by itself.  
        /// However, it was provided at the Raster<T> level so that you could write a single block of code, 
        /// regardless of what file access class was actually used.  
        /// This allows for open ended inheritance for file access, while still allowing access to the ReadRaster method.
        /// </summary>
        public static void OpenIntegerRaster()
        {
            Raster r = new Raster();
            r.Open(); // this is valid even with no filename.  It will launch a dialog.
            if (r.DataType == typeof(int))
            {
                Raster<int> intRaster = r.ToIntRaster();
                if (intRaster.IsInRam)
                {
                    // Access data values on a raster small enough to fit in memory
                    for (int row = 0; row < intRaster.NumRows; row++)
                    {
                        for (int col = 0; col < intRaster.NumColumns; col++)
                        {
                            int value = intRaster.Data[row][col];
                        }
                    }
                }

                // Read data values from a huge raster
                int startX = 0;
                int startY = 0;
                int sizeX = 1000;
                int sizeY = 1000;
                int[][] data = intRaster.ReadRaster(startX, startY, sizeX, sizeY);
                for (int row = 0; row < 1000; row++)
                {
                    for (int col = 0; col < 1000; col++)
                    {
                        int value = data[row][col];
                    }
                }

            }
        }
    }
}
