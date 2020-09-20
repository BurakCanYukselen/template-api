using System;
using Newtonsoft.Json;

namespace API.Base.Core.Extensions
{
    public static class StringExtension
    {
        public static string ToJson(this object source)
        {
            return JsonConvert.SerializeObject(source);
        }
        
        public static string ToJson(this object source, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(source, settings);
        }

        public static TObject FromJson<TObject>(this string source)
        {
            return JsonConvert.DeserializeObject<TObject>(source);
        }

        public static TObject ConvertTo<TObject>(this string source)
        {
            return (TObject) Convert.ChangeType(source, typeof(TObject));
        }
    }
}