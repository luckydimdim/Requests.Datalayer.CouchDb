using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.BusinessLayers.Requests.Criteria;
using Cmas.DataLayers.CouchDb.Requests.Dtos;
using Cmas.Infrastructure.Domain.Queries;
using CouchRequests = MyCouch.Requests;
using Cmas.DataLayers.Infrastructure;
using Cmas.BusinessLayers.Requests.Entities;
using System;

namespace Cmas.DataLayers.CouchDb.Requests.Queries
{
    public class FindByContractIdQuery : IQuery<FindByContractId, Task<IEnumerable<Request>>>
    {
        private readonly IMapper _autoMapper;
        private readonly CouchWrapper _couchWrapper;

        public FindByContractIdQuery(IServiceProvider serviceProvider)
        {
            _autoMapper = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<IEnumerable<Request>> Ask(FindByContractId criterion)
        {
            var result = new List<Request>();

            var query =
                new CouchRequests.QueryViewRequest(DbConsts.DesignDocumentName, DbConsts.ByContractDocsViewName)
                    .Configure(
                        q => q.Key(criterion.ContractId));

            var viewResult = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Views.QueryAsync<RequestDto>(query);
            });

            foreach (var row in viewResult.Rows.OrderByDescending(s => s.Value.CreatedAt))
            {
                result.Add(_autoMapper.Map<Request>(row.Value));
            }

            return result;
        }
    }
}