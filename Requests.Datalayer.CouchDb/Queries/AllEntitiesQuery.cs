using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.Requests.Dtos;
using Cmas.Infrastructure.Domain.Criteria;
using Cmas.Infrastructure.Domain.Queries;
using MyCouch;
using CouchRequest = MyCouch.Requests;
using Request = Cmas.BusinessLayers.Requests.Entities.Request;

namespace Cmas.DataLayers.CouchDb.Requests.Queries
{
    public class AllEntitiesQuery : IQuery<AllEntities, Task<IEnumerable<Request>>>
    {
        private IMapper _autoMapper;

        public AllEntitiesQuery(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public async Task<IEnumerable<Request>> Ask(AllEntities criterion)
        {
            using (var client = new MyCouchClient(DbConsts.DbConnectionString, DbConsts.DbName))
            {
                var result = new List<Request>();

                var query = new CouchRequest.QueryViewRequest(DbConsts.DesignDocumentName, DbConsts.AllDocsViewName);

                var viewResult = await client.Views.QueryAsync<RequestDto>(query);

                foreach (var row in viewResult.Rows.OrderByDescending(s => s.Value.CreatedAt))
                {

                    var request = _autoMapper.Map<Request>(row.Value);
                    request.Id = row.Value._id;
                    result.Add(request);
                }

                return result;
            }
        }
    }
}
