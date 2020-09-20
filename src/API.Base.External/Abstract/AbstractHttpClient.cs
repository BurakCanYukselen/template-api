using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API.Base.Core.Extensions;
using API.Base.External.Models;
using Newtonsoft.Json;

namespace API.Base.External.Abstract
{
    public abstract class AbstractHttpClient : HttpClient
    {
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings()
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
        };

        protected virtual Encoding Encoding => Encoding.UTF8;

        public AbstractHttpClient(string url)
        {
            this.BaseAddress = new Uri(url);
        }

        public async Task<ExternalServiceResponse<TData>> GetAsync<TData>(string endpoint)
        {
            var result = new ExternalServiceResponse<TData>();
            var response = await base.GetAsync(endpoint);
            
            result.IsSuccess = response.IsSuccessStatusCode;
            if (!response.IsSuccessStatusCode)
                return result;

            return await GetObjectFromHttpMessage(result, response);
        }

        public async Task<ExternalServiceResponse<TData>> PutAsync<TData>(string endpoint, TData payload)
        {
            var result = new ExternalServiceResponse<TData>();
            var stringContent = GetJsonSerializedStringContent(payload);
            var response = await base.PutAsync(endpoint, stringContent);

            result.IsSuccess = response.IsSuccessStatusCode;
            if (!response.IsSuccessStatusCode)
                return result;
            
            return await GetObjectFromHttpMessage(result, response);
        }

        public async Task<ExternalServiceResponse<TData>> PostAsync<TData>(string endpoint, TData payload)
        {
            var result = new ExternalServiceResponse<TData>();
            var stringContent = GetJsonSerializedStringContent(payload);
            var response = await base.PostAsync(endpoint, stringContent);

            result.IsSuccess = response.IsSuccessStatusCode;
            if (!response.IsSuccessStatusCode)
                return result;
            
            return await GetObjectFromHttpMessage(result, response);
        }

        public async Task<ExternalServiceResponse<TData>> DeleteAsync<TData>(string endpoint)
        {
            var result = new ExternalServiceResponse<TData>();
            var response = await base.DeleteAsync(endpoint);

            result.IsSuccess = response.IsSuccessStatusCode;
            if (!response.IsSuccessStatusCode)
                return result;
            
            return await GetObjectFromHttpMessage(result, response);
        }

        private async Task<ExternalServiceResponse<TData>> GetObjectFromHttpMessage<TData>(ExternalServiceResponse<TData> result, HttpResponseMessage response)
        {
            var responseContent = await response.RequestMessage.Content.ReadAsStringAsync();
            result.Data = responseContent.FromJson<TData>();
            return result;
        }
        
        private StringContent GetJsonSerializedStringContent<TData>(TData data)
        {
            var serializedData = data.ToJson(this._serializerSettings);
            var content = new StringContent(serializedData, this.Encoding, "application/json");
            return content;
        }
    }
}