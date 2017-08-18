using System;

namespace RestWell.Extensions
{
    public static class StringExtensions
    {
        #region Public Methods

        public static bool IsNullOrEmptyOrWhitespace(this string str)
        {
            return string.IsNullOrWhiteSpace(str) || str == String.Empty;
        }

        public static TEnum ToEnum<TEnum>(this string str, bool ignoreCase) where TEnum : struct
        {
            return Enum.Parse<TEnum>(str, ignoreCase);
        }

        #endregion Public Methods
    }
}
