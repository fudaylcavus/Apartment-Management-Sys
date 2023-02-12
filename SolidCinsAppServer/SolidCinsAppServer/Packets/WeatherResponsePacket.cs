public class WeatherResponsePacket
{
    public CurrentWeather current_weather { get; set; }
}


public class CurrentWeather
{
    public float temperature { get; set; }
    public float windspeed { get; set; }
    public float winddirection { get; set; }
    public int weathercode { get; set; }
    public string time { get; set; }

}
