using System.Dynamic;
using System.Reflection;

namespace WebApi.Helpers
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// It returns a new IEnumerable of Dynamic object from this IEnumerable of TSource. 
        /// From each TSource object it selects the properties specified in 'fields' as comma sparated value format.
        /// If no field is specified, all properties are selected
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<ExpandoObject> ShapeDataOnIEnumerable<TSource>(this IEnumerable<TSource> source, string fields)
        {
            if(source==null)
                throw new ArgumentNullException(nameof(source));
            var expandoObjectList = new List<ExpandoObject>();
            var propertyInfoList = new List<PropertyInfo>();
            if (string.IsNullOrEmpty(fields))
            {
                var propertyInfos = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                propertyInfoList.AddRange(propertyInfos);
            }
            else
            {
                var fieldsAfterSplit = fields.Split(',');
                foreach (var field in fieldsAfterSplit)
                {
                    var propertyName = field.Trim();
                    var propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfo == null)
                        throw new Exception($"Property {propertyName} was not found on {typeof(TSource)}");
                    propertyInfoList.Add(propertyInfo);
                }
            }
            foreach (var sourceObject in source)
            {
                var dataShapedObject = new ExpandoObject();
                foreach (var propertyInfo in propertyInfoList)
                {
                    var propertyValue = propertyInfo.GetValue(sourceObject);
                    dataShapedObject.TryAdd(propertyInfo.Name, propertyValue);
                }
                expandoObjectList.Add(dataShapedObject);
            }
            return expandoObjectList;
        }
    }
}
