using Newtonsoft.Json.Converters;

namespace IO.Swagger.Helpers
{
    public class MyDateFormatConverter : IsoDateTimeConverter

    {

        public MyDateFormatConverter()

        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
