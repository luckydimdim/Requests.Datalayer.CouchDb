using System;
using System.Threading.Tasks;
using AutoMapper;
using MyCouch;
using Cmas.BusinessLayers.Requests.CommandsContexts;
using Cmas.Infrastructure.Domain.Commands;
using Cmas.DataLayers.CouchDb.Requests.Dtos;

namespace Cmas.DataLayers.CouchDb.Requests.Commands
{
 
    public class UpdateRequestCommand : ICommand<UpdateRequestCommandContext>
    {

        private IMapper _autoMapper;

        public UpdateRequestCommand(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public async Task<UpdateRequestCommandContext> Execute(UpdateRequestCommandContext commandContext)
        {
            using (var client = new MyCouchClient(DbConsts.DbConnectionString, DbConsts.DbName))
            {
                // FIXME: нельзя так делать, надо от frontend получать
                var existingDoc = (await client.Entities.GetAsync<RequestDto>(commandContext.Request.Id)).Content;
 
                var newDto = _autoMapper.Map<RequestDto>(commandContext.Request);
                newDto._id = existingDoc._id;
                newDto._rev = existingDoc._rev;

                var result = await client.Entities.PutAsync(newDto._id, newDto);

                if (!result.IsSuccess)
                {
                    throw new Exception(result.Error);
                }

                // TODO: возвращать _revid

                return commandContext;
            }

        }
    }
}
