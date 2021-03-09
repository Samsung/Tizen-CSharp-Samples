# Tizen-CSharp-Samples
Tizen C# Samples for Mobile, Wearable, and TV profiles.

## Tools for samples

### Visual Studio 2019
* [Download Visual Studio 2019](https://www.visualstudio.com/downloads/)

### Visual Studio Tools for Tizen
* [Download Plugin](https://docs.tizen.org/application/vstools/install)

### Reference
* [Tizen .NET Application](https://docs.tizen.org/application/dotnet/index)
* [Tizen .NET API Reference](https://docs.tizen.org/application/dotnet/api/overview)
  - https://samsung.github.io/TizenFX/stable/
* [Tizen CircularUI](https://samsung.github.io/Tizen.CircularUI)
* [Tizen .NET Forum](https://developer.tizen.org/forums/tizen-.net/active)

### Sample structure

Sample apps will be deployed for each app type.

#### AS-IS
```
├── Mobile
│   ├── A
│   ├── B
│   └── C
├── Wearable
│   ├── D
│   ├── E
│   └── F
└── TV
    ├── G
    ├── H
    └── I
```

#### TO-BE
```
├── Mobile
│ ├── NUI
│ │ └── ...
│ ├── OpenTk
│ │ └── ...
│ └── Xamarin.Forms
│ │ └── ...
├── Wearable
│ ├── NUI
│ │ └── ...
│ ├── OpenTK
│ │ └── ...
│ └── Xamarin.Forms
│ │ └── ...
└── TV
  ├── NUI
  │ └── ...
  ├── OpenTK
  │ └── ...
  └── Xamarin.Forms
    └── ...
```

### Branch policy
* master: ready applications for Tizen 5.5
