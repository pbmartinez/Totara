using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Api
{
    public class ApiEndpoints
    {
        static string BASE_URL = "https://localhost:7219";

        public static class Get 
        {
            public static string Gateway()
            {
                return $"{BASE_URL}/api/gateway";
            }
            public static string Gateway<TKey>(TKey id)
            {
                return $"{BASE_URL}/api/gateway/{id}";
            }

            public static string Peripheral()
            {
                return $"{BASE_URL}/api/peripheral";
            }
            public static string Peripheral<TKey>(TKey id)
            {
                return $"{BASE_URL}/api/peripheral/{id}";
            }
        }

        public static class Post 
        {
            public static string Gateway()
            {
                return $"{BASE_URL}/api/gateway";
            }

            public static string Peripheral()
            {
                return $"{BASE_URL}/api/peripheral";
            }
        }

    }
}
