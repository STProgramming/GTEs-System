using GTEs_BE.Datas.Models;
using System.Runtime.CompilerServices;

namespace GTEs_BE.Services
{
    public class StartUpService : IHostedService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StartUpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var status = await client.GetFromJsonAsync<WeConnectBridgeStatus>("http://localhost:5001/weconnect/status");

                if (status?.IsAuthenticated == true)
                {
                    SystemStatusModel.IsUserAuthenticated = true;
                    SystemStatusModel.IsCarLocked = status.IsCarLocked;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[INIT] WeConnect non raggiungibile o non autenticato.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

