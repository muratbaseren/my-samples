using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQSample.Subcriber.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RabbitMQSample.Subcriber.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RabbitMQHelper _rabbitMQHelper;

        public HomeController(ILogger<HomeController> logger, RabbitMQHelper rabbitMQHelper)
        {
            _logger = logger;
            _rabbitMQHelper = rabbitMQHelper;

            if (!_rabbitMQHelper.IsStart)
            {
                _rabbitMQHelper.StartConsuming();
            }

        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult IndexAjax()
        {
            return Json(_rabbitMQHelper.Messages);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class RabbitMQHelper
    {
        public bool IsStart { get; set; }
        public List<string> Messages { get; set; }

        public RabbitMQHelper()
        {
            Messages = new List<string>();
        }

        public void StartConsuming()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "mylist1",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (object model, BasicDeliverEventArgs e) =>
            {
                var body = e.Body;
                var message = Encoding.UTF8.GetString(body.Span);

                Messages.Add(message);
            };

            channel.BasicConsume(
                queue: "mylist1",
                autoAck: true,
                consumer: consumer);

            IsStart = true;
        }
    }
}
