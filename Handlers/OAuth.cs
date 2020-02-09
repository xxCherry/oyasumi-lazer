using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace oyasumi_lazer.Handlers
{ 
    internal class OAuth
    {
        public static List<OAuthScheme> TokenList = new List<OAuthScheme>();
        public static string Generate()
        {
            var token = new OAuthScheme
            {
                AccessToken = Guid.NewGuid().ToString(),
                ExpiresIn = DateTime.Now.AddDays(7).Ticks,
                RefreshToken = Guid.NewGuid().ToString()
            };
            TokenList.Add(token);
            return JsonConvert.SerializeObject(token);
        }
        public class OAuthScheme
        {
            public string AccessToken { get; set; }
            public long ExpiresIn { get; set; }
            public string RefreshToken { get; set; }
        }
    }
}
