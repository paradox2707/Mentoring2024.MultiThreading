using Task1.Interfaces;
using Task1.Models;

namespace Task1.Repos;

public class MockTripRepository : ITripRepository
{
    public TripDetails LoadTrip(string touristName)
    {
        return new TripDetails
        {
            TouristName = touristName,
            FlyCost = 1000m,
            AccomodationCost = 2000m,
            ExcursionCost = 500m
        };
    }
}
