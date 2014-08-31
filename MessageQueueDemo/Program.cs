using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessageQueueDemo
{
    class Program
    {
        static string dbConnectionString = "localhost";

        static string channel_SubmitData = "submit_data";
        static string channel_suspect = "suspect_relation";

        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(x =>
                {
                    using (RedisClient redisClient = new RedisClient(dbConnectionString))
                    {
                        using (IRedisSubscription subscript = redisClient.CreateSubscription())
                        {
                            subscript.OnMessage = (channel, message) =>
                            {

                                Console.WriteLine(DateTime.Now.ToLongTimeString() + " 提交数据：" + message);
                                Thread.Sleep(500);
                                using (RedisClient redisPubClient = new RedisClient(dbConnectionString))
                                {
                                    redisPubClient.PublishMessage(channel_suspect, message);
                                }
                            };
                            subscript.SubscribeToChannels(channel_SubmitData);
                        }
                    }
                });

            ThreadPool.QueueUserWorkItem(x =>
            {
                using (RedisClient redisClient = new RedisClient(dbConnectionString))
                {
                    using (IRedisSubscription subscript = redisClient.CreateSubscription())
                    {
                        subscript.OnMessage = (channel, message) =>
                        {

                            Console.WriteLine(DateTime.Now.ToLongTimeString() + " 建立疑似关系：" + message);
                            Thread.Sleep(1000);
                        };
                        subscript.SubscribeToChannels(channel_suspect);
                    }
                }
            });
            Console.WriteLine("请提交数据");
            string cmd = Console.ReadLine();
            while (!string.IsNullOrEmpty(cmd))
            {
                if (cmd.StartsWith("batch"))
                {
                    int dataCount = 0;
                    if (int.TryParse(cmd.Substring(6, cmd.Length - 6), out dataCount))
                    {
                        Console.WriteLine("批量发布数据" + dataCount + "条");
                        using (RedisClient redisClient = new RedisClient(dbConnectionString))
                        {
                            for (var i = 1; i <= dataCount; i++)
                            {
                                redisClient.PublishMessage(channel_SubmitData, i.ToString());
                            }
                        }
                    }
                }
                else
                {
                    using (RedisClient redisClient = new RedisClient(dbConnectionString))
                    {
                        Console.WriteLine(DateTime.Now.ToLongTimeString() + " 发布数据" + cmd);
                        redisClient.PublishMessage(channel_SubmitData, cmd);
                    }
                }

                cmd = Console.ReadLine();
            }
        }
    }
}
