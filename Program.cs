﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using oyasumi_lazer.Handlers;
using oyasumi_lazer.Objects;
using HttpMultipartParser;
using Newtonsoft.Json;
using oyasumi_lazer.Database;
using static oyasumi_lazer.Database.OyasumiDbContext;

namespace oyasumi_lazer
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            using var db = new OyasumiDbContext();
            var user = new Users 
            { 
                Username = "test",
                Country = "RU",
                Password = "dbtest"
            };
            db.DBUsers.Add(user);
            await db.SaveChangesAsync();
            var listener = new HttpListener();
            listener.Prefixes.Add("http://+:5005/");
            listener.Start();
            if (!File.Exists("config.json"))
            {
                File.WriteAllText("config.json", JsonConvert.SerializeObject(new ConfigScheme
                {
                    Database = "oyasumi",
                    Username = "root",
                    Password = ""
                }));
            }
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
                    var stream = request.InputStream;
                    /*using var sr = new StreamReader(stream);
                    var body = sr.ReadToEnd(); */
                    switch (request.Url.AbsolutePath)
                    {
                        case "/oauth/token":
                            var parser = await MultipartFormDataParser.ParseAsync(stream).ConfigureAwait(false);
                            var token = OAuth.Generate(parser.GetParameterValue("username"), parser.GetParameterValue("password"));
                            await response.OutputStream.WriteAsync(Encoding.UTF8.GetBytes(token), 0, Encoding.UTF8.GetBytes(token).Length);
                            break;
                        case "/v2/me":
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