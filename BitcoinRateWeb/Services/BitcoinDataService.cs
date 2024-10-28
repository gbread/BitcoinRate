namespace BitcoinRateWeb.Services
{
    public class BitcoinDataService : IHostedService, IDisposable
    {
        private Timer _timer;

        public event Action<BitcoinService.BitcoinServiceResponse> OnBitcoinDataUpdated;

        private readonly BitcoinService _bitcoinService;
        private readonly ILogger _logger;

        public BitcoinService.BitcoinServiceResponse LastResponse { get; private set; }

        public BitcoinDataService(BitcoinService bitcoinService, ILogger<BitcoinDataService> logger)
        {
            _bitcoinService = bitcoinService;
            this._logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(UpdateBitcoinData, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            return Task.CompletedTask;
        }

        private async void UpdateBitcoinData(object state)
        {
            LastResponse = await _bitcoinService.GetBitcoinPriceInCZKAsync();
            _logger.Log(LogLevel.Debug, "Downloaded bitcoin rates: {LastResponse}", LastResponse);
            OnBitcoinDataUpdated?.Invoke(LastResponse);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() => _timer?.Dispose();
    }
}