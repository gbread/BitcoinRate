using BitcoinRateWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BitcoinRateWeb.DAL
{
    public class BitcoinDataContext : DbContext
    {
        public BitcoinDataContext(DbContextOptions<BitcoinDataContext> options) : base(options)
        {
        }

        public DbSet<BitcoinPrice> BitcoinPrices { get; set; }
    }
}