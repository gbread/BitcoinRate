using BitcoinRateWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BitcoinRateWeb.DAL
{
    public class BitcoinStoredPriceService
    {
        private readonly BitcoinDataContext _context;

        public BitcoinStoredPriceService(BitcoinDataContext context)
        {
            _context = context;
        }

        public async Task<List<BitcoinPrice>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.BitcoinPrices.ToListAsync();
        }

        public async Task<BitcoinPrice> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.BitcoinPrices.FindAsync(id, cancellationToken);
        }

        public async Task AddAsync(BitcoinPrice bitcoinPrice, CancellationToken cancellationToken = default)
        {
            await _context.BitcoinPrices.AddAsync(bitcoinPrice, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddAsync(IEnumerable<BitcoinPrice> bitcoinPrice, CancellationToken cancellationToken = default)
        {
            await _context.BitcoinPrices.AddRangeAsync(bitcoinPrice, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(BitcoinPrice bitcoinPrice, CancellationToken cancellationToken = default)
        {
            _context.BitcoinPrices.Update(bitcoinPrice);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(IEnumerable<BitcoinPrice> bitcoinPrice, CancellationToken cancellationToken = default)
        {
            _context.BitcoinPrices.UpdateRange(bitcoinPrice);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(BitcoinPrice bitcoinPrice, CancellationToken cancellationToken = default)
        {
            _context.BitcoinPrices.Remove(bitcoinPrice);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(IEnumerable<BitcoinPrice> bitcoinPrices, CancellationToken cancellationToken = default)
        {
            _context.BitcoinPrices.RemoveRange(bitcoinPrices);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}