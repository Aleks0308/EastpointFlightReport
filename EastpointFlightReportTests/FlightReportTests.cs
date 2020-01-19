using EastpointFlightReportCommon.Parser;
using System;
using Xunit;

namespace EastpointFlightReportTests
{
    public class FlightReportTests
    {
        [Theory]
        [InlineData("TestData/London_Dublin-CanProceed.txt")]
        void FlightReport_CanProceed(string inputFileName)
        {
            var parser = new FlightReportFileParser();

            var flightReport = parser.ParseFlightReport(inputFileName);

            Assert.True(flightReport.CanFlightProceed());
        }

        [Theory]
        [InlineData("TestData/London_Dublin-CannotProceed.txt")]
        void FlightReport_CannotProceed(string inputFileName)
        {
            var parser = new FlightReportFileParser();

            var flightReport = parser.ParseFlightReport(inputFileName);

            Assert.False(flightReport.CanFlightProceed());
        }

        [Theory]
        [InlineData("TestData/2Aircrafts.txt")]
        void FlightReport_ThrowsIfMoreThan1Aircraft(string inputFileName)
        {
            var parser = new FlightReportFileParser();

            Assert.Throws<Exception>(() =>
            {
                var flightReport = parser.ParseFlightReport(inputFileName);
            });
        }

        [Theory]
        [InlineData("TestData/2Routes.txt")]
        void FlightReport_ThrowsIfMoreThan1Route(string inputFileName)
        {
            var parser = new FlightReportFileParser();

            Assert.Throws<Exception>(() =>
            {
                var flightReport = parser.ParseFlightReport(inputFileName);
            });
        }
    }
}
