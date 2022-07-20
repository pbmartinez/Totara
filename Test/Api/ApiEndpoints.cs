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
            public static string Usuario()
            {
                return $"{BASE_URL}/api/usuario";
            }
            public static string Usuario<TKey>(TKey id)
            {
                return $"{BASE_URL}/api/usuario/{id}";
            }
        }
        #endregion

        #region POST Methods
        public static class Post
        {
            public static string Usuario()
            {
                return $"{BASE_URL}/api/usuario";
            }

            
        }
        #endregion

        #region PUT Methods
        public static class Put
        {
            public static string Usuario<TKey>(TKey id)
            {
                return $"{BASE_URL}/api/usuario/{id}";
            }
            
        }
        #endregion

        #region DELETE Methods
        public static class Delete
        {
            public static string Usuario<TKey>(TKey id)
            {
                return $"{BASE_URL}/api/usuario/{id}";
            }
        } 
        #endregion
    }
}
