using Newtonsoft.Json;

namespace BitcoinRateWeb.Services
{
    public class BitcoinService
    {
        private class Time
        {
            public string updated { get; set; }
            public string updatedISO { get; set; }
            public string updateduk { get; set; }
        }

        private class Currency
        {
            public string code { get; set; }
            public string symbol { get; set; }
            public string rate { get; set; }
            public string description { get; set; }
            public float rate_float { get; set; }
        }

        private class Bpi
        {
            public Currency USD { get; set; }
            public Currency GBP { get; set; }
            public Currency EUR { get; set; }
        }

        private class CoindeskApiResponse
        {
            public Time time { get; set; }
            public string disclaimer { get; set; }
            public string chartName { get; set; }
            public Bpi bpi { get; set; }
        }

        public record BitcoinServiceResponse
        {
            public DateTime Timestamp { get; set; }
            public decimal BtcToEur { get; set; }
            public decimal BtcToCzk { get; set; }
        }

        private readonly HttpClient _httpClient;
        private readonly CurrencyConversionProviderViaCnbService _cnbConverterProviderService;

        public BitcoinService(HttpClient httpClient, CurrencyConversionProviderViaCnbService cnbConverterProviderService)
        {
            _httpClient = httpClient;
            _cnbConverterProviderService = cnbConverterProviderService;
        }

        public async Task<BitcoinServiceResponse> GetBitcoinPriceInCZKAsync()
        {
            // Fetch the BTC/EUR price from Coindesk
            var coindeskResponse = await _httpClient.GetStringAsync("https://api.coindesk.com/v1/bpi/currentprice.json");
            var bitcoinData = JsonConvert.DeserializeObject<CoindeskApiResponse>(coindeskResponse);

            // Get the BTC to EUR price
            var btcToEur = (decimal)bitcoinData.bpi.EUR.rate_float;

            var eurToCzkConverter = await _cnbConverterProviderService.GetConverterEurToCzk();
            var btcToCzk = eurToCzkConverter.Convert(btcToEur, "EUR", "CZK");

            return new BitcoinServiceResponse
            {
                Timestamp = DateTime.UtcNow,
                BtcToEur = btcToEur,
                BtcToCzk = btcToCzk,
            };
        }
    }
}