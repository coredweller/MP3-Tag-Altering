using System;

namespace DomainObjects
{
    public interface ISetSong : IEntity
    {

        Guid SetSongId { get; set; }

        Guid? SetId { get; set; }

        string SongName { get; set; }

        double? Length { get; set; }

        bool Cover { get; set; }

        string Abbreviation { get; set; }

        short? Order { get; set; }

        string Tease { get; set; }

        string Notes { get; set; }

        string SpecialAppearances { get; set; }

        short? JamStyle { get; set; }

        bool BustOut { get; set; }

        bool Segue { get; set; }

    }
}
