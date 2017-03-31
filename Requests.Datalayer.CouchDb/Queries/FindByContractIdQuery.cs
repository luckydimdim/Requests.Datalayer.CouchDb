using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.BusinessLayers.Requests.Criteria;
using Cmas.DataLayers.CouchDb.Requests.Dtos;
using Cmas.Infrastructure.Domain.Queries;
using MyCouch;
using CouchRequest = MyCouch.Requests;
using Request = Cmas.BusinessLayers.Requests.Entities.Request;

namespace Cmas.DataLayers.CouchDb.Requests.Queries
{
    public class FindByContractIdQuery : IQuery<FindByContractId, Task<IEnumerable<Request>>>
    {
        private IMapper _autoMapper;

        public FindByContractIdQuery(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public async Task<IEnumerable<Request>> Ask(FindByContractId criterion)
        {
            using (var client = new MyCouchClient(DbConsts.DbConnectionString, DbConsts.DbName))
            {

                var result = new List<Request>();

                var query = new CouchRequest.QueryViewRequest(DbConsts.DesignDocumentName, DbConsts.ByContractDocsViewName).Configure(q => q.Key(criterion.ContractId));

                var viewResult = await client.Views.QueryAsync<RequestDto>(query);

                foreach (var row in viewResult.Rows.OrderByDescending(s => s.Value.CreatedAt))
                {

                    var order = _autoMapper.Map<Request>(row.Value);
                    order.Id = row.Value._id;
                    result.Add(order);
                }

                return result;

            }

        }
    }
}
