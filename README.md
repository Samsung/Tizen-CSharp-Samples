# Tizen-CSharp-Samples
Tizen C# Samples for Mobile, Wearable, and TV profiles.

## Precondition
* Visual Studio 2017 : [Download](https://www.visualstudio.com/downloads/)
* Visual Studio Tools for Tizen : [Download](https://developer.tizen.org/development/visual-studio-tools-tizen/installing-visual-studio-tools-tizen)

### Reference
* [Tizen .NET](https://developer.tizen.org/development/api-reference/.net-application)
* [TizenFX API Reference](https://developer.tizen.org/dev-guide/csapi/index.html)

## Samples Submission Guidelines

### Branch policy
* master: ready applications for Tizen 4.0 M2
* dev: for development
* use LF line endings. For more infomation about automatic conversion from CRLF to LF see [Tip section](#tip)

### Sample Requirements
* Screenshots - A folder where screen shots for the sample application are located. It is better to take screenshots for all pages and main features of the sample application (at least one screen shot of the sample). When the sample is a CrossPlatform application, you should have the another folder which is named with the platform, for example, iOS, Android, Windows or Tizen. [See here](https://github.com/Samsung/Tizen-CSharp-Samples/tree/dev/TV/Gallery/Screenshots/) for an example.
 
* README - Every sample application should have README.md file which has the name of the sample, a description, version and the supporting profile, and author. See [README.md](https://github.com/Samsung/Tizen-CSharp-Samples/blob/dev/TV/Gallery/README.md) for an example


### Sample structure
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


## Tip
 * How to troubleshoot changes between CRLF and LF ?
   * Apply the [core.autocrlf](https://help.github.com/articles/dealing-with-line-endings/) configuration through the Git command.
    
     * (Window) : git config --global core.autocrlf true
     * (Linux) : git config --global core.autocrlf input
