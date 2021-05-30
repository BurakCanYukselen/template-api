using System.Collections;

namespace API.Base.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty(this ICollection list)
        {
            return list == null || list.Count == 0;
        }
    }
}