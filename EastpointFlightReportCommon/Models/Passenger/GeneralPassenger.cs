using System;
using System.Collections.Generic;
using System.Text;

namespace EastpointFlightReportCommon.Models
{
    class GeneralPassenger : Passenger
    {
        public GeneralPassenger(params string[] args) : base(args)
        {
        }

        public GeneralPassenger(string firstName, int age) : base(firstName, age)
        {
        }
    }
}
