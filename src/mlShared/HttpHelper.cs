using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace mlShared
{
    public class HttpHelper
    {
        public HttpClient Client { get; } = new HttpClient();

        public HttpHelper(string baseUri) {
            Client.BaseAddress = new Uri(Util.GetSetting(baseUri));
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        JsonSerializerSettings defaultJsonSettings = new JsonSerializerSettings() {
            Formatting = Formatting.Indented,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        };

        public async Task<HttpResponseMessage> PostAsync<T>(string controller, T contents) {
            var asJson = Newtonsoft.Json.JsonConvert.SerializeObject(contents, defaultJsonSettings);
            return await Client.PostAsync(controller, new StringContent(asJson, Encoding.UTF8, "application/json"));
        }

        public async Task<TReturn> GetAsync<TReturn>(string controller, params string[] queryArgs) {
            HttpResponseMessage result;
            if (queryArgs == null || queryArgs.Length < 1)
            {
                result = await this.Client.GetAsync(controller);
            }
            else {
                if (queryArgs.Length % 2 != 0) {
                    throw new Exception("Alternating query arguments did not have a value for each key. ie count, 7, order, ascending");
                }
                var queryString = controller + "?";
                var encoder = System.Text.Encodings.Web.HtmlEncoder.Default;
                for (int i = 0; i < queryArgs.Length; i++) {
                    if (i % 2 == 0)
                    {
                        queryString += encoder.Encode(queryArgs[i]) + "=";
                    }
                    else {
                        queryString += encoder.Encode(queryArgs[i]) + "&";
                    }
                }
                queryString = queryString.TrimEnd('&');
                result = await this.Client.GetAsync(queryString);
            }
            if (!result.IsSuccessStatusCode) {
                throw new Exception("HTTP Get failed: " + result.StatusCode + result.ReasonPhrase ?? "");
            }
            var asJson = await result.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TReturn>(asJson);
        }

    }
}
