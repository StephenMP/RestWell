using System;

namespace RestWell.Extensions
{
    public static class StringExtensions
    {
        public static TEnum ToEnum<TEnum>(this string str, bool ignoreCase) where TEnum : struct
        {
            return Enum.Parse<TEnum>(str, ignoreCase);
        }

        public static bool IsNullOrEmptyOrWhitespace(this string str)
        {
            return string.IsNullOrWhiteSpace(str) || str == String.Empty;
        }
    }
}
