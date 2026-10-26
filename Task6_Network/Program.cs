using System;
using System.Text;
using System.Threading.Tasks;

namespace Task6_Network
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            WeatherService weatherService = new WeatherService();

            while (true)
            {
                Console.Write("Şəhərin adını daxil edin (Çıxış üçün 'exit'): ");
                string city = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(city)) continue;
                if (city.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

                var weather = await weatherService.GetWeatherAsync(city);
                if (weather != null)
                {
                    Console.WriteLine(weather);
                }
                else
                {
                    Console.WriteLine("Məlumat tapılmadı.\n");
                }
            }
        }
    }
}