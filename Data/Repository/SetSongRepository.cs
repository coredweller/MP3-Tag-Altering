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
    public class SetSongRepository : BaseRepository<ISetSong, SetSong>, ISetSongRepository
    {
        LogWriter writer = new LogWriter();
        public SetSongRepository(IDatabase database) : base(database) { }

        public SetSongRepository(IDatabaseFactory factory) : base(factory) { }

        private IQueryable<ISetSong> GetAll()
        {
            return Database.SetSongDataSource.Where(setSong => setSong.Deleted == false);
        }

        public IQueryable<ISetSong> FindAll()
        {
            return GetAll();
        }

        public ISetSong FindBySetSongId(Guid id)
        {
            return GetAll().SingleOrDefault(x => x.SetSongId == id);
        }

        public IList<ISetSong> FindBySetId(Guid id)
        {
            return GetAll().Where( x => x.SetId == id ).OrderBy(y => y.Order).ToList();
        }

        public override void Add(ISetSong entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;


            if (GetAll().Any(x => x.SetSongId == entity.SetSongId))
            {
                writer.WriteLine("A SetSong with an id={0}".FormatWith(entity.SetSongId));
                throw new AlreadyExistsException("A SetSong with an id={0}".FormatWith(entity.SetSongId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(ISetSong entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
