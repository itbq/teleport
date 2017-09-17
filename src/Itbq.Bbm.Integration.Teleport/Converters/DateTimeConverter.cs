using Newtonsoft.Json.Converters;

namespace Itbq.Bbm.Integration.Teleport.Converters
{
    public class DateTimeConverter : IsoDateTimeConverter
    {
        public DateTimeConverter() => DateTimeFormat = "yyyy-MM-dd";
    }
}
