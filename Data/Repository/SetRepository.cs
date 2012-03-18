using System;
using System.Collections.Generic;
using System.Linq;
using DomainObjects;
using Core.Helpers;
using Repository;
using Core.Exceptions;
using Core.Infrastructure.Logging;

namespace Repository
{
    public class SetRepository : BaseRepository<ISet, Set>, ISetRepository
    {
        LogWriter writer = new LogWriter();
        public SetRepository(IDatabase database) : base(database) { }

        public SetRepository(IDatabaseFactory factory) : base(factory) { }

        private IQueryable<ISet> GetAll()
        {
            return Database.SetDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<ISet> FindAll()
        {
            return GetAll().OrderBy(s => s.SetId);
        }

        public ISet FindBySetId(Guid setId)
        {
            return GetAll().SingleOrDefault(set => set.SetId == setId);
        }

        public IQueryable<ISet> FindByShowId(Guid showId)
        {
            return GetAll().Where(set => set.ShowId == showId).OrderBy(x => x.SetNumber);
        }

        public override void Add(ISet entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(set => set.SetId == entity.SetId))
            {
                writer.WriteLine("A Set with an id={0}".FormatWith(entity.SetId));
                throw new AlreadyExistsException("A Set with an id={0}".FormatWith(entity.SetId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(ISet entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
