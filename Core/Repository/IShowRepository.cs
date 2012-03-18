using System;
using DomainObjects;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public interface IShowRepository
    {
        void Add(IShow entity);
        IShow FindByShowDate(DateTime date);
        IShow FindByShowId(Guid id);
        IQueryable<IShow> FindShowsAfterDate(DateTime date);
        IQueryable<IShow> FindShowsBeforeDate(DateTime date);
        IQueryable<IShow> FindAll();
        void Remove(IShow entity);
    }
}
