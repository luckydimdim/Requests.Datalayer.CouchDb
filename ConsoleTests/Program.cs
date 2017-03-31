using System;
using AutoMapper;
using Cmas.DataLayers.CouchDb.Requests;

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
                 
                
                //AllEntitiesTest().Wait();
               

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            Console.ReadKey();
        }

    }
}