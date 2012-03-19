using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainObjects;

namespace Core.Services
{
    public interface IShowService
    {
        IList<ISetSong> GetSetList( DateTime showDate );
    }
}
