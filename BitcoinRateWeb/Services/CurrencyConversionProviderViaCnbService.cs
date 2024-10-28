using BitcoinRateWeb.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace BitcoinRateWeb.Services
{
    public class CurrencyConversionProviderViaCnbService
    {
        private class Rate
        {
            public string ValidFor { get; set; }
            public int Order { get; set; }
            public string Country { get; set; }
            public string Currency { get; set; }
            public int Amount { get; set; }
            public string CurrencyCode { get; set; }

            [JsonProperty("rate")]
            public decimal RateValue { get; set; }
        }

        private class CnbResponse
        {
            public List<Rate> Rates { get; set; }
        }

        private readonly HttpClient _httpClient;

        public CurrencyConversionProviderViaCnbService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ICurrencyConvertor> GetConverterEurToCzk()
        {
            var cnbResponse = await _httpClient.GetStringAsync("https://api.cnb.cz/cnbapi/exrates/daily");
            var ratesData = JsonConvert.DeserializeObject<CnbResponse>(cnbResponse);

            var eurRate = ratesData.Rates.FirstOrDefault(r => r.CurrencyCode == "EUR");

            return new SimpleCurrencyConvertor("EUR", "CZK", eurRate.RateValue);
        }
    }
}