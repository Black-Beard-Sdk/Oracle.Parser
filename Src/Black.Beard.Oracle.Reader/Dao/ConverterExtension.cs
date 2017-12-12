using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using System.Data.Common;

namespace Bb.Beard.Oracle.Reader.Dao
{

    /// <summary>
    /// 
    /// </summary>
    public static class ConverterExtension
    {

        /// <summary>
        /// Froms the base64 string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static byte[] FromBase64String(string s)
        {
            return Convert.FromBase64String(s);
        }

        #region ToBase64String

        /// <summary>
        /// To the base64 string.
        /// </summary>
        /// <param name="inArray">The in array.</param>
        /// <returns></returns>
        public static string ToBase64String(byte[] inArray)
        {
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// To the base64 string.
        /// </summary>
        /// <param name="inArray">The in array.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static string ToBase64String(byte[] inArray, Base64FormattingOptions options)
        {
            return Convert.ToBase64String(inArray, options);
        }

        #endregion ToBase64String

        #region ToBoolean

        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static bool ToBoolean(this bool value)
        {
            return value;
        }

        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ToBoolean(this byte value)
        {
            return (value != 0);
        }

        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ToBoolean(this char value)
        {
            return ((IConvertible)value).ToBoolean(null);
        }

        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ToBoolean(this DateTime value)
        {
            return ((IConvertible)value).ToBoolean(null);
        }

        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ToBoolean(this decimal value)
        {
            return (value != 0M);
        }

        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ToBoolean(this double value)
        {
            return !(value == 0.0);
        }

        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ToBoolean(this short value)
        {
            return (value != 0);
        }

        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ToBoolean(this int value)
        {
            return (value != 0);
        }

        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ToBoolean(this long value)
        {
            return (value != 0L);
        }

        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ToBoolean(this sbyte value)
        {
            return (value != 0);
        }

        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ToBoolean(this float value)
        {
            return !(value == 0f);
        }

        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ToBoolean(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            var t = value.Trim().ToLower();
            if (t == "n" || t == "0" || t == "no" || t == "n/a" || t == "disabled" || t == "none")
                return false;
            else if (t == "y" || t == "yes" || t == "o" || t == "1" || t == "-1" || t == "enabled")
                return true;

            return bool.Parse(value);
        }

        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ToBoolean(this ushort value)
        {
            return (value != 0);
        }

        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ToBoolean(this uint value)
        {
            return (value != 0);
        }

        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ToBoolean(this ulong value)
        {
            return (value != 0L);
        }

        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static bool ToBoolean(this string value, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            return bool.Parse(value);
        }

        #endregion ToBoolean

        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static byte ToByte(this bool value)
        {
            if (!value)
                return 0;
            return 1;
        }

        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte ToByte(this byte value)
        {
            return value;
        }

        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static byte ToByte(this char value)
        {
            if (value > '\x00ff')
                throw new OverflowException("Overflow_Byte");
            return (byte)value;
        }

        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte ToByte(this DateTime value)
        {
            return ((IConvertible)value).ToByte(null);
        }


        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte ToByte(this decimal value)
        {
            return decimal.ToByte(decimal.Round(value, 0));
        }


        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte ToByte(this double value)
        {
            return ToByte(ToInt32(value));
        }


        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static byte ToByte(this short value)
        {
            if ((value < 0) || (value > 0xff))
                throw new OverflowException("Overflow_Byte");
            return (byte)value;
        }


        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static byte ToByte(this int value)
        {
            if ((value < 0) || (value > 0xff))
                throw new OverflowException("Overflow_Byte");
            return (byte)value;
        }


        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static byte ToByte(this long value)
        {
            if ((value < 0L) || (value > 0xffL))
                throw new OverflowException("Overflow_Byte");
            return (byte)value;
        }

        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static byte ToByte(this sbyte value)
        {
            if (value < 0)
                throw new OverflowException("Overflow_Byte");
            return (byte)value;
        }


        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte ToByte(this float value)
        {
            return ToByte((double)value);
        }


        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte ToByte(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            return byte.Parse(value, CultureInfo.CurrentCulture);
        }


        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static byte ToByte(this ushort value)
        {
            if (value > 0xff)
                throw new OverflowException("Overflow_Byte");
            return (byte)value;
        }


        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static byte ToByte(this uint value)
        {
            if (value > 0xff)
                throw new OverflowException("Overflow_Byte");
            return (byte)value;
        }


        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static byte ToByte(this ulong value)
        {
            if (value > 0xffL)
                throw new OverflowException("Overflow_Byte");
            return (byte)value;
        }

        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static byte ToByte(this string value, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            return byte.Parse(value, NumberStyles.Integer, provider);
        }

        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fromBase">From base.</param>
        /// <returns></returns>
        public static byte ToByte(this string value, int fromBase)
        {
            return Convert.ToByte(value, fromBase);
        }

        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static char ToChar(this bool value)
        {
            return ((IConvertible)value).ToChar(null);
        }

        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static char ToChar(this byte value)
        {
            return (char)value;
        }

        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static char ToChar(this char value)
        {
            return value;
        }

        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static char ToChar(this DateTime value)
        {
            return ((IConvertible)value).ToChar(null);
        }

        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static char ToChar(this decimal value)
        {
            return ((IConvertible)value).ToChar(null);
        }

        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static char ToChar(this double value)
        {
            return ((IConvertible)value).ToChar(null);
        }


        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static char ToChar(this short value)
        {
            if (value < 0)
                throw new OverflowException("Overflow_Char");
            return (char)((ushort)value);
        }


        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static char ToChar(this int value)
        {
            if ((value < 0) || (value > 0xffff))
                throw new OverflowException("Overflow_Char");
            return (char)value;
        }


        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static char ToChar(this long value)
        {
            if ((value < 0L) || (value > 0xffffL))
                throw new OverflowException("Overflow_Char");
            return (char)((ushort)value);
        }

        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static char ToChar(this sbyte value)
        {
            if (value < 0)
                throw new OverflowException("Overflow_Char");
            return (char)((ushort)value);
        }

        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static char ToChar(this float value)
        {
            return ((IConvertible)value).ToChar(null);
        }

        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static char ToChar(this string value)
        {
            return ToChar(value, null);
        }


        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static char ToChar(this ushort value)
        {
            return (char)value;
        }


        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static char ToChar(this uint value)
        {
            if (value > 0xffff)
                throw new OverflowException("Overflow_Char");
            return (char)value;
        }


        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static char ToChar(this ulong value)
        {
            if (value > 0xffffL)
                throw new OverflowException("Overflow_Char");
            return (char)value;
        }

        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">value</exception>
        /// <exception cref="System.FormatException"></exception>
        public static char ToChar(this string value, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");
            if (value.Length != 1)
                throw new FormatException("Format_NeedSingleChar");
            return value[0];
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this bool value)
        {
            return ((IConvertible)value).ToDateTime(null);
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this byte value)
        {
            return ((IConvertible)value).ToDateTime(null);
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this char value)
        {
            return ((IConvertible)value).ToDateTime(null);
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this DateTime value)
        {
            return value;
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this decimal value)
        {
            return ((IConvertible)value).ToDateTime(null);
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this double value)
        {
            return ((IConvertible)value).ToDateTime(null);
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this short value)
        {
            return ((IConvertible)value).ToDateTime(null);
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this int value)
        {
            return ((IConvertible)value).ToDateTime(null);
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long value)
        {
            return ((IConvertible)value).ToDateTime(null);
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this sbyte value)
        {
            return ((IConvertible)value).ToDateTime(null);
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this float value)
        {
            return ((IConvertible)value).ToDateTime(null);
        }


        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return new DateTime(0L);
            return DateTime.Parse(value, CultureInfo.CurrentCulture);
        }


        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this ushort value)
        {
            return ((IConvertible)value).ToDateTime(null);
        }


        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this uint value)
        {
            return ((IConvertible)value).ToDateTime(null);
        }


        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this ulong value)
        {
            return ((IConvertible)value).ToDateTime(null);
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(value))
                return new DateTime(0L);
            return DateTime.Parse(value, provider);
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static decimal ToDecimal(this bool value)
        {
            return (value ? 1 : 0);
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this byte value)
        {
            return value;
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this char value)
        {
            return ((IConvertible)value).ToDecimal(null);
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this DateTime value)
        {
            return ((IConvertible)value).ToDecimal(null);
        }


        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this decimal value)
        {
            return value;
        }


        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this double value)
        {
            return (decimal)value;
        }


        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this short value)
        {
            return value;
        }


        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this int value)
        {
            return value;
        }


        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this long value)
        {
            return value;
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this sbyte value)
        {
            return value;
        }


        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this float value)
        {
            return (decimal)value;
        }


        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0M;
            return decimal.Parse(value, CultureInfo.CurrentCulture);
        }


        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this ushort value)
        {
            return value;
        }


        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this uint value)
        {
            return value;
        }


        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this ulong value)
        {
            return value;
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this string value, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(value))
                return 0M;
            return decimal.Parse(value, NumberStyles.Number, provider);
        }


        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static double ToDouble(this bool value)
        {
            return (value ? ((double)1) : ((double)0));
        }


        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this byte value)
        {
            return (double)value;
        }

        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this char value)
        {
            return ((IConvertible)value).ToDouble(null);
        }

        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this DateTime value)
        {
            return ((IConvertible)value).ToDouble(null);
        }


        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this decimal value)
        {
            return (double)value;
        }


        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this double value)
        {
            return value;
        }


        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this short value)
        {
            return (double)value;
        }


        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this int value)
        {
            return (double)value;
        }


        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this long value)
        {
            return (double)value;
        }

        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this sbyte value)
        {
            return (double)value;
        }


        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this float value)
        {
            return (double)value;
        }


        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0.0;
            return double.Parse(value, CultureInfo.CurrentCulture);
        }


        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this ushort value)
        {
            return (double)value;
        }


        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this uint value)
        {
            return (double)value;
        }


        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this ulong value)
        {
            return (double)value;
        }

        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static double ToDouble(this string value, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(value))
                return 0.0;
            return double.Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, provider);
        }


        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static short ToInt16(this bool value)
        {
            if (!value)
                return 0;
            return 1;
        }


        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static short ToInt16(this byte value)
        {
            return value;
        }


        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static short ToInt16(this char value)
        {
            if (value > '翿')
                throw new OverflowException("Overflow_Int16");
            return (short)value;
        }

        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static short ToInt16(this DateTime value)
        {
            return ((IConvertible)value).ToInt16(null);
        }


        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static short ToInt16(this decimal value)
        {
            return decimal.ToInt16(decimal.Round(value, 0));
        }


        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static short ToInt16(this double value)
        {
            return ToInt16(ToInt32(value));
        }


        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static short ToInt16(this short value)
        {
            return value;
        }


        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static short ToInt16(this int value)
        {
            if ((value < -32768) || (value > 0x7fff))
                throw new OverflowException("Overflow_Int16");
            return (short)value;
        }


        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static short ToInt16(this long value)
        {
            if ((value < -32768L) || (value > 0x7fffL))
                throw new OverflowException("Overflow_Int16");
            return (short)value;
        }


        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static short ToInt16(this sbyte value)
        {
            return value;
        }


        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static short ToInt16(this float value)
        {
            return ToInt16((double)value);
        }


        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static short ToInt16(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            return short.Parse(value, CultureInfo.CurrentCulture);
        }


        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static short ToInt16(this ushort value)
        {
            if (value > 0x7fff)
                throw new OverflowException("Overflow_Int16");
            return (short)value;
        }


        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static short ToInt16(this uint value)
        {
            if (value > 0x7fffL)
                throw new OverflowException("Overflow_Int16");
            return (short)value;
        }

        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static short ToInt16(this ulong value)
        {
            if (value > 0x7fffL)
                throw new OverflowException("Overflow_Int16");
            return (short)value;
        }

        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static short ToInt16(this string value, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            return short.Parse(value, NumberStyles.Integer, provider);
        }

        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fromBase">From base.</param>
        /// <returns></returns>
        public static short ToInt16(this string value, int fromBase)
        {
            return Convert.ToInt16(value, fromBase);
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static int ToInt32(this bool value)
        {
            if (!value)
                return 0;
            return 1;
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ToInt32(this byte value)
        {
            return value;
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ToInt32(this char value)
        {
            return value;
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ToInt32(this DateTime value)
        {
            return ((IConvertible)value).ToInt32(null);
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ToInt32(this decimal value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static int ToInt32(this double value)
        {
            if (value >= 0.0)
            {
                if (value < 2147483647.5)
                {
                    int num = (int)value;
                    double num2 = value - num;
                    if ((num2 > 0.5) || ((num2 == 0.5) && ((num & 1) != 0)))
                        num++;
                    return num;
                }
            }
            else if (value >= -2147483648.5)
            {
                int num3 = (int)value;
                double num4 = value - num3;
                if ((num4 < -0.5) || ((num4 == -0.5) && ((num3 & 1) != 0)))
                    num3--;
                return num3;
            }
            throw new OverflowException("Overflow_Int32");
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ToInt32(this short value)
        {
            return value;
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ToInt32(this int value)
        {
            return value;
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static int ToInt32(this long value)
        {
            if ((value < -2147483648L) || (value > 0x7fffffffL))
                throw new OverflowException("Overflow_Int32");
            return (int)value;
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ToInt32(this sbyte value)
        {
            return value;
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ToInt32(this float value)
        {
            return ToInt32((double)value);
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ToInt32(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            return int.Parse(value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ToInt32(this ushort value)
        {
            return value;
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static int ToInt32(this uint value)
        {
            if (value > 0x7fffffff)
                throw new OverflowException("Overflow_Int32");
            return (int)value;
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static int ToInt32(this ulong value)
        {
            if (value > 0x7fffffffL)
                throw new OverflowException("Overflow_Int32");
            return (int)value;
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static int ToInt32(this string value, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            return int.Parse(value, NumberStyles.Integer, provider);
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fromBase">From base.</param>
        /// <returns></returns>
        public static int ToInt32(this string value, int fromBase)
        {
            return Convert.ToInt32(value, fromBase);
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static long ToInt64(this bool value)
        {
            return (value ? ((long)1) : ((long)0));
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToInt64(this byte value)
        {
            return (long)value;
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToInt64(this char value)
        {
            return (long)value;
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToInt64(this DateTime value)
        {
            return ((IConvertible)value).ToInt64(null);
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToInt64(this decimal value)
        {
            return decimal.ToInt64(decimal.Round(value, 0));
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToInt64(this double value)
        {
            return (long)Math.Round(value);
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToInt64(this short value)
        {
            return (long)value;
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToInt64(this int value)
        {
            return (long)value;
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToInt64(this long value)
        {
            return value;
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToInt64(this sbyte value)
        {
            return (long)value;
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToInt64(this float value)
        {
            return ToInt64((double)value);
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToInt64(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0L;
            return long.Parse(value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToInt64(this ushort value)
        {
            return (long)value;
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToInt64(this uint value)
        {
            return (long)value;
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static long ToInt64(this ulong value)
        {
            if (value > 0x7fffffffffffffffL)
                throw new OverflowException("Overflow_Int64");
            return (long)value;
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static long ToInt64(this string value, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(value))
                return 0L;
            return long.Parse(value, NumberStyles.Integer, provider);
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fromBase">From base.</param>
        /// <returns></returns>
        public static long ToInt64(this string value, int fromBase)
        {
            return Convert.ToInt64(value, fromBase);
        }

        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static sbyte ToSByte(this bool value)
        {
            if (!value)
                return 0;
            return 1;
        }

        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static sbyte ToSByte(this byte value)
        {
            if (value > 0x7f)
                throw new OverflowException("Overflow_SByte");
            return (sbyte)value;
        }

        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static sbyte ToSByte(this char value)
        {
            if (value > '\x007f')
                throw new OverflowException("Overflow_SByte");
            return (sbyte)value;
        }


        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static sbyte ToSByte(this DateTime value)
        {
            return ((IConvertible)value).ToSByte(null);
        }


        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static sbyte ToSByte(this decimal value)
        {
            return decimal.ToSByte(decimal.Round(value, 0));
        }


        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static sbyte ToSByte(this double value)
        {
            return ToSByte(ToInt32(value));
        }


        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static sbyte ToSByte(this short value)
        {
            if ((value < -128) || (value > 0x7f))
                throw new OverflowException("Overflow_SByte");
            return (sbyte)value;
        }


        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static sbyte ToSByte(this int value)
        {
            if ((value < -128) || (value > 0x7f))
                throw new OverflowException("Overflow_SByte");
            return (sbyte)value;
        }


        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static sbyte ToSByte(this long value)
        {
            if ((value < -128L) || (value > 0x7fL))
                throw new OverflowException("Overflow_SByte");
            return (sbyte)value;
        }

        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static sbyte ToSByte(this sbyte value)
        {
            return value;
        }

        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static sbyte ToSByte(this float value)
        {
            return ToSByte((double)value);
        }

        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static sbyte ToSByte(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            return sbyte.Parse(value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static sbyte ToSByte(this ushort value)
        {
            if (value > 0x7f)
                throw new OverflowException("Overflow_SByte");
            return (sbyte)value;
        }


        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static sbyte ToSByte(this uint value)
        {
            if (value > 0x7fL)
                throw new OverflowException("Overflow_SByte");
            return (sbyte)value;
        }

        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static sbyte ToSByte(this ulong value)
        {
            if (value > 0x7fL)
                throw new OverflowException("Overflow_SByte");
            return (sbyte)value;
        }

        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static sbyte ToSByte(this object value, IFormatProvider provider)
        {
            if (value != null)
                return ((IConvertible)value).ToSByte(provider);
            return 0;
        }

        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static sbyte ToSByte(this string value, IFormatProvider provider)
        {
            return sbyte.Parse(value, NumberStyles.Integer, provider);
        }

        /// <summary>
        /// To the s byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fromBase">From base.</param>
        /// <returns></returns>
        public static sbyte ToSByte(this string value, int fromBase)
        {
            return Convert.ToSByte(value, fromBase);
        }

        /// <summary>
        /// To the single.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static float ToSingle(this bool value)
        {
            return (value ? ((float)1) : ((float)0));
        }

        /// <summary>
        /// To the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float ToSingle(this byte value)
        {
            return (float)value;
        }

        /// <summary>
        /// To the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float ToSingle(this char value)
        {
            return ((IConvertible)value).ToSingle(null);
        }

        /// <summary>
        /// To the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float ToSingle(this DateTime value)
        {
            return ((IConvertible)value).ToSingle(null);
        }

        /// <summary>
        /// To the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float ToSingle(this decimal value)
        {
            return (float)value;
        }

        /// <summary>
        /// To the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float ToSingle(this double value)
        {
            return (float)value;
        }

        /// <summary>
        /// To the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float ToSingle(this short value)
        {
            return (float)value;
        }

        /// <summary>
        /// To the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float ToSingle(this int value)
        {
            return (float)value;
        }

        /// <summary>
        /// To the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float ToSingle(this long value)
        {
            return (float)value;
        }

        /// <summary>
        /// To the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float ToSingle(this sbyte value)
        {
            return (float)value;
        }


        /// <summary>
        /// To the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float ToSingle(this float value)
        {
            return value;
        }


        /// <summary>
        /// To the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float ToSingle(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0f;
            return float.Parse(value, CultureInfo.CurrentCulture);
        }


        /// <summary>
        /// To the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float ToSingle(this ushort value)
        {
            return (float)value;
        }


        /// <summary>
        /// To the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float ToSingle(this uint value)
        {
            return (float)value;
        }


        /// <summary>
        /// To the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float ToSingle(this ulong value)
        {
            return (float)value;
        }

        /// <summary>
        /// To the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static float ToSingle(this string value, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(value))
                return 0f;
            return float.Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, provider);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this bool value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this byte value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="toBase">To base.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this byte value, int toBase)
        {
            return ToString(value, toBase);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this char value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this DateTime value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this decimal value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this double value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this short value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="toBase">To base.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this short value, int toBase)
        {
            return Convert.ToString(value, toBase);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this int value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="toBase">To base.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this int value, int toBase)
        {
            return Convert.ToString(value, toBase);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this long value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="toBase">To base.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this long value, int toBase)
        {
            return Convert.ToString(value, toBase);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this sbyte value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this float value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this string value, IFormatProvider provider)
        {
            return value;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this ushort value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this uint value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this ulong value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static ushort ToUInt16(this bool value)
        {
            if (!value)
                return 0;
            return 1;
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ushort ToUInt16(this byte value)
        {
            return value;
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ushort ToUInt16(this char value)
        {
            return value;
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ushort ToUInt16(this DateTime value)
        {
            return ((IConvertible)value).ToUInt16(null);
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ushort ToUInt16(this decimal value)
        {
            return decimal.ToUInt16(decimal.Round(value, 0));
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ushort ToUInt16(this double value)
        {
            return ToUInt16(ToInt32(value));
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static ushort ToUInt16(this short value)
        {
            if (value < 0)
                throw new OverflowException("Overflow_UInt16");
            return (ushort)value;
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static ushort ToUInt16(this int value)
        {
            if ((value < 0) || (value > 0xffff))
                throw new OverflowException("Overflow_UInt16");
            return (ushort)value;
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static ushort ToUInt16(this long value)
        {
            if ((value < 0L) || (value > 0xffffL))
                throw new OverflowException("Overflow_UInt16");
            return (ushort)value;
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static ushort ToUInt16(this sbyte value)
        {
            if (value < 0)
                throw new OverflowException("Overflow_UInt16");
            return (ushort)value;
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ushort ToUInt16(this float value)
        {
            return ToUInt16((double)value);
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ushort ToUInt16(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            return ushort.Parse(value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ushort ToUInt16(this ushort value)
        {
            return value;
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static ushort ToUInt16(this uint value)
        {
            if (value > 0xffff)
                throw new OverflowException("Overflow_UInt16");
            return (ushort)value;
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static ushort ToUInt16(this ulong value)
        {
            if (value > 0xffffL)
                throw new OverflowException("Overflow_UInt16");
            return (ushort)value;
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static ushort ToUInt16(this string value, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            return ushort.Parse(value, NumberStyles.Integer, provider);
        }

        /// <summary>
        /// To the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fromBase">From base.</param>
        /// <returns></returns>
        public static ushort ToUInt16(this string value, int fromBase)
        {
            return Convert.ToUInt16(value, fromBase);
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static uint ToUInt32(this bool value)
        {
            if (!value)
                return 0;
            return 1;
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static uint ToUInt32(this byte value)
        {
            return value;
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static uint ToUInt32(this char value)
        {
            return value;
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static uint ToUInt32(this DateTime value)
        {
            return ((IConvertible)value).ToUInt32(null);
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static uint ToUInt32(this decimal value)
        {
            return decimal.ToUInt32(decimal.Round(value, 0));
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static uint ToUInt32(this double value)
        {
            if ((value < -0.5) || (value >= 4294967295.5))
                throw new OverflowException("Overflow_UInt32");
            uint num = (uint)value;
            double num2 = value - num;
            if ((num2 > 0.5) || ((num2 == 0.5) && ((num & 1) != 0)))
                num++;
            return num;
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static uint ToUInt32(this short value)
        {
            if (value < 0)
                throw new OverflowException("Overflow_UInt32");
            return (uint)value;
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static uint ToUInt32(this int value)
        {
            if (value < 0)
                throw new OverflowException("Overflow_UInt32");
            return (uint)value;
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static uint ToUInt32(this long value)
        {
            if ((value < 0L) || (value > 0xffffffffL))
                throw new OverflowException("Overflow_UInt32");
            return (uint)value;
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static uint ToUInt32(this sbyte value)
        {
            if (value < 0)
                throw new OverflowException("Overflow_UInt32");
            return (uint)value;
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static uint ToUInt32(this float value)
        {
            return ToUInt32((double)value);
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static uint ToUInt32(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            return uint.Parse(value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static uint ToUInt32(this ushort value)
        {
            return value;
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static uint ToUInt32(this uint value)
        {
            return value;
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static uint ToUInt32(this ulong value)
        {
            if (value > 0xffffffffL)
                throw new OverflowException("Overflow_UInt32");
            return (uint)value;
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static uint ToUInt32(this object value, IFormatProvider provider)
        {
            if (value != null)
                return ((IConvertible)value).ToUInt32(provider);
            return 0;
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static uint ToUInt32(this string value, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            return uint.Parse(value, NumberStyles.Integer, provider);
        }

        /// <summary>
        /// To the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fromBase">From base.</param>
        /// <returns></returns>
        public static uint ToUInt32(this string value, int fromBase)
        {
            return Convert.ToUInt32(value, fromBase);
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static ulong ToUInt64(this bool value)
        {
            if (!value)
                return 0L;
            return 1L;
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ulong ToUInt64(this byte value)
        {
            return (ulong)value;
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ulong ToUInt64(this char value)
        {
            return (ulong)value;
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ulong ToUInt64(this DateTime value)
        {
            return ((IConvertible)value).ToUInt64(null);
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ulong ToUInt64(this decimal value)
        {
            return decimal.ToUInt64(decimal.Round(value, 0));
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ulong ToUInt64(this double value)
        {
            return (ulong)Math.Round(value);
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static ulong ToUInt64(this short value)
        {
            if (value < 0)
                throw new OverflowException("Overflow_UInt64");
            return (ulong)value;
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static ulong ToUInt64(this int value)
        {
            if (value < 0)
                throw new OverflowException("Overflow_UInt64");
            return (ulong)value;
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static ulong ToUInt64(this long value)
        {
            if (value < 0L)
                throw new OverflowException("Overflow_UInt64");
            return (ulong)value;
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        public static ulong ToUInt64(this sbyte value)
        {
            if (value < 0)
                throw new OverflowException("Overflow_UInt64");
            return (ulong)value;
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ulong ToUInt64(this float value)
        {
            return ToUInt64((double)value);
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ulong ToUInt64(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0L;
            return ulong.Parse(value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ulong ToUInt64(this ushort value)
        {
            return (ulong)value;
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ulong ToUInt64(this uint value)
        {
            return (ulong)value;
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ulong ToUInt64(this ulong value)
        {
            return value;
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static ulong ToUInt64(this object value, IFormatProvider provider)
        {
            if (value != null)
                return ((IConvertible)value).ToUInt64(provider);
            return 0L;
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static ulong ToUInt64(this string value, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(value))
                return 0L;
            return ulong.Parse(value, NumberStyles.Integer, provider);
        }

        /// <summary>
        /// To the u int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fromBase">From base.</param>
        /// <returns></returns>
        public static ulong ToUInt64(this string value, int fromBase)
        {
            return Convert.ToUInt64(value, fromBase);
        }

    }

}