using System.Threading.Tasks;
using AutoMapper;
using Cmas.BusinessLayers.Requests.CommandsContexts;
using Cmas.DataLayers.CouchDb.Requests.Dtos;
using Cmas.DataLayers.Infrastructure;
using Cmas.Infrastructure.Domain.Commands;
using Microsoft.Extensions.Logging;

namespace Cmas.DataLayers.CouchDb.Requests.Commands
{
    public class CreateRequestCommand : ICommand<CreateRequestCommandContext>
    {
        private IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly CouchWrapper _couchWrapper;

        public CreateRequestCommand(IMapper autoMapper, ILoggerFactory loggerFactory)
        {
            _autoMapper = autoMapper;
            _logger = loggerFactory.CreateLogger<CreateRequestCommand>();
            _couchWrapper = new CouchWrapper(DbConsts.DbConnectionString, DbConsts.DbName, _logger);
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