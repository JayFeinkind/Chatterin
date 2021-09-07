using Chatterin.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Chatterin.Services
{
    public abstract class ServiceBase<TEntity> where TEntity : class
    {
        protected ChatterinContext Context { get; }
        public ServiceBase(ChatterinContext context)
        {
            Context = context;
        }

        protected IQueryable<TEntity> ReadOnlyQuery => Context.Set<TEntity>().AsNoTracking();
        protected IQueryable<TEntity> PersistedQuery => Context.Set<TEntity>().AsTracking();
    }
}
