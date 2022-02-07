using System;
using System.Linq;
using System.Reflection;
using API.Base.Core.Models;

namespace API.Base.Core.Extensions
{
    public static class ObjectExtension
    {
        public static ApiResponse<object> ToApiResponse(this object source, bool success)
        {
            return new ApiResponse<object>(source) {Success = success};
        }
        
        public static TValue GetPropertyValue<TValue>(this PropertyInfo[] propertyInfos, object targetObject, params string[] propertyNames)
        {
            var propertyObject = propertyInfos.FirstOrDefault(p => propertyNames.Contains(p.Name, StringComparer.InvariantCultureIgnoreCase));
            if (propertyObject == null)
                return default;
            if (typeof(TValue) == typeof(Guid))
            {
                var guid = new Guid(propertyObject?.GetValue(targetObject).ToString());
                return (TValue) Convert.ChangeType(guid, typeof(TValue));
            }

            var objectValue = (TValue) propertyObject?.GetValue(targetObject);
            return objectValue;
        }
    }
}
