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

                var dto = await client.Entities.GetAsync<RequestDto>(criterion.Id);

                Request result = _autoMapper.Map<Request>(dto.Content);
                result.Id = dto.Content._id;

                return result;
 
            }

        }
    }
}
