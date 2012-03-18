using System;
using System.Collections.Generic;
using DomainObjects;
using System.Linq;

namespace Repository
{
    public interface ISetSongRepository
    {
        IQueryable<ISetSong> FindAll();
        ISetSong FindBySetSongId(Guid id);
        ISetSong FindBySetId(Guid id);
        void Add(ISetSong entity);
        void Remove(ISetSong entity);
    }
}
