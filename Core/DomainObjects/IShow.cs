using System;

namespace DomainObjects
{
    public interface IShow : IEntity
    {
        Guid ShowId { get; set; }
        string VenueName { get; }
        string City { get; }
        string State { get; }
        string Country { get; }
        int? Order { get; }
        decimal? TicketPrice { get; }
        string Notes { get; }
        DateTime? ShowDate { get; }
        string VenueNotes { get; set; }

        string GetShowName();
    }

    //Use this to represent the star system.
    public enum ShowRank
    {
        None = 0,
        Excellent = 1,
        Great = 2,
        Good = 3,
        Average = 4,
        BelowAverage = 5,
    }
}
