using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Task6_Network
{
    public class WeatherRoot
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("current")]
        public Current Current { get; set; }

        public override string ToString()
        {
            return $@"
            --- Hava Məlumatı ---
            Şəhər: {Location?.Name}, {Location?.Country}
            Temperatur: {Current?.TempC} °C
            Hiss edilən: {Current?.FeelslikeC} °C
            Hava şəraiti: {Current?.Condition?.Text}
            Rütubət: {Current?.Humidity}%
            Küləyin sürəti: {Current?.WindKph} km/saat
            ---------------------
            ";
        }
    }

    public class Location
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
    }

    public class Current
    {
        [JsonProperty("temp_c")]
        public double TempC { get; set; }
        [JsonProperty("feelslike_c")]
        public double FeelslikeC { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
        [JsonProperty("wind_kph")]
        public double WindKph { get; set; }
        [JsonProperty("condition")]
        public Condition Condition { get; set; }
    }

    public class Condition
    {
        [JsonProperty("text")]
        public string? Text { get; set; }
    }

    public class WeatherService
    {
        private readonly string apiKey = "e50dd5c835bb435bbfb205010262610";
        private readonly string url = "https://api.weatherapi.com/v1/current.json";
        private readonly HttpClient httpClient = new HttpClient();

        public async Task<WeatherRoot?> GetWeatherAsync(string city)
        {
            var response = await httpClient.GetAsync($"{url}?key={apiKey}&q={Uri.EscapeDataString(city)}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Status code: {(int)response.StatusCode}");
                return null;
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<WeatherRoot>(jsonString);

            return data;
        }
    }
}