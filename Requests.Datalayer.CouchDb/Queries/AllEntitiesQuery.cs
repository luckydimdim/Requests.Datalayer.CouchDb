﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.Requests.Dtos;
using Cmas.DataLayers.Infrastructure;
using Cmas.Infrastructure.Domain.Criteria;
using Cmas.Infrastructure.Domain.Queries;
using Cmas.BusinessLayers.Requests.Entities;
using couchREquests = MyCouch.Requests;
using System;

namespace Cmas.DataLayers.CouchDb.Requests.Queries
{
    public class AllEntitiesQuery : IQuery<AllEntities, Task<IEnumerable<Request>>>
    {
        private readonly IMapper _autoMapper;
        private readonly CouchWrapper _couchWrapper;

        public AllEntitiesQuery(IServiceProvider serviceProvider)
        {
            _autoMapper = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<IEnumerable<Request>> Ask(AllEntities criterion)
        {
            var result = new List<Request>();

            var query = new couchREquests.QueryViewRequest(DbConsts.DesignDocumentName, DbConsts.AllDocsViewName);

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