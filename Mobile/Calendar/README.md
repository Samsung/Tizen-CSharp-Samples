# Calendar

The Calendar application demonstrates how to implement an calendar service which can add and configure an calendar.
This sample is following Portable Class Libraries (PCL) application model and using some Xamarin.Forms features such as XAML files for GUI, and subsystem ports by using the Dependency Service.

<table>
<tr>
<td><center><img src='cal1.png' height=400></center></td>
<td><center><img src='cal2.png' height=400></center></td>
</tr>
</table>

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project.

### Prerequisites

* [Visual Studio](https://www.visualstudio.com/) - Buildtool, IDE
* [Visual Studio Tools for Tizen](https://developer.tizen.org/development/tizen-.net-preview/visual-studio-tools-tizen) - Visual Studio plugin for Tizen .net app development.
* [StyleCop](https://github.com/StyleCop/StyleCop) - Coding Rule Checker

### Installing

* Build Calendar source codes by the Visual Studio.
* Run Tizen mobile emulator.
* Install build output tpk file to the Tizen mobile emulator.

```
d:\> "C:\Program Files (x86)\Tizen\SDK\tools\sdb.exe" install org.tizen.example.Calendar.Tizen.Mobile-1.0.0.tpk
```

## Running the tests

* Build source codes by Visual Studio
* Visual Studio > Test > Run > All Tests

### And coding style tests

* Visual Studio > Tools > Run StyleCop

## Acknowledgments
