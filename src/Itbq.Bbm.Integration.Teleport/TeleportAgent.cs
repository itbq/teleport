using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Itbq.Bbm.Integration.Teleport.Properties;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace Itbq.Bbm.Integration.Teleport
{
    public static class TeleportAgent
    {
        private const string RequestTemplate = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><requests>{0}</requests>";

        private static string Uid { get; } = Settings.Default.UID;

        private static readonly HttpClient Client = new HttpClient();

        public static async Task<HttpResponseMessage> SendAsync(Request.Request request)
        {
            if (!Validate(request, out IList<string> errors))
            {
                return
                    new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            Content = new StringContent(string.Join("\n", errors))
                        };
            }

            // http://vteleport.ru/outer/SendRequestXml
            // var body = $"uid={Uid}&data={HttpUtility.UrlEncode(string.Format(RequestTemplate, request.ToXml()))}";
            try
            {
                var json =
                    new Dictionary<string, string>
                        {
                            { "uid", Uid },
                            { "data", HttpUtility.UrlEncode(string.Format(RequestTemplate, request.ToXml())) }
                        };
                var content = new FormUrlEncodedContent(json);
                var response = await Client.PostAsync(Settings.Default.TeleportUrl, content).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                return response;

            }
            catch (Exception ex)
            {
                return
                    new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            Content = new StringContent(ex.Message)
                        };
            }
        }

        private static bool Validate(Request.Request request, out IList<string> errors)
        {
            var generator = new JSchemaGenerator();
            //generator.
            var schema = generator.Generate(request.GetType());
            var data = JObject.Parse(request.ToJson());

            return data.IsValid(schema, out errors);
        }
    }
}
