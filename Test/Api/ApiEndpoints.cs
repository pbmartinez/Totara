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

        #region GET Methods
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
            public static string Brand()
            {
                return $"{BASE_URL}/api/brand";
            }
            public static string Brand<TKey>(TKey id)
            {
                return $"{BASE_URL}/api/brand/{id}";
            }
            public static string Provider()
            {
                return $"{BASE_URL}/api/provider";
            }
            public static string Provider<TKey>(TKey id)
            {
                return $"{BASE_URL}/api/provider/{id}";
            }
        }
        #endregion

        #region POST Methods
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
            public static string Brand()
            {
                return $"{BASE_URL}/api/brand";
            }

            public static string Provider()
            {
                return $"{BASE_URL}/api/provider";
            }
        }
        #endregion

        #region PUT Methods
        public static class Put
        {
            public static string Gateway<TKey>(TKey id)
            {
                return $"{BASE_URL}/api/gateway/{id}";
            }
            public static string Peripheral<TKey>(TKey id)
            {
                return $"{BASE_URL}/api/peripheral/{id}";
            }
            public static string Brand<TKey>(TKey id)
            {
                return $"{BASE_URL}/api/brand/{id}";
            }
            public static string Provider<TKey>(TKey id)
            {
                return $"{BASE_URL}/api/provider/{id}";
            }
        }
        #endregion

        #region DELETE Methods
        public static class Delete
        {
            public static string Gateway<TKey>(TKey id)
            {
                return $"{BASE_URL}/api/gateway/{id}";
            }
            public static string Peripheral<TKey>(TKey id)
            {
                return $"{BASE_URL}/api/peripheral/{id}";
            }
            public static string Brand<TKey>(TKey id)
            {
                return $"{BASE_URL}/api/brand/{id}";
            }
            public static string Provider<TKey>(TKey id)
            {
                return $"{BASE_URL}/api/provider/{id}";
            }
        } 
        #endregion
    }
}
