# Selkhound

Selkhound is an open source, community-based chat app combinining the best features of Discord and Matrix.

Our design goals include:

* Open User API, allowing for users to use the default client applicaiton or to roll their own.
* Federated user instances. Use the public Selkhound instance or spin up your own.
  * Docker and Kubernetes support is planned.
* End to End Encryption with perfect forward secrecy.
* Role-based Access Control.
* Extensible through plugin API to allow for third party integrations, such as OAuth.
* Documented and version controlled API using SEMVER.

## Client Compatibility

The below chart tracks the current client compatibility for the official Selkhound client, as well as containing download links to the raw executables.

For Android and iOS, links to the store builds are below the table.


|  Platform   |   Compatibility    |       Min Version        | Recommended Version  |                  Download                  |
|-------------|--------------------|--------------------------|----------------------|--------------------------------------------|
| iOS         | :white_check_mark: | 14.2                     | Latest               |                                            |
| MacCatalyst | :white_check_mark: | 14.0                     | Latest               |                                            |
| Android     | :white_check_mark: | Android 9 (API 28 - Pie) | Android 13 (API 33)  |                                            |
| Windows     | :white_check_mark: | Windows 10.0.17763.0     | Windows 10.0.17763.0 |                                            |
| Linux       | Experimental       |                          |                      |                                            |

## Download Links

Selkhound Server: [Coming Soon](#)

Links to the F-Droid, Google Play, and Apple App Stores will be placed here once we reach version 1.0.0.
If you would like to install the beta client, please use the download links located in the table above.

## Project Layout

- **Selkhound.API.Abstractions** holds common gRPC definitions for the server and client.
- **Selkhound.Client** is the primary client application for iOS, MacOS, Android, and Windows, with experimental support for Linux using GTK#. This is a [MAUI Blazor](https://learn.microsoft.com/en-us/shows/xamarinshow/introduction-to-net-maui-blazor--the-xamarin-show) application which shares content with the web client.
- **Selkhound.Client.Web** is the web portal. This is a Blazor WASM app, which means it is **not compatible with IE11**. Please use a modern browser.
- **Selkhound.Client.Shared** is a class library which holds all Blazor Areas, Views, View Models, and other web components such as stylesheets and Javascript components. This provides a unified back-end for both the client and web portal.
- **Selkhound.Server** is a headless gRPC API which represents the entirety of the server back-end. This can be run stand-alone or clustered, such as behind a load balancer.
