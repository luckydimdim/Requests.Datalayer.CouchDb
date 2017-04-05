using System;
using AutoMapper;
using Cmas.DataLayers.CouchDb.Requests;
using System.Threading.Tasks;
using Cmas.BusinessLayers.Requests.Entities;
using Cmas.DataLayers.CouchDb.Requests.Queries;
using Cmas.Infrastructure.Domain.Criteria;

namespace ConsoleTests
{
    class Program
    {
        private static IMapper _mapper;

        static void Main(string[] args)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.AddProfile<AutoMapperProfile>();
                });

                _mapper = config.CreateMapper();


                FindByIdQueryTest().Wait();


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            Console.ReadKey();
        }

        static async Task<bool> FindByIdQueryTest()
        {
            FindByIdQuery findByIdQuery = new FindByIdQuery(_mapper);
            FindById criterion = new FindById("26270cfa2422b2c4ebf158285e0ccb73");
            Request result = null;

            try
            {
                result = await findByIdQuery.Ask(criterion);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            Console.WriteLine(result.Id);

            return true;
        }



    }
}