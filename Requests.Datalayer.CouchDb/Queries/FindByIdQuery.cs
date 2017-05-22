using System.Threading.Tasks;
using AutoMapper;
using Cmas.BusinessLayers.Requests.Entities;
using Cmas.DataLayers.CouchDb.Requests.Dtos;
using Cmas.DataLayers.Infrastructure;
using Cmas.Infrastructure.Domain.Criteria;
using Cmas.Infrastructure.Domain.Queries;
using Microsoft.Extensions.Logging;
using System;

namespace Cmas.DataLayers.CouchDb.Requests.Queries
{
    public class FindByIdQuery : IQuery<FindById, Task<Request>>
    {
        private readonly IMapper _autoMapper;
        private readonly CouchWrapper _couchWrapper;

        public FindByIdQuery(IServiceProvider serviceProvider)
        {
            _autoMapper = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<Request> Ask(FindById criterion)
        {
            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Entities.GetAsync<RequestDto>(criterion.Id);
            });

            if (result == null)
                return null;

            return _autoMapper.Map<Request>(result.Content);
        }
    }
}