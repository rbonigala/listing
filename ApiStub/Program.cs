using Newtonsoft.Json;
using System;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int port;
            if (args.Length == 0 || !int.TryParse(args[0], out port))
                port = 6000;

            var server = FluentMockServer.Start(port);
            Console.WriteLine("FluentMockServer running at {0}", string.Join(",", server.Ports));

            // Order of rules matters. First matching is taken.
            server
                .Given(Request.Create().WithPath(u => u.Contains("/Valid")).UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody("[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]},{\"name\":\"Steve\",\"gender\":\"Male\",\"age\":45,\"pets\":null},{\"name\":\"Fred\",\"gender\":\"Male\",\"age\":40,\"pets\":[{\"name\":\"Tom\",\"type\":\"Cat\"},{\"name\":\"Max\",\"type\":\"Cat\"},{\"name\":\"Sam\",\"type\":\"Dog\"},{\"name\":\"Jim\",\"type\":\"Cat\"}]},{\"name\":\"Samantha\",\"gender\":\"Female\",\"age\":40,\"pets\":[{\"name\":\"Tabby\",\"type\":\"Cat\"}]},{\"name\":\"Alice\",\"gender\":\"Female\",\"age\":64,\"pets\":[{\"name\":\"Simba\",\"type\":\"Cat\"},{\"name\":\"Nemo\",\"type\":\"Fish\"}]}]"));

            server
                .Given(Request.Create().WithPath(u => u.Equals("/SingleMale")).UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody("[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]}]"));

            server
                .Given(Request.Create().WithPath(u => u.Contains("/SingleMaleNoPets")).UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody("[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[]}]"));

            server
                .Given(Request.Create().WithPath(u => u.Contains("/SingleMaleSingleDog")).UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody("[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Fido\",\"type\":\"Dog\"}]}]"));


            Console.WriteLine("Press any key to stop the server");
            Console.ReadKey();

            Console.WriteLine("Displaying all requests");
            var allRequests = server.LogEntries;
            Console.WriteLine(JsonConvert.SerializeObject(allRequests, Formatting.Indented));

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }
    }
}
