using System.Threading.Tasks;
using Cmas.Infrastructure.Domain.Queries;
using System;
using Cmas.Infrastructure.Configuration;
using System.Net.Http;
using Cmas.BusinessLayers.Requests.Criteria;
using System.Net.Http.Headers;
using System.Text;

namespace Cmas.DataLayers.CouchDb.Requests.Queries
{
    public class GetCounterQuery : IQuery<GetCounter, Task<string>>
    {
        private readonly CmasConfiguration _configuration;

        public GetCounterQuery(IServiceProvider serviceProvider)
        {
            _configuration = serviceProvider.GetConfiguration();
        }

        public async Task<string> Ask(GetCounter criterion)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var login = _configuration.Databases.Requests.Login;
                    var password = _configuration.Databases.Requests.Password;

                    client.BaseAddress = new Uri(_configuration.Databases.Requests.ConnectionString);
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Basic",
                            Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{login}:{password}")));

                    var response = await client.PostAsync("/requests/_design/doc/_update/counter/requests-counter",
                        null);

                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}