using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HR_Project_B
{
    class Account
    {
        public string id;
        public string name;
        public string password;

        public Account(string name, string password="secret")
        {
            this.id = Guid.NewGuid().ToString();
            this.name = name;
            this.password = password;
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public void fromJSON(string json)
        {
            Account acc = JsonConvert.DeserializeObject<Account>(json);
            id = acc.id;
            name = acc.name;
            password = acc.password;
        }
    }
}
