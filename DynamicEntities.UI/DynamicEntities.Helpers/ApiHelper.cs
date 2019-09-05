using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEntities.Helpers
{
    public class ApiHelper
    {
        public static async Task<T> GetData<T>(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var responseReceived = await response.Content.ReadAsStringAsync();
                    var list = JsonConvert.DeserializeObject<T>(responseReceived);
                    return list;
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public static async Task<T> PostData<T>(string url, object model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var myContent = JsonConvert.SerializeObject(model);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await client.PostAsync(url, byteContent);
                    var responseReceived = await response.Content.ReadAsStringAsync();
                    var list = JsonConvert.DeserializeObject<T>(responseReceived);
                    return list;
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
