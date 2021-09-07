using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Chatterin.Services
{
    public interface IServiceBase<TResource>
    {
        Task<List<TResource>> GetAndInsert(Expression<Func<TResource, bool>> filter = null);
        Task InsertData(IEnumerable<TResource> entities);
    }
}
