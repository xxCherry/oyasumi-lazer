﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using oyasumi_lazer.Objects;

namespace oyasumi_lazer.Handlers
{ 
    internal class OAuth
    {
        public static List<User> PlayerList = new List<User>();
        public static string Generate(string username, string password)
        {
            var token = new OAuthScheme
            {
                AccessToken = Guid.NewGuid().ToString(),
                ExpiresIn = DateTime.Now.AddDays(7).Ticks,
                RefreshToken = Guid.NewGuid().ToString()
            };
            var player = new User();
            
            PlayerList.Add(player);
            return JsonConvert.SerializeObject(token);
        }
    }
    public class OAuthScheme
    {
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
    }
}
