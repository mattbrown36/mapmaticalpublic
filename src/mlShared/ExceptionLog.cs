using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mlShared.Models
{
    /// <summary>
    /// Converts exceptions to a json payload for google stackdriver.
    /// </summary>
    public class MlExceptionLog
    {
        public MlExceptionLog(Exception ex)
        {

            var stackCleaned = ex.StackTrace.Replace("\r", "");
            var stackLines = stackCleaned.Split('\n');
            this.stackTrace = stackLines.ToList();

            var timestring = DateTime.UtcNow.ToString("yyyyMMdd|HH:mm:ss - ");
            var location = stackTrace.FirstOrDefault() ?? "No Stack Trace?";

            this.msg = location + " |msg:| " + ex.Message + " |time:| " + timestring;

        }

        [JsonProperty(Order = 0)]
        public string msg { get; set; } = "";

        [JsonProperty(Order = 1)]
        public List<String> stackTrace { get; set; } = new List<string>();

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                NullValueHandling = NullValueHandling.Ignore,
            }).Trim(',') + '\n';
        }
    }
}
