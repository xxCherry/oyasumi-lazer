using System;
using System.IO;
using System.Net;
using System.Text;
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
            XConsole.PrintInfo("oyasumi!lazer - custom server for osu!lazer\n  by Cherry, 2020");
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
                XConsole.PrintInfo(headers);
                XConsole.PrintInfo(request.Url.AbsolutePath);
                if (request.HttpMethod == "POST")
                {
                    using var sr = new StreamReader(request.InputStream);
                    XConsole.PrintInfo(await sr.ReadToEndAsync());
                    switch (request.Url.AbsolutePath)
                    {
                        case "/oauth/token":
                            await response.OutputStream.WriteAsync(Encoding.UTF8.GetBytes(OAuth.Generate()), 0, Encoding.UTF8.GetBytes(OAuth.Generate()).Length);
                            break;
                        default:
                            XConsole.PrintInfo($"Unknown requested path: {request.Url.AbsolutePath}");
                            break;
                    }
                    response.Close();
                }

                
            }
        }
    }
}