using System.Threading.Tasks;
using Cmas.Infrastructure.Domain.Commands;
using Cmas.BusinessLayers.Requests.CommandsContexts;
using Cmas.DataLayers.Infrastructure;
using System;

namespace Cmas.DataLayers.CouchDb.Requests.Commands
{
    public class DeleteRequestCommand : ICommand<DeleteRequestCommandContext>
    {
        private readonly CouchWrapper _couchWrapper;

        public DeleteRequestCommand(IServiceProvider serviceProvider)
        {
            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<DeleteRequestCommandContext> Execute(DeleteRequestCommandContext commandContext)
        {
            var header = await _couchWrapper.GetHeaderAsync(commandContext.Id);

            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Documents.DeleteAsync(commandContext.Id, header.Rev);
            });

            return commandContext;
        }
    }
}