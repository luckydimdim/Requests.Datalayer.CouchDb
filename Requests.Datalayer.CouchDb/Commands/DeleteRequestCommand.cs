using System.Threading.Tasks;
using Cmas.Infrastructure.Domain.Commands;
using Cmas.BusinessLayers.Requests.CommandsContexts;
using Cmas.DataLayers.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Cmas.DataLayers.CouchDb.Requests.Commands
{
    public class DeleteRequestCommand : ICommand<DeleteRequestCommandContext>
    {
        private readonly ILogger _logger;
        private readonly CouchWrapper _couchWrapper;

        public DeleteRequestCommand(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DeleteRequestCommand>();
            _couchWrapper = new CouchWrapper(DbConsts.DbConnectionString, DbConsts.DbName, _logger);
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