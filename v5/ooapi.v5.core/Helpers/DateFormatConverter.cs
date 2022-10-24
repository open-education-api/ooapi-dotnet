using Newtonsoft.Json.Converters;

namespace ooapi.v5.Helpers
{
    public class MyDateFormatConverter : IsoDateTimeConverter

    {

        public MyDateFormatConverter()

        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
