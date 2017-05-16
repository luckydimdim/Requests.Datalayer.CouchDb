using System.Threading.Tasks;
using AutoMapper;
using Cmas.BusinessLayers.Requests.CommandsContexts;
using Cmas.Infrastructure.Domain.Commands;
using Cmas.DataLayers.CouchDb.Requests.Dtos;
using Cmas.DataLayers.Infrastructure;
using System;

namespace Cmas.DataLayers.CouchDb.Requests.Commands
{
    public class UpdateRequestCommand : ICommand<UpdateRequestCommandContext>
    {
        private readonly IMapper _autoMapper;
        private readonly CouchWrapper _couchWrapper;

        public UpdateRequestCommand(IServiceProvider serviceProvider)
        {
            _autoMapper = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<UpdateRequestCommandContext> Execute(UpdateRequestCommandContext commandContext)
        {
            // FIXME: нельзя так делать, надо от frontend получать Rev
            var header = await _couchWrapper.GetHeaderAsync(commandContext.Request.Id);

            var entity = _autoMapper.Map<RequestDto>(commandContext.Request);

            entity._rev = header.Rev;

            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Entities.PutAsync(entity._id, entity);
            });

            return commandContext; // TODO: возвращать _revid
        }
    }
}