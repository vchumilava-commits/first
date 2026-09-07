using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Tests1.Helpers
{
    public class NominatimClient
    {
        private readonly HttpClient client;
        public NominatimClient()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("https://nominatim.openstreetmap.org/")
            };

            client.DefaultRequestHeaders.UserAgent.ParseAdd("Tests1");
        }

        public async Task<string> GetCountryCodeAsync(double lat, double lng)
        {
            // перевод в формат с точкой вместо запятой в качестве разделителя (без этого тест падает) и подстановка в урл
            var url = string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "reverse?lat={0}&lon={1}&format=json",
                lat,
                lng);
            var json = await client.GetStringAsync(url);

            // переходим country_code и передаем значение
            return JsonDocument.Parse(json)
                .RootElement
                .GetProperty("address")
                .GetProperty("country_code")
                .GetString();
        }
    }
}
