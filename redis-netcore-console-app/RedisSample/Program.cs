using RestSharp;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new RedisManagerPool("localhost:6379");
            using (var client = manager.GetClient())
            {
                InsertUserDataToRedis(client);

                //client.RemoveAll(client.GetAllKeys());
                //Console.WriteLine("All keys removed.");
                //WriteAllRedisKeys(client);
            }

            Console.ReadKey();
        }

        private static void InsertUserDataToRedis(IRedisClient client)
        {
            var restClient = new RestClient("https://jsonplaceholder.typicode.com");
            var restReq = new RestRequest("users");
            IRestResponse<List<User>> restResp = restClient.Execute<List<User>>(restReq);

            if (restResp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<User> users = restResp.Data;
                users.ForEach(user => client.Set<User>($"user{user.id}", user));

                Console.WriteLine($"User Count : {users.Count}");
                Console.WriteLine($"Added to Redis to all users.");
                Console.WriteLine();
                WriteAllRedisKeys(client);
                Console.WriteLine();

                int firstUserId = users.First().id;
                User firstUser = client.Get<User>($"user{firstUserId}");
                Console.WriteLine($"First User (id : {firstUserId}): {firstUser}");
            }
            else
            {
                Console.WriteLine("Rest Response Error!!");
                Console.WriteLine($"Rest Response Status Code : {restResp.StatusCode}");
            }
        }

        private static void WriteAllRedisKeys(IRedisClient client)
        {
            Console.WriteLine($"Redis Key List : { string.Join(", ", client.GetAllKeys()) }");
        }
    }

    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public Address address { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public Company company { get; set; }

        public override string ToString()
        {
            return $"User : {id} {name} {username} {email} {phone} {website}{Environment.NewLine}{address}{Environment.NewLine}{company}";
        }
    }

    public class Address
    {
        public string street { get; set; }
        public string suite { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public Geo geo { get; set; }

        public override string ToString()
        {
            return $"Address : {street} {suite} {city} {zipcode}{Environment.NewLine}{geo}";
        }
    }

    public class Geo
    {
        public string lat { get; set; }
        public string lng { get; set; }

        public override string ToString()
        {
            return $"Geo : {lat} {lng}";
        }
    }

    public class Company
    {
        public string name { get; set; }
        public string catchPhrase { get; set; }
        public string bs { get; set; }

        public override string ToString()
        {
            return $"Company : {name} {catchPhrase} {bs}";
        }
    }

}
