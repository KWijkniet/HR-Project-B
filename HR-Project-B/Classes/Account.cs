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
        public int role;
        public string email;
        public string phone;

        public Account(string name, int _role, string _email, string _phone, string password = "secret")
        {
            this.id = Guid.NewGuid().ToString();
            this.name = name;
            this.password = password;
            this.role = _role;
            this.email = _email;
            this.phone = _phone;
        }

        public Account(dynamic data)
        {
            this.id = data.id;
            this.name = data.name;
            this.password = data.password;
            this.role = data.role;
            this.email = data.email;
            this.phone = data.phone;
        }
    }
}
