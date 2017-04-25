using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.BusinessLayers.Requests.Criteria;
using Cmas.DataLayers.CouchDb.Requests.Dtos;
using Cmas.Infrastructure.Domain.Queries;
using CouchRequests = MyCouch.Requests;
using Cmas.DataLayers.Infrastructure;
using Microsoft.Extensions.Logging;
using Cmas.BusinessLayers.Requests.Entities;

namespace Cmas.DataLayers.CouchDb.Requests.Queries
{
    public class FindByContractIdQuery : IQuery<FindByContractId, Task<IEnumerable<Request>>>
    {
        private IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly CouchWrapper _couchWrapper;

        public FindByContractIdQuery(IMapper autoMapper, ILoggerFactory loggerFactory)
        {
            _autoMapper = autoMapper;
            _logger = loggerFactory.CreateLogger<FindByContractIdQuery>();
            _couchWrapper = new CouchWrapper(DbConsts.DbConnectionString, DbConsts.DbName, _logger);
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