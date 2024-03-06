using IGDB.Attributes;
using System.Reflection;

namespace IGDB.Extensions
{
    internal static class TypeExtensions
    {

        public static string? GetIGDBUrl(this Type type)
        {
            var igdbURLAttribute = type.GetCustomAttribute<IGDBUrlAttribute>();
            if (igdbURLAttribute is null) return null;
            return igdbURLAttribute.Url;
        }

    }
}
