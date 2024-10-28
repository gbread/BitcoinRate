using System.ComponentModel.DataAnnotations;

namespace BitcoinRateWeb.Models
{
    public class BitcoinPrice
    {
        [Key]
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }
        public decimal BtcToEur { get; set; }
        public decimal BtcToCzk { get; set; }

        [MaxLength(100)]
        public string Note { get; set; }
    }
}