using Newtonsoft.Json.Converters;

namespace ooapi.v5.Helpers;


public class DateFormatConverter : IsoDateTimeConverter
{

    /// <param name="format"></param>
    public DateFormatConverter(string format)
    {
        DateTimeFormat = format;
    }
}
