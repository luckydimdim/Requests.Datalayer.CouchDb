using System;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.BusinessLayers.Requests.CommandsContexts;
using Cmas.DataLayers.CouchDb.Requests.Dtos;
using Cmas.Infrastructure.Domain.Commands;
using MyCouch;

namespace Cmas.DataLayers.CouchDb.Requests.Commands
{
    public class CreateRequestCommand : ICommand<CreateRequestCommandContext>
    {
        private IMapper _autoMapper;

        public CreateRequestCommand(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public async Task<CreateRequestCommandContext> Execute(CreateRequestCommandContext commandContext)
        {
            using (var store = new MyCouchStore(DbConsts.DbConnectionString, DbConsts.DbName))
            {
                var doc = _autoMapper.Map<RequestDto>(commandContext.Request);

                doc._id = null;
                doc._rev = null;

                var result = await store.Client.Entities.PostAsync(doc);

                if (!result.IsSuccess)
                {
                    throw new Exception(result.Error);
                }

                commandContext.Id = result.Id;


                return commandContext;
            }

        }
    }
}
