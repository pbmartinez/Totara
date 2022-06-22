namespace WebApi.Parameters
{
    public class QueryStringParameters
    {
        /// <summary>
        /// Fields to be fetched from the object. If empty, all properties will be fetched
        /// </summary>
        public string Fields { get; set; } = string.Empty;

        /// <summary>
        /// Navigation properties to be loaded from object. 
        /// To propagate navigation properties (eg ThenIncludes) use dot ".", ex: Category.SubCategories
        /// </summary>
        public string Includes { get; set; } = string.Empty;
    }
}
