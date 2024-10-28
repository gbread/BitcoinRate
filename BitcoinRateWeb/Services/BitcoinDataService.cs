using BitcoinRateWeb.Settings;
using Microsoft.Extensions.Options;

namespace BitcoinRateWeb.Services
{
    public class BitcoinDataService : IHostedService, IDisposable
    {
        private Timer _timer;

        public event Action<BitcoinService.BitcoinServiceResponse> OnBitcoinDataUpdated;

        private readonly BitcoinService _bitcoinService;
        private readonly ILogger _logger;
        private readonly BitcoinSettings _settings;

        public BitcoinService.BitcoinServiceResponse LastResponse { get; private set; }

        public BitcoinDataService(BitcoinService bitcoinService, ILogger<BitcoinDataService> logger, IOptions<BitcoinSettings> settings)
        {
            _bitcoinService = bitcoinService;
            _logger = logger;
            _settings = settings.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(UpdateBitcoinData, null, TimeSpan.Zero, TimeSpan.FromSeconds(_settings.UpdateIntervalSeconds));
            return Task.CompletedTask;
        }

        private async void UpdateBitcoinData(object state)
        {
            try
            {
                LastResponse = await _bitcoinService.GetBitcoinPriceInCZKAsync();
                _logger.Log(LogLevel.Debug, "Downloaded bitcoin rates: {LastResponse}", LastResponse);
                OnBitcoinDataUpdated?.Invoke(LastResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "Error in bitcoin rates download: No data downloaded");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() => _timer?.Dispose();
    }
}