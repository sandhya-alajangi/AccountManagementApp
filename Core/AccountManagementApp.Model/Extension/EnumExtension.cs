using System;
using AccountManagementApp.Model.Enumerators;

namespace AccountManagementApp.Model.Extension
{
    /// <summary>
    /// Extension class for Enums
    /// </summary>
    public static class EnumExtension
    {

        /// <summary>
        /// Returns proper string correspond to column delimiter
        /// </summary>
        /// <param name="instance">type of enum</param>
        /// <returns>matching string representation of Delimiter enums</returns>
        public static string ToStringValue(this Delimiter instance)
        {
            switch (instance)
            {
                case Delimiter.Comma:
                    return ",";
                case Delimiter.Semicolon:
                    return ";";
                case Delimiter.Tab:
                    return "\t";
                case Delimiter.VerticalBar:
                    return "|";
                case Delimiter.LineBreak:
                    return Environment.NewLine;
                case Delimiter.CarriageReturn:
                    return char.ConvertFromUtf32(13);
                case Delimiter.LineFeed:
                    return char.ConvertFromUtf32(10);
                default:
                    return string.Empty;
            }
        }


        /// <summary>
        ///  Returns value of based on it's Enum value
        /// </summary>
        /// <param name="instance">it holds the enum instance value</param>
        /// <returns>Value of enum type</returns>
        public static string ToStringValue(this TextQualifier instance)
        {
            switch (instance)
            {
                case TextQualifier.SingleQuotes:
                    return "'";
                case TextQualifier.DoubleQuotes:
                    return "\"";
                default:
                    return string.Empty;
            }
        }

    }
}