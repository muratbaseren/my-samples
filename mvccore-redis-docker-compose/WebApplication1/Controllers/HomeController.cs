using Microsoft.AspNetCore.Mvc;
using RestSharp;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        private void InsertUserDataToRedis(IRedisClient client)
        {
            var restClient = new RestClient("https://jsonplaceholder.typicode.com");
            var restReq = new RestRequest("users");
            IRestResponse<List<User>> restResp = restClient.Execute<List<User>>(restReq);

            if (restResp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<User> users = restResp.Data;
                users.ForEach(user => client.Set<User>($"user{user.id}", user));

                WriteAllRedisKeys(client);
            }
            else
            {
                ViewBag.Error = $"Rest Response Status Code : {restResp.StatusCode}";
            }
        }

        private void WriteAllRedisKeys(IRedisClient client)
        {
            ViewBag.Result = $"Redis Key List : { string.Join(", ", client.GetAllKeys()) }";
        }

        public IActionResult Index()
        {
            using (var client = GetRedisClient())
            {
                InsertUserDataToRedis(client);
            }

            return View();
        }

        public IActionResult Details()
        {
            List<string> usernames = new List<string>();
            using (var client = GetRedisClient())
            {
                List<string> keys = client.GetAllKeys();
                IDictionary<string, User> users = client.GetAll<User>(keys);

                foreach (string key in users.Keys)
                {
                    usernames.Add(users[key].name);
                }
            }

            return View(usernames);
        }

        public IActionResult Privacy()
        {
            using (var client = GetRedisClient())
            {
                client.RemoveAll(client.GetAllKeys());
                ViewBag.Result = "All keys removed.";
            }

            return View();
        }

        private static IRedisClient GetRedisClient()
        {
            string redisIp = Dns.GetHostAddressesAsync("docker_redis_1").Result.FirstOrDefault().ToString();
            var manager = new RedisManagerPool(redisIp);
            return manager.GetClient();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

namespace WebApplication1
{
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
