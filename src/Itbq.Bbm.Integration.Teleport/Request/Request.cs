using System;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

using Itbq.Bbm.Integration.Teleport.Converters;
using Newtonsoft.Json;

namespace Itbq.Bbm.Integration.Teleport.Request
{
    public abstract class Request
    {
        [JsonProperty("first_name", Required = Required.Always)]
        public string FirtsName { get; set; }

        [JsonProperty("middle_name", Required = Required.Always)]
        public string MiddleName { get; set; }

        [JsonProperty("last_name", Required = Required.Always)]
        public string LastName { get; set; }

        [JsonProperty("birthday", Required = Required.Always)]
        [JsonConverter(typeof(DateTimeConverter))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [JsonProperty("amount", Required = Required.Always)]
        public decimal Amount { get; set; }

        [JsonProperty("period", Required = Required.Always)]
        public int Period { get; set; }

        [JsonProperty("phone", Required = Required.Always)]
        public string Phone { get; set; }

        [JsonProperty("residential_region", Required = Required.Always)]
        public string ResidentialRegion { get; set; }

        [JsonProperty("residential_city", Required = Required.Always)]
        public string ResidentialCity { get; set; }

        [JsonProperty("registration_region", Required = Required.Always)]
        public string RegistrationRegion { get; set; }

        [JsonProperty("registration_city", Required = Required.Always)]
        public string RegistrationCity { get; set; }

        [JsonProperty("email", Required = Required.DisallowNull)]
        [EmailAddress]
        public string Email { get; set; }

        [JsonProperty("car", Required = Required.DisallowNull)]
        public Car? Car { get; set; }

        [JsonProperty("inn_number", Required = Required.DisallowNull)]
        public string Inn { get; set; }

        [JsonProperty("occupation", Required = Required.DisallowNull)]
        public string Occupation { get; set; }

        public static T FromJson<T>(string json) where T : Request => JsonConvert.DeserializeObject<T>(json);

        public static T FromXml<T>(string xml) where T : Request => FromXml<T>(XDocument.Parse(xml));

        public static T FromXml<T>(XObject xml) where T : Request => FromJson<T>(JsonConvert.SerializeXNode(xml, Formatting.None, true));

        public string ToJson()
        {
            var settings =
                new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DateFormatString = "yyyy-MM-dd"
                    };
            return JsonConvert.SerializeObject(this, settings);
        }

        public string ToXml() => JsonConvert.DeserializeXNode(ToJson(), "request").ToString(SaveOptions.DisableFormatting);
    }
}
