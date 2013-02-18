#Bing Maps WinRT helpers#
=================

##UriBuilder

Simple helper class to simplify creation of URI scheme for running and manipulating Maps application on Windows 8 (WindowsRT) operating system. You don't need to have Map SDK to show map or location in your application. Leverage power of BingMaps URI protocol using this simple and lightweight helper.

###Usage

```csharp

var builder = new BingMapsUriBuilder();

builder.SetCenterPoint(45, 15).SetZoomLevel(16).ShowTraffic(true);
builder.ShowMap();

```

[Nuget package](https://nuget.org/packages/BingMapsHelpers.UriBuilder/) is also available.
