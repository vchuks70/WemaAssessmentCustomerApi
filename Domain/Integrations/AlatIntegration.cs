using Domain.DataTransferObjects.Responses.Alat;
using Domain.Helper;
using Domain.Integrations.Intefaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Integrations
{
    public class AlatIntegration : IAlatIntegration
    {
        public HttpClient _httpClient { get; set; }
        private readonly AppSettings _appSettings;

        public AlatIntegration(IOptions<AppSettings> appSettings)
        {
            _httpClient = new HttpClient();
            _appSettings = appSettings.Value;
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _appSettings.AlatKey);
        }

        public async Task<GetAllBanksResponse> GetAllBanks()
        {
            var url = _appSettings.GetBanksUrl;

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var resultString =  await response.Content.ReadAsStringAsync();
            var deserializedResult = JsonConvert.DeserializeObject<GetAllBanksResponse>(resultString);
            return deserializedResult;
        }
    }
}
