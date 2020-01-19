using System;
using System.Collections.Generic;
using System.Text;

namespace EastpointFlightReportCommon.Models
{
    public class Aircraft
    {
        public string Title;
        public int NumberOfSeats;

        public Aircraft(params string[] args)
            : this(args[0], int.Parse(args[1]))
        { }

        public Aircraft(string title, int numberOfSeats)
        {
            Title = title;
            NumberOfSeats = numberOfSeats;
        }
    }
}
