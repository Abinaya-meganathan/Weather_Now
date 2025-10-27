namespace Weather_Now.Models
{
    public class WeatherNowModel
    {
        public string? City { get; set; }
        public double Temperature { get; set; }
        public double WindSpeed { get; set; }
        public string? WeatherCode { get; set; }
        public string? WeatherDescription { get; set; }
    }
}
