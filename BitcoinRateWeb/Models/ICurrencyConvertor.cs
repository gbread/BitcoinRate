namespace BitcoinRateWeb.Models
{
    public interface ICurrencyConvertor
    {
        public decimal Convert(decimal value, string fromCurrencyCode, string toCurrencyCode);

        public bool CanConvert(string fromCurrencyCode, string toCurrencyCode);
    }
}