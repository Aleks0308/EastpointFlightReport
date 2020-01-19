using EastpointFlightReportCommon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EastpointFlightReportCommon.Models
{
    public class Passenger
    {
        public string FirstName;
        public int Age;

        public Passenger(params string[] args)
            : this(args[0], int.Parse(args[1]))
        { }

        public Passenger(string firstName, int age)
        {
            FirstName = firstName;
            Age = age;
        }

        public virtual int GetBagCount()
        {
            return 1;
        }

        public virtual int GetAdjustedTicketPrice(Route route)
        {
            return route.TicketPrice;
        }
    }
}
