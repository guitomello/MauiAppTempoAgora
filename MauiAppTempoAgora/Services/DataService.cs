using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null;

            string api_key = "94f4790a90a7d3f0cc3584e7619415a0";

            string url = $"https://api.openweathermap.org/data/2.5/weather?q=" + $"{cidade}&units=metric&appid={api_key}";

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    t = new()
                    {
                        cod = (int)response.StatusCode
                    };
                }

                string json = await response.Content.ReadAsStringAsync();

                var rascunho = JObject.Parse(json);

                if (response.IsSuccessStatusCode)
                {
                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                    t = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],
                        temp_max = (double)rascunho["main"]["temp_max"],
                        temp_min = (double)rascunho["main"]["temp_min"],
                        visibility = (int)rascunho["visibility"],
                        sunrise = sunrise.ToString(),
                        sunset = sunset.ToString(),
                        speed = (double)rascunho["wind"]["speed"],
                        cod = (int)response.StatusCode
                    };
                }

                
            }

            return t;
        }
    }
}
