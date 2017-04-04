using System;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.BusinessLayers.Requests.Entities;
using Cmas.DataLayers.CouchDb.Requests.Dtos;
using Cmas.Infrastructure.Domain.Criteria;
using Cmas.Infrastructure.Domain.Queries;
using MyCouch;

namespace Cmas.DataLayers.CouchDb.Requests.Queries
{
    public class FindByIdQuery : IQuery<FindById, Task<Request>>
    {
        private IMapper _autoMapper;

        public FindByIdQuery(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public async Task<Request> Ask(FindById criterion)
        {
            using (var client = new MyCouchClient(DbConsts.DbConnectionString, DbConsts.DbName))
            {
                var result = await client.Entities.GetAsync<RequestDto>(criterion.Id);

                if (!result.IsSuccess)
                {
                    throw new Exception(result.Error);
                }

                return _autoMapper.Map<Request>(result.Content);
            }

        }
    }
}
