using System.Globalization;

namespace ooapi.v5.core.Utility;

internal class Filter
{
    public string PropertyName { get; set; } = default!;

    public Operator Operation { get; set; }

    public object Value { get; set; } = default!;

    public static object? TryParseToType(object input, Type type)
    {
        var inputAsString = input?.ToString();

        if(inputAsString is null || inputAsString.ToLower() == "null")
        {
            return null;
        }

        return type switch
        {
            Type t when t == typeof(string) => inputAsString,
            Type t when t == typeof(long) => long.Parse(inputAsString),
            Type t when t == typeof(long?) => input as long?,
            Type t when t == typeof(int) => int.Parse(inputAsString),
            Type t when t == typeof(int?) => input as int?,
            Type t when t == typeof(short) => short.Parse(inputAsString),
            Type t when t == typeof(short?) => input as short?,
            Type t when t == typeof(DateTime) => DateTime.Parse(inputAsString, DateTimeFormatInfo.InvariantInfo),
            Type t when t == typeof(DateTime?) => DateTime.Parse(inputAsString, DateTimeFormatInfo.InvariantInfo),
            Type t when t == typeof(Guid) => Guid.Parse(inputAsString),
            Type t when t == typeof(Guid?) => Guid.Parse(inputAsString),
            Type t when t == typeof(bool) => bool.Parse(inputAsString),
            Type t when t == typeof(bool?) => bool.Parse(inputAsString),
            Type t when t == typeof(double) => double.Parse(inputAsString),
            Type t when t == typeof(double?) => double.Parse(inputAsString),
            Type t when t == typeof(float) => float.Parse(inputAsString),
            Type t when t == typeof(float?) => float.Parse(inputAsString),
            _ => inputAsString
        };
    }
}
