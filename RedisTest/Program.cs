using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;

namespace RedisTest
{
    class Program
    {
        static string dbConnectionString = "localhost";
        static void Main(string[] args)
        {
            //using (RedisClient redisClient = new RedisClient(dbConnectionString))
            //{
            //    redisClient.FlushAll();
            //    for (int i = 0; i < 7000; i++)
            //    {
            //        redisClient.Set(i.ToString(), Guid.NewGuid());
            //    }
            //}
            var model = new TestModel();
            Console.WriteLine(model.Dump());
            Console.Read();
        }
    }

    public class TestModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
