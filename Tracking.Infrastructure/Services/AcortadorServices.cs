using Tracking.Application.Common.Interface;

namespace Tracking.Infrastructure.Services
{
    public class AcortadorServices : IAcortadorServices
    {
        private readonly HttpClient _httpClient;

        public AcortadorServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AcordarEnlace(string longUrl)
        {
            if (string.IsNullOrWhiteSpace(longUrl))
                throw new ArgumentException("La URL no puede estar vacía.");

            var encodedUrl = Uri.EscapeDataString(longUrl);

            var response = await _httpClient.GetAsync(
                $"https://tinyurl.com/api-create.php?url={encodedUrl}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error al acortar la URL.");

            var shortUrl = await response.Content.ReadAsStringAsync();

            return shortUrl;
        }
    }
}
