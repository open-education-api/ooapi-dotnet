using System.Globalization;

namespace ooapi.v5.core.Utility;

internal class Filter
{
    public string PropertyName { get; set; } = default!;

    public Operator Operation { get; set; }

    public object Value { get; set; } = default!;

    public static object? TryParseToType(object input, Type type)
    {
        var inputAsString = input.ToString();

        if (inputAsString is null)
        {
            return null;
        }

        if (type == typeof(string))
        {
            return inputAsString;
        }
        else if (type == typeof(long))
        {
            return long.Parse(inputAsString);
        }
        else if (type == typeof(long?))
        {
            if (IsNull(input)) { return null; }
            return input as long?;
        }
        else if (type == typeof(int))
        {
            return int.Parse(inputAsString);
        }
        else if (type == typeof(int?))
        {
            if (IsNull(input)) { return null; }
            return input as int?;
        }
        else if (type == typeof(short))
        {
            return short.Parse(inputAsString);
        }
        else if (type == typeof(short?))
        {
            if (IsNull(input)) { return null; }
            return input as short?;
        }
        else if (type == typeof(DateTime))
        {
            return DateTime.Parse(inputAsString, DateTimeFormatInfo.InvariantInfo);
        }
        else if (type == typeof(DateTime?))
        {
            if (IsNull(input)) { return null; }
            return DateTime.Parse(inputAsString, DateTimeFormatInfo.InvariantInfo);
        }
        else if (type == typeof(Guid))
        {
            return Guid.Parse(inputAsString);
        }
        else if (type == typeof(Guid?))
        {
            if (IsNull(input)) { return null; }
            return Guid.Parse(inputAsString);
        }
        else if (type == typeof(bool))
        {
            return bool.Parse(inputAsString);
        }
        else if (type == typeof(bool?))
        {
            if (IsNull(input)) { return null; }
            return bool.Parse(inputAsString);
        }
        else if (type == typeof(double))
        {
            return double.Parse(inputAsString);
        }
        else if (type == typeof(double?))
        {
            if (IsNull(input)) { return null; }
            return double.Parse(inputAsString);
        }
        else if (type == typeof(float))
        {
            return float.Parse(inputAsString);
        }
        else if (type == typeof(float?))
        {
            if (IsNull(input)) { return null; }
            return float.Parse(inputAsString);
        }
        return inputAsString;
    }

    private static bool IsNull(object input)
    {
        return input is null || input.ToString()?.ToLower() == "null";
    }
}
