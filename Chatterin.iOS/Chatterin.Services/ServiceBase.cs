using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chatterin.ClassLibrary;
using SQLite;
using System.Linq;
using System.Linq.Expressions;

namespace Chatterin.Services
{
    /// <summary>
    /// provides generic support for querying web server for new data and replacing
    ///   db records.
    /// </summary>
    /// <typeparam name="TResource">Resource</typeparam>
    public abstract class ServiceBase<TResource> : IServiceBase<TResource> where TResource : Resource, new()
    {
        protected SQLiteAsyncConnection Db { get; private set; }
        protected ApiService ApiService { get; private set; }

        protected abstract string UrlAppendage { get; }
        readonly IConnectivityService _connectivityService;

        public ServiceBase(ApiService apiService, FileService fileService, IConnectivityService connectivityService)
        {
            _connectivityService = connectivityService;
            Db = new SQLiteAsyncConnection(fileService.DbFilePath);
            ApiService = apiService;
        }

        /// <summary>
        /// Retrieve data from webservice and replace data in the db
        /// </summary>
        /// <returns></returns>
        private async Task RefreshData()
        {
            var apiResult = await ApiService.GetAsync<List<TResource>>(UrlAppendage);

            // if server returns an error code do not update data
            if (apiResult.Success)
            {
                var data = apiResult.Data?.Result;
                await InsertData(data);
            }
        }

        /// <summary>
        /// Retrieve data from webservice (if internet connection available) and replace data in the db
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>List of resources from db</returns>
        public async Task<List<TResource>> GetAndInsert(Expression<Func<TResource, bool>> filter = null)
        {
            // if device is not connected, the data returned is from db
            if (_connectivityService.IsConnected)
            {
                await RefreshData();
            }

            return await QueryResource(filter);
        }

        /// <summary>
        /// Insert resources into sqlite db
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task InsertData(IEnumerable<TResource> entities)
        {
            await Db.Table<TResource>().DeleteAsync(e => true);

            if (entities?.Any() == true)
            {
                await Db.InsertAllAsync(entities);

                await InsertDependentResources(entities);
            }
        }

        /// <summary>
        /// Handle subordinate resources.  TODO look into making this
        ///    an attribute on resource properties
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        protected virtual Task InsertDependentResources(IEnumerable<TResource> entities)
        {
            return Task.FromResult(0);
        }

        protected virtual async Task<List<TResource>> QueryResource(Expression<Func<TResource, bool>> filter = null)
        {
            var query = Db.Table<TResource>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }
    }
}
