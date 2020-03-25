using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Edu.Ntu.Foundation.Core.Constants;

namespace Edu.Ntu.Foundation.Core.Utils
{
    public static class HttpUtil
    {
        public static T Post<T>(IHttpClientFactory factory, string clientName, object body, string servicePath)
        {
            var jsonBody = JsonUtil.SerializeObject(body, DateFormatHandling.MicrosoftDateFormat);

            var client = factory.CreateClient(clientName);
            StringContent content = new StringContent(jsonBody);
            content.Headers.ContentType = new MediaTypeHeaderValue(RequestHeaders.JSON_CONTENT);

            var response = client.PostAsync(servicePath, content).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Get data from TableService failed. URI: "
                        + response.RequestMessage.RequestUri + ", Error Message: " + responseContent);
            }

            return JsonUtil.DeserializeObject<T>(responseContent);
        }
    }
}
