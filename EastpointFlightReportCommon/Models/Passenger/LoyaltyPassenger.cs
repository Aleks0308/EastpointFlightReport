using System;
using System.Collections.Generic;
using System.Text;

namespace EastpointFlightReportCommon.Models
{
    class LoyaltyPassenger : Passenger
    {
        public LoyaltyPassenger(params string[] args) 
            : this (args[0], int.Parse(args[1]), int.Parse(args[2]), args[3].Equals("TRUE"), args[4].Equals("TRUE"))
        {
        }

        public LoyaltyPassenger(string firstName, int age, int currentLoyaltyPoints, bool usingLoyaltyPoints, bool usingExtraBaggage)
            : base(firstName, age)
        {
            CurrentLoyaltyPoints = currentLoyaltyPoints;
            UsingLoyaltyPoints = usingLoyaltyPoints;
            UsingExtraBaggage = usingExtraBaggage;
        }

        public int CurrentLoyaltyPoints { get; set; }
        public bool UsingLoyaltyPoints { get; set; }
        public bool UsingExtraBaggage { get; set; }

        public override int GetBagCount()
        {
            var bagCount = base.GetBagCount();

            if(UsingExtraBaggage)
                return bagCount + 1;
            
            return bagCount;
        }

        public override int GetAdjustedTicketPrice(Route route)
        {
            int adjustedTicketPrice = route.TicketPrice - GetRedeemedLoyaltyPoints(route);

            return adjustedTicketPrice;
        }

        public int GetRedeemedLoyaltyPoints(Route route)
        {
            if (UsingLoyaltyPoints)
                return (CurrentLoyaltyPoints - route.TicketPrice) >= 0 ? route.TicketPrice : CurrentLoyaltyPoints;
            else return 0;
        }
    }
}
