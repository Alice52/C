using Newtonsoft.Json;

namespace Edu.Ntu.Foundation.Core.Utils
{
    public static class JsonUtil
    {
        public static string SerializeObject(object body, JsonSerializerSettings jsonSerializerSettings = null)
        {
            if (jsonSerializerSettings == null)
            {
                return JsonConvert.SerializeObject(body);
            }
            else
            {
                return JsonConvert.SerializeObject(body, jsonSerializerSettings);
            }
        }

        public static string SerializeObject(object body, DateFormatHandling dateFormatHandling)
        {
            return JsonConvert.SerializeObject(body, new JsonSerializerSettings
            {
                DateFormatHandling = dateFormatHandling
            });
        }

        public static T DeserializeObject<T>(string body, JsonSerializerSettings jsonSerializerSettings = null)
        {
            if (jsonSerializerSettings == null)
            {
                return JsonConvert.DeserializeObject<T>(body);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(body, jsonSerializerSettings);
            }
        }

        public static string FormatJsonString(string content, JsonSerializerSettings jsonSerializerSettings)
        {
            var contentObject = DeserializeObject<object>(content);
            return SerializeObject(contentObject, jsonSerializerSettings);
        }
    }
}
