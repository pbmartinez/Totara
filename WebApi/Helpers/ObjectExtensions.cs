using System.Dynamic;
using System.Reflection;

namespace WebApi.Helpers
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// It returns a new Dynamic object from this TSource. 
        /// It selects the properties specified in 'fields' as comma sparated value format.
        /// If no field is specified, all properties are selected
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static ExpandoObject ShapeData<TSource>(this TSource source, string fields)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var dataShapedObject = new ExpandoObject();

            if (string.IsNullOrWhiteSpace(fields))
            {
                var propertyInfos = typeof(TSource).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                foreach (var propertyInfo in propertyInfos)
                {
                    var propertyValue = propertyInfo.GetValue(source);
                    dataShapedObject.TryAdd(propertyInfo.Name, propertyValue);
                }
                return dataShapedObject;
            }
            var fieldsAfterSplit = fields.Split(',');
            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();
                var propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                    throw new Exception($"Property {propertyName} wasn't found on {typeof(TSource)}");
                var propertyValue = propertyInfo.GetValue(source);
                dataShapedObject.TryAdd(propertyInfo.Name, propertyValue);
            }

            return dataShapedObject;
        }
    }
}
