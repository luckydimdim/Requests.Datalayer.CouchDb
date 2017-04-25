using System.Threading.Tasks;
using AutoMapper;
using Cmas.BusinessLayers.Requests.Entities;
using Cmas.DataLayers.CouchDb.Requests.Dtos;
using Cmas.DataLayers.Infrastructure;
using Cmas.Infrastructure.Domain.Criteria;
using Cmas.Infrastructure.Domain.Queries;
using Microsoft.Extensions.Logging;

namespace Cmas.DataLayers.CouchDb.Requests.Queries
{
    public class FindByIdQuery : IQuery<FindById, Task<Request>>
    {
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly CouchWrapper _couchWrapper;

        public FindByIdQuery(IMapper autoMapper, ILoggerFactory loggerFactory)
        {
            _autoMapper = autoMapper;
            _logger = loggerFactory.CreateLogger<FindByIdQuery>();
            _couchWrapper = new CouchWrapper(DbConsts.DbConnectionString, DbConsts.DbName, _logger);
        }

        public async Task<Request> Ask(FindById criterion)
        {
            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Entities.GetAsync<RequestDto>(criterion.Id);
            });

            return _autoMapper.Map<Request>(result.Content);
        }
    }
}