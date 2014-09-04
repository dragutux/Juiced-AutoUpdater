using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Forms;

namespace Updater
{
    class config
    {
        public string GetVariable(string key)
        {
            string value = "";

            using(StreamReader file = File.OpenText(Application.StartupPath + @"\launcher\config.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject obj = (JObject)JToken.ReadFrom(reader);
                value = (string)obj[key];
            }

            return value;
        }
    }
}
