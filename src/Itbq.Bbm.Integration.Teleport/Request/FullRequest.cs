using System;
using System.ComponentModel.DataAnnotations;

using Itbq.Bbm.Integration.Teleport.Converters;
using Newtonsoft.Json;

namespace Itbq.Bbm.Integration.Teleport.Request
{
    public class FullRequest : Request
    {
        [JsonProperty("home_phone", Required = Required.DisallowNull)]
        public string HomePhone { get; set; }

        [JsonProperty("id_sex", Required = Required.DisallowNull)]
        public Sex? Sex { get; set; }

        [JsonProperty("passport_series", Required = Required.DisallowNull)]
        public string PassportSeries { get; set; }

        [JsonProperty("passport_number", Required = Required.DisallowNull)]
        public string PassportNumber { get; set; }

        [JsonProperty("passport_date_of_issue", Required = Required.DisallowNull)]
        [JsonConverter(typeof(DateTimeConverter))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? PassportDateOfIssue { get; set; }

        [JsonProperty("birthplace", Required = Required.DisallowNull)]
        public string Birthplace { get; set; }

        [JsonProperty("passport_org", Required = Required.DisallowNull)]
        public string PassportOrg { get; set; }

        /// <summary>
        /// Format 111-222
        /// </summary>
        [JsonProperty("passport_code", Required = Required.DisallowNull)]
        public string PassportCode { get; set; }

        [JsonProperty("residential_street", Required = Required.DisallowNull)]
        public string ResidentialStreet { get; set; }

        [JsonProperty("residential_house", Required = Required.DisallowNull)]
        public string ResidentialHouse { get; set; }

        [JsonProperty("residential_building", Required = Required.DisallowNull)]
        public string ResidentialBuilding { get; set; }

        [JsonProperty("residential_apartment", Required = Required.DisallowNull)]
        public string ResidentialApartment { get; set; }

        [JsonProperty("registration_street", Required = Required.DisallowNull)]
        public string RegistrationStreet { get; set; }

        [JsonProperty("registration_house", Required = Required.DisallowNull)]
        public string RegistrationHouse { get; set; }

        [JsonProperty("registration_building", Required = Required.DisallowNull)]
        public string RegistrationBuilding { get; set; }

        [JsonProperty("registration_apartment", Required = Required.DisallowNull)]
        public string RegistrationApartment { get; set; }

        [JsonProperty("incoming", Required = Required.DisallowNull)]
        public string Incoming { get; set; }

        [JsonProperty("work_name", Required = Required.DisallowNull)]
        public string WorkName { get; set; }

        [JsonProperty("experience", Required = Required.DisallowNull)]
        public string Experience { get; set; }

        [JsonProperty("work_phone", Required = Required.DisallowNull)]
        public string WorkPhone { get; set; }

        [JsonProperty("boss_phone", Required = Required.DisallowNull)]
        public string BossPhone { get; set; }

        [JsonProperty("work_region", Required = Required.DisallowNull)]
        public string WorkRegion { get; set; }

        [JsonProperty("work_city", Required = Required.DisallowNull)]
        public string WorkCity { get; set; }

        [JsonProperty("work_street", Required = Required.DisallowNull)]
        public string WorkStreet { get; set; }

        [JsonProperty("work_house", Required = Required.DisallowNull)]
        public string WorkHouse { get; set; }

        [JsonProperty("work_building", Required = Required.DisallowNull)]
        public string WorkBuilding { get; set; }

        [JsonProperty("work_apartment", Required = Required.DisallowNull)]
        public string WorkApartment { get; set; }
    }
}
