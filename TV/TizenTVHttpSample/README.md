# TizenTVHttpSample

The **TizenTVHttpSample** application shows how to get data with HttpClient and HttpWebRequest.

## [How to test it](#how_to_test)

 - Precondition: Network connectivity should be available to access the Internet.

To get weather information, we use the [OpenWeatherMap](<https://openweathermap.org/>) API.

You should sign up for a `free API key` at [OpenWeatherMap](http://openweathermap.org/appid) and put your api key in the following line of **MainPageModel.cs** file:
```
public const string WEATHER_API_KEY = "PUT YOUR API KEY HERE";
```

If you want to get Seoul weather info, press `Get Weather` button.

![][TV_Http_1]

In case of London,

 ![][TV_Http_3]

If you want to make a Http Web GET request, press `Get Data` button.

 ![][TV_Http_2] 


[TV_Http_1]: ./screenshots/TVHttpSample_1.png
[TV_Http_2]: ./screenshots/TVHttpSample_2.png
[TV_Http_3]: ./screenshots/TVHttpSample_3.png