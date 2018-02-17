using System;
using System.Text;

namespace ZephyrHelper
{
    public class Authorization
    {
        private string userName; 
        private string password;

        public Authorization(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public string GetEncoding()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{password}"));
        }

        
    }
}
