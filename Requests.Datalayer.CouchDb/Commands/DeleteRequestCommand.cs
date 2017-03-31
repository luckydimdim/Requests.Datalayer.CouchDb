using System;
using System.Threading.Tasks;
using MyCouch;
using Cmas.Infrastructure.Domain.Commands;
using Cmas.BusinessLayers.Requests.CommandsContexts;

namespace Cmas.DataLayers.CouchDb.Requests.Commands
{
    public class DeleteRequestCommand : ICommand<DeleteRequestCommandContext>
    {
        public async Task<DeleteRequestCommandContext> Execute(DeleteRequestCommandContext commandContext)
        {
            using (var store = new MyCouchStore(DbConsts.DbConnectionString, DbConsts.DbName))
            {

                bool success = await store.DeleteAsync(commandContext.Id);

                if (!success)
                {
                    throw new Exception("error while deleting");
                }

                return commandContext;
            }

        }
    }
}
