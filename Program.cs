using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using oyasumi_lazer.Handlers;


namespace oyasumi_lazer
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var listener = new HttpListener();
            listener.Prefixes.Add("http://+:5005/");
            listener.Start();
            Console.WriteLine("oyasumi!lazer - custom server for osu!lazer\nby Cherry, 2020");
            while (true)
            {
                var context = await listener.GetContextAsync();
                var request = context.Request;
                var response = context.Response;
                var headers = "";
                response.KeepAlive = true;
                response.AddHeader("Keep-Alive", "timeout=5, max=100");
                response.StatusCode = 200;
                response.AddHeader("Content-Type", "text/json; charset=UTF-8");
                foreach (var k in request.Headers.AllKeys) headers += k + ":" + " " + request.Headers[k] + "\n";
                var path = request.Url.AbsolutePath switch
                {
                    "/oauth/token" => OAuth.Generate(),
                };
                XConsole.PrintInfo(headers);
                XConsole.PrintInfo(request.Url.AbsolutePath);
                if (request.HttpMethod == "POST")
                {
                    using var sr = new StreamReader(request.InputStream);
                    XConsole.PrintInfo(await sr.ReadToEndAsync());
                    response.Close();
                }

                
            }
        }
    }
}