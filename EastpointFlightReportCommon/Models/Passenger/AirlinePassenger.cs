using System;
using System.Collections.Generic;
using System.Text;

namespace EastpointFlightReportCommon.Models
{
    public class AirlinePassenger : Passenger
    {
        public AirlinePassenger(params string[] args) : base(args)
        {
        }

        public AirlinePassenger(string firstName, int age) : base(firstName, age)
        {
        }

        public override int GetAdjustedTicketPrice(Route route)
        {
            return 0;
        }
    }
}
