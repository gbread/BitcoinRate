using BitcoinRateWeb.Models;
using System.Reflection.Metadata.Ecma335;

namespace BitcoinRateWeb.Services
{
    public class CurrencyConversionProviderViaCnbService
    {
        public async Task<ICurrencyConvertor> GetConverterEurToCzk()
        {
            return new SimpleCurrencyConvertor("EUR", "CZK", 24.5M);
        }
    }
}