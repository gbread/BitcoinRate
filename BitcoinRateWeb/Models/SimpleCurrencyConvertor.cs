namespace BitcoinRateWeb.Models
{
    public class SimpleCurrencyConvertor : ICurrencyConvertor
    {
        public string FromCurrencyCode { get; }
        public string ToCurrencyCode { get; }
        public decimal Rate { get; }

        public SimpleCurrencyConvertor(string fromCurrencyCode, string toCurrencyCode, decimal rate)
        {
            FromCurrencyCode = fromCurrencyCode;
            ToCurrencyCode = toCurrencyCode;
            Rate = rate;
        }

        public bool CanConvert(string fromCurrencyCode, string toCurrencyCode)
        {
            if (fromCurrencyCode != FromCurrencyCode) return false;
            if (toCurrencyCode != ToCurrencyCode) return false;

            return true;
        }

        public decimal Convert(decimal value, string fromCurrencyCode, string toCurrencyCode)
        {
            if (!CanConvert(fromCurrencyCode, toCurrencyCode))
            {
                throw new NotSupportedException();
            }

            return value * Rate;
        }
    }
}