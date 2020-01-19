using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EastpointFlightReportCommon.Models;

namespace EastpointFlightReportCommon
{
    public class FlightReport
    {
        public Route Route;
        public Aircraft Aircraft;
        public List<Passenger> Passengers = new List<Passenger>();


        public int GetTotalPassengerCount()
        {
            return Passengers.Count;
        }

        public int GetGeneralPassengerCount()
        {
            return Passengers.OfType<GeneralPassenger>().Count();
        }

        public int GetAirlinePassengerCount()
        {
            return Passengers.OfType<AirlinePassenger>().Count();
        }

        public int GetLoyaltyPassengerCount()
        {
            return Passengers.OfType<LoyaltyPassenger>().Count();
        }

        public int GetTotalNumberOfBags()
        {
            return Passengers.Sum(p => p.GetBagCount());
        }

        public int GetTotalLoyaltyPointsRedeemed()
        {
            return Passengers.OfType<LoyaltyPassenger>().Where(p => p.UsingLoyaltyPoints)
                .Sum(p => p.GetRedeemedLoyaltyPoints(Route));
        }

        public int GetTotalCostOfFlight()
        {
            return Passengers.Count() * Route.CostPerPassenger;
        }

        public int GetTotalUnadjustedTicketRevenue()
        {
            return Passengers.Count() * Route.TicketPrice;
        }

        public int GetTotalAdjustedRevenue()
        {
            return Passengers.Sum(p => p.GetAdjustedTicketPrice(Route));
        }

        public bool CanFlightProceed()
        {
            var totalRevenue = GetTotalAdjustedRevenue();
            var totalCost = GetTotalCostOfFlight();
            var totalPassengers = GetTotalPassengerCount();
            var loadPercentage = ((double)totalPassengers / Aircraft.NumberOfSeats) * 100;

            if (totalRevenue <= totalCost)
                return false;
            if (totalPassengers > Aircraft.NumberOfSeats)
                return false;
            if (loadPercentage < Route.MinimumTakeoffLoadPercentage)
                return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(GetTotalPassengerCount());
            sb.Append(" ");
            sb.Append(GetGeneralPassengerCount());
            sb.Append(" ");
            sb.Append(GetAirlinePassengerCount());
            sb.Append(" ");
            sb.Append(GetLoyaltyPassengerCount());
            sb.Append(" ");
            sb.Append(GetTotalNumberOfBags());
            sb.Append(" ");
            sb.Append(GetTotalLoyaltyPointsRedeemed());
            sb.Append(" ");
            sb.Append(GetTotalCostOfFlight());
            sb.Append(" ");
            sb.Append(GetTotalUnadjustedTicketRevenue());
            sb.Append(" ");
            sb.Append(GetTotalAdjustedRevenue());
            sb.Append(" ");
            sb.Append(CanFlightProceed().ToString().ToUpper());

            return sb.ToString();
        }
    }
}
