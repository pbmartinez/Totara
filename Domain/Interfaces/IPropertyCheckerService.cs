using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPropertyCheckerService
    {
        /// <summary>
        /// It checks if certain type of T contains all properties specified in fields as comma separated values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fields"></param>
        /// <returns></returns>
        bool TypeHasProperties<T>(string fields);
    }
}
