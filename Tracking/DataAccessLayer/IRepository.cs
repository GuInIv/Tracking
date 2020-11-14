using System;
using System.Collections.Generic;
using System.Linq;

namespace Tracking
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Read();
        IEnumerable<TEntity> Find(Func<TEntity, Boolean> predicate);
        void Add(TEntity entity);

    }
}
