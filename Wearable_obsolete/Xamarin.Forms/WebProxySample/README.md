# WebProxySample

The **WebProxySample** application shows how to use WebProxy to access the Internet when Samsung Galaxy Watch has a connection to a mobile phone through Bluetooth.

You can get detail information from the posting [`Access the Internet Using WebProxy when Samsung Galaxy Watch and a phone are connected via Bluetooth`][web_proxy].

## [How to test it](#how_to_test)

 - Precondition: Network connectivity should be available. To test using WebProxy, make sure that you need to connect your watch and phone via Bluetooth.

The MainPage looks like below.

 ![][mainpage]

A Galaxy Watch can communicate with a network through the connected phone but on the other hand, Wi-Fi or cellular networks can used when the paired mobile phone is not available.

If Galaxy watch has a Bluetooth connection to a mobile phone, it can access the Internet after setting WebProxy.

If you want to make a Http Web GET request, press `GetData` button.

| thru a mobile phone | thru Wi-Fi |
| :-: | :-: |
| ![][connected_to_mobilephone] | ![][connected_thru_wifi] |

If you want to download a file, you can use Watch's Wi-Fi network or the connected phone's network connectivity.

Let's check it out by presssing `Download` button.

![][downloading_1] ![][downloading_2]  ![][downloading_3]


[web_proxy]: https://samsung.github.io/Tizen.NET/wearables/web-proxy/
[mainpage]: ./screenshots/webproxy_mainpage.png
[connected_thru_wifi]: ./screenshots/webproxy_httpwebresponse_wifi.png
[connected_to_mobilephone]: ./screenshots/webproxy_httpwebresponse_mobile.png
[downloading_1]: ./screenshots/webproxy_download_a_flie_1.png
[downloading_2]: ./screenshots/webproxy_download_a_flie_2.png
[downloading_3]: ./screenshots/webproxy_download_a_flie_3.png