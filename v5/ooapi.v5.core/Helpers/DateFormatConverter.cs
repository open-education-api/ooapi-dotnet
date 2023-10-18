using Newtonsoft.Json.Converters;

namespace ooapi.v5.Helpers;

/// <summary>
/// 
/// </summary>
public class DateFormatConverter : IsoDateTimeConverter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="format"></param>
    public DateFormatConverter(string format)
    {
        DateTimeFormat = format;
    }
}
