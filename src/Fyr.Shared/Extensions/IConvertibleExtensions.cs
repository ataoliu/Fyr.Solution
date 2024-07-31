using System.ComponentModel;
using System.Globalization;

namespace Fyr.Extensions;

public static class IConvertibleExtensions
{
    public static bool IsNumeric(this Type type)
    {
        switch (Type.GetTypeCode(type))
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
                return true;

            default:
                return false;
        }
    }

    /// <summary>
    /// 类型直转
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>

    public static T? ConvertTo<T>(this IConvertible value) where T : IConvertible
    {
        if (value != null)
        {
            var type = typeof(T);
            if (value.GetType() == type)
            {
                return (T)value;
            }

            if (type.IsEnum)
            {
                return (T)Enum.Parse(type, value.ToString(CultureInfo.InvariantCulture));
            }

            if (value == DBNull.Value)
            {
                return default;
            }

            if (type.IsNumeric())
            {
                return (T)value.ToType(type, new NumberFormatInfo());
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var underlyingType = Nullable.GetUnderlyingType(type);
                return (T)(underlyingType!.IsEnum ? Enum.Parse(underlyingType, value.ToString(CultureInfo.CurrentCulture)) : Convert.ChangeType(value, underlyingType));
            }

            var converter = TypeDescriptor.GetConverter(value);
            if (converter.CanConvertTo(type))
            {
                var result = converter.ConvertTo(value, type);
                if (result != null)
                    return (T)result;
                else
                    return default;
            }

            converter = TypeDescriptor.GetConverter(type);
            if (converter.CanConvertFrom(value.GetType()))
            {
                var result = converter.ConvertFrom(value);
                if (result != null)
                    return (T)result;
                else
                    return default;
            }

            return (T)Convert.ChangeType(value, type);
        }
        if (value != null)
            return (T)value;
        else
            return default;
    }

    /// <summary>
    /// 类型直转
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="defaultValue">转换失败的默认值</param>
    /// <returns></returns>
    public static T? TryConvertTo<T>(this IConvertible value, T? defaultValue = default) where T : IConvertible
    {
        try
        {
            return ConvertTo<T>(value);
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// 类型直转
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="result">转换失败的默认值</param>
    /// <returns></returns>
    public static bool TryConvertTo<T>(this IConvertible value, out T? result) where T : IConvertible
    {
        try
        {
            result = ConvertTo<T>(value);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    /// <summary>
    /// 类型直转
    /// </summary>
    /// <param name="value"></param>
    /// <param name="type">目标类型</param>
    /// <param name="result">转换失败的默认值</param>
    /// <returns></returns>
    public static bool TryConvertTo(this IConvertible value, Type type, out object? result)
    {
        try
        {
            result = ConvertTo(value, type);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    /// <summary>
    /// 类型直转
    /// </summary>
    /// <param name="value"></param>
    /// <param name="type">目标类型</param>
    /// <returns></returns>
    public static object? ConvertTo(this IConvertible? value, Type type)
    {
        if (value == null)
        {
            return default;
        }

        if (value.GetType() == type)
        {
            return value;
        }

        if (value == DBNull.Value)
        {
            return null;
        }

        if (type.IsAssignableFrom(typeof(string)))
        {
            return value.ToString();
        }

        if (type.IsEnum)
        {
            return Enum.Parse(type, value.ToString(CultureInfo.InvariantCulture));
        }

        if (type.IsAssignableFrom(typeof(Guid)))
        {
            Guid.TryParse(value.ToString(), out var result);
            return result;
        }

        if (type.IsAssignableFrom(typeof(DateTime)))
        {
            DateTime.TryParse(value.ToString(), out var result);
            return result;
        }

        if (type.IsAssignableFrom(typeof(DateTimeOffset)))
        {
            DateTimeOffset.TryParse(value.ToString(), out var result);
            return result;
        }
        if (type.IsNumeric())
        {

            return value.ToType(type, new NumberFormatInfo());
        }

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            return underlyingType!.IsEnum ? Enum.Parse(underlyingType, value.ToString(CultureInfo.CurrentCulture)) : Convert.ChangeType(value, underlyingType);
        }

        var converter = TypeDescriptor.GetConverter(value);
        if (converter.CanConvertTo(type))
        {
            return converter.ConvertTo(value, type);
        }

        converter = TypeDescriptor.GetConverter(type);
        return converter.CanConvertFrom(value.GetType()) ? converter.ConvertFrom(value) : Convert.ChangeType(value, type);
    }

    /// <summary>
    /// 对象类型转换
    /// </summary>
    /// <param name="this">当前值</param>
    /// <returns>转换后的对象</returns>
    public static T? ChangeTypeTo<T>(this object @this)
    {
        var result = ChangeType(@this, typeof(T));
        if (result != null)
            return (T)result;
        else return default;
    }

    /// <summary>
    /// 对象类型转换
    /// </summary>
    /// <param name="this">当前值</param>
    /// <param name="type">指定类型的类型</param>
    /// <returns>转换后的对象</returns>
    public static object? ChangeType(this object @this, Type type)
    {

        var currType = Nullable.GetUnderlyingType(@this.GetType()) ?? @this.GetType();
        type = Nullable.GetUnderlyingType(type) ?? type;
        if (@this == DBNull.Value)
        {
            if (!type.IsValueType)
            {
                return null;
            }
            throw new Exception("不能将null值转换为" + type.Name + "类型!");
        }

        if (currType == type)
        {
            return @this;
        }

        if (type.IsAssignableFrom(typeof(string)))
        {
            return @this.ToString();
        }

        if (type.IsEnum)
        {

            string value = @this.ToString() ?? string.Empty;
            return Enum.Parse(type, value, true);
        }

        if (type.IsAssignableFrom(typeof(Guid)))
        {
            Guid.TryParse(@this.ToString(), out var result);
            return result;
        }

        if (!type.IsArray || !currType.IsArray)
        {
            return Convert.ChangeType(@this, type);
        }

        var length = ((Array)@this).Length;
        // var targetType = Type.GetType(type.FullName?.Trim('[', ']'));
        var fullName = type.FullName?.Trim('[', ']') ?? throw new ArgumentNullException(nameof(type.FullName));
        var targetType = Type.GetType(fullName);
        if (targetType is null)
        {
            throw new ArgumentNullException(nameof(targetType), "The targetType cannot be null.");
        }
        var array = Array.CreateInstance(targetType, length);
        for (int j = 0; j < length; j++)
        {
            var tmp = ((Array)@this).GetValue(j);
            if (tmp != null)
                array.SetValue(ChangeType(tmp, targetType), j);
        }

        return array;
    }
}