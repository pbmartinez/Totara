using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Api
{
    public class ApiEndpoints
    {

        #region GET Methods
        public static class Get
        {
            public static string Usuario()
            {
                return $"/api/usuario";
            }
            public static string Usuario<TKey>(TKey id)
            {
                return $"/api/usuario/{id}";
            }
        }
        #endregion

        #region POST Methods
        public static class Post
        {
            public static string Usuario()
            {
                return $"/api/usuario";
            }

            
        }
        #endregion

        #region PUT Methods
        public static class Put
        {
            public static string Usuario<TKey>(TKey id)
            {
                return $"/api/usuarios/{id}";
            }
            
        }
        #endregion

        #region DELETE Methods
        public static class Delete
        {
            public static string Usuario<TKey>(TKey id)
            {
                return $"/api/usuarios/{id}";
            }
        } 
        #endregion
    }
}
