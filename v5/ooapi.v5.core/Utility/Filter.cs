namespace ooapi.v5.core.Utility;

class Filter
{
    public string PropertyName { get; set; }
    public Operator Operation { get; set; }
    public object Value { get; set; }

    public static object TryParseToType(object input, Type type)
    {
        if (type == typeof(string))
        {
            return input.ToString();
        }
        else if (type == typeof(long))
        {
            return long.Parse(input.ToString());
        }
        else if (type == typeof(long?))
        {
            if (IsNull(input)) { return null; }
            return input as long?;
        }
        else if (type == typeof(int))
        {
            return int.Parse(input.ToString());
        }
        else if (type == typeof(int?))
        {
            if (IsNull(input)) { return null; }
            return input as int?;
        }
        else if (type == typeof(short))
        {
            return short.Parse(input.ToString());
        }
        else if (type == typeof(short?))
        {
            if (IsNull(input)) { return null; }
            return input as short?;
        }
        else if (type == typeof(DateTime))
        {
            return DateTime.Parse(input.ToString());
        }
        else if (type == typeof(DateTime?))
        {
            if (IsNull(input)) { return null; }
            return DateTime.Parse(input.ToString());
        }
        else if (type == typeof(Guid))
        {
            return Guid.Parse(input.ToString());
        }
        else if (type == typeof(Guid?))
        {
            if (IsNull(input)) { return null; }
            return Guid.Parse(input.ToString());
        }
        else if (type == typeof(bool))
        {
            return bool.Parse(input.ToString());
        }
        else if (type == typeof(bool?))
        {
            if (IsNull(input)) { return null; }
            return bool.Parse(input.ToString());
        }
        else if (type == typeof(double))
        {
            return double.Parse(input.ToString());
        }
        else if (type == typeof(double?))
        {
            if (IsNull(input)) { return null; }
            return double.Parse(input.ToString());
        }
        else if (type == typeof(float))
        {
            float.Parse(input.ToString());
        }
        else if (type == typeof(float?))
        {
            if (IsNull(input)) { return null; }
            return float.Parse(input.ToString());
        }
        return input.ToString();
    }

    private static bool IsNull(object input)
    {
        return input == null || input.ToString().ToLower() == "null";
    }
}
