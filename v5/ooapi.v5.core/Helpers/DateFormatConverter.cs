﻿using Newtonsoft.Json.Converters;

namespace ooapi.v5.Helpers;

public class DateFormatConverter : IsoDateTimeConverter
{
    public DateFormatConverter(string format)
    {
        DateTimeFormat = format;
    }
}
