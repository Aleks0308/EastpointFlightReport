using System;
using System.IO;
using EastpointFlightReportCommon;
using EastpointFlightReportCommon.Parser;
using EastpointFlightReportCommon.Writer;

namespace EastpointFlightReport
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "", outputFilePath = "";
            if(args.Length >= 2)
            {
                inputFilePath = args[0];
                outputFilePath = args[1];
            }
            else
            {
                Console.WriteLine("Please enter the input file path: ");
                inputFilePath = Console.ReadLine();
                Console.WriteLine("Please enter the output file path: ");
                outputFilePath = Console.ReadLine();
            }

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("Input file not found.");
                return;
            }

            try
            {
                var parser = new FlightReportFileParser();
                var flightReport = parser.ParseFlightReport(inputFilePath);

                var writer = new FlightReportFileWriter();
                writer.ExportFlightReport(flightReport, outputFilePath);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
