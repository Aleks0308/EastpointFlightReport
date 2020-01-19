using System;
using System.Collections.Generic;
using System.Text;

namespace EastpointFlightReportCommon.Models
{
    public class Route
    {
        public string Origin, Destination;
        public int CostPerPassenger, TicketPrice, MinimumTakeoffLoadPercentage;

        public Route(params string[] args) : 
            this(args[0], args[1], int.Parse(args[2]), int.Parse(args[3]), int.Parse(args[4]))
        { }

        public Route(string origin, string destination, int costPerPassenger, int ticketPrice, int minimumTakeoffLoadPercentage)
        {
            Origin = origin;
            Destination = destination;
            CostPerPassenger = costPerPassenger;
            TicketPrice = ticketPrice;
            MinimumTakeoffLoadPercentage = minimumTakeoffLoadPercentage;
        }
    }
}
