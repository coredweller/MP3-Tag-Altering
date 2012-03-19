using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository;
using Core.Helpers;
using DomainObjects;

namespace Core.Services
{
    public class ShowService : IShowService
    {
        private readonly IShowRepository _ShowRepository;
        private readonly ISetRepository _SetRepository;
        private readonly ISetSongRepository _SetSongRepository;

        public ShowService( IShowRepository showRepository,
                            ISetRepository setRepository,
                            ISetSongRepository setSongRepository ) {

            Checks.Argument.IsNotNull( showRepository, "showRepository" );
            Checks.Argument.IsNotNull( setRepository, "setRepository" );
            Checks.Argument.IsNotNull( setSongRepository, "setSongRepository" );

            _ShowRepository = showRepository;
            _SetRepository = setRepository;
            _SetSongRepository = setSongRepository;
        }

        /// <summary>
        /// Gets the set list for the specified show date
        /// </summary>
        /// <param name="showDate">The date to get the set list for</param>
        /// <returns>A list of songs in the order of the setlist</returns>
        public IList<ISetSong> GetSetList( DateTime showDate ) {
            var show = _ShowRepository.FindByShowDate( showDate );

            if ( show == null ) return null;

            var sets = _SetRepository.FindByShowId( show.ShowId ).OrderBy( x => x.SetNumber.Value ).ToList();

            var setList = new List<ISetSong>();

            sets.ForEach( set => {
                    var setSongs = _SetSongRepository.FindBySetId( set.SetId );
                    setList.AddRange( setSongs );
                } );

            return setList;
        }
    }
}
