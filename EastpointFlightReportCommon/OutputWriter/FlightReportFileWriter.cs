using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EastpointFlightReportCommon.Writer
{
    public class FlightReportFileWriter
    {
        public void ExportFlightReport(FlightReport flightReport, string outputFilePath)
        {
            File.WriteAllText(outputFilePath, flightReport.ToString());
        }
    }
}
