using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public static class TaskExtensions
    {
        //TODO Add or Modify Method to use it as extension method instead of static class method

        /// <summary>
        /// Used for capture all exceptions when using WhenAll(params Task<T>[] tasks)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tasks"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<IEnumerable<T>> WhenAll<T>(params Task<T>[] tasks)
        {            
            var allTasks = Task.WhenAll(tasks);
            try
            {
                return await allTasks;
            }
            catch (Exception)
            {
                //ignore to later throw all exceptions raised during task execution
            }
            throw allTasks.Exception ?? throw new Exception("This exception should not be reached at TaskExtensions");
        }
    }
}
