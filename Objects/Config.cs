using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace oyasumi_lazer.Objects
{
    public class ConfigScheme
    {
        public string Database;
        public string Username;
        public string Password;
    }
    public class Config {
        public static ConfigScheme Get<CohfigScheme>()
        {
            return JsonConvert.DeserializeObject<ConfigScheme>(File.ReadAllText("config.json"));
        }
    }
}
