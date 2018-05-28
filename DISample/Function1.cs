
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Autofac;

namespace DISample
{


    public static class Function1
    {
        private static IContainer Container { get; set; }

        static Function1() {
            var builder = new ContainerBuilder();
            builder.RegisterType<ServiceAImpl>().As<IServiceA>().SingleInstance();
            builder.RegisterType<ServiceBImpl>().As<IServiceB>().SingleInstance();
            Container = builder.Build();
        }

        [FunctionName("Function1")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function1 processed a request.");
            using (var scope = Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IServiceA>();

                return (ActionResult)new OkObjectResult($"Hello, {service.GetMessage()}: instanceId: {service.GetInstanceId()}");
            }
        }

        [FunctionName("Function2")]
        public static IActionResult Function2([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function2 processed a request.");
            using (var scope = Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IServiceA>();

                return (ActionResult)new OkObjectResult($"Hello, {service.GetMessage()}: instanceId: {service.GetInstanceId()}");
            }
        }
    }
}
