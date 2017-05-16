using System.Threading.Tasks;
using AutoMapper;
using Cmas.BusinessLayers.Requests.CommandsContexts;
using Cmas.DataLayers.CouchDb.Requests.Dtos;
using Cmas.DataLayers.Infrastructure;
using Cmas.Infrastructure.Domain.Commands;
using System;

namespace Cmas.DataLayers.CouchDb.Requests.Commands
{
    public class CreateRequestCommand : ICommand<CreateRequestCommandContext>
    {
        private readonly IMapper _autoMapper;
        private readonly CouchWrapper _couchWrapper;

        public CreateRequestCommand(IServiceProvider serviceProvider)
        {
            _autoMapper = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<CreateRequestCommandContext> Execute(CreateRequestCommandContext commandContext)
        {
            var doc = _autoMapper.Map<RequestDto>(commandContext.Request);

            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Entities.PostAsync(doc);
            });

            commandContext.Id = result.Id;

            return commandContext;
        }
    }
}