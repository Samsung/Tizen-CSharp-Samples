# CustomFonts

The **CustomFonts** application shows how to use custom fonts instead of system fonts.

## Xamarin.Forms application
 This sample is a fork of [WorkingWithFonts in Xamarin.Forms samples repository][xamarin.samples].

 It's been modified a little bit modified so that it can be seen on Tizen Wearables.

1. You can add a font file (.ttf or .otf files) you want to use to your res directory and specify the global font path as follows:

```c#
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
             // To use a custom font, you need to add app's res directory to global font path.
            ElmSharp.Utility.AppendGlobalFontPath(this.DirectoryInfo.Resource);
            LoadApplication(new App());
        }
    }
```

2. And then, you can change font family of component such as Label, Button, etc.

```c#
//------------------------------------
//  C#
//------------------------------------
    var label = new Label
    {
        Text = "Hello, Xamarin.Forms!",
        FontFamily = Device.RuntimePlatform == Device.iOS ? "Lobster-Regular" :
                    Device.RuntimePlatform == Device.Tizen ? "Lobster" :
                    Device.RuntimePlatform == Device.Android ? "Lobster-Regular.ttf#Lobster-Regular" : "Assets/Fonts/Lobster-Regular.ttf#Lobster",
        VerticalOptions = LayoutOptions.CenterAndExpand,
        HorizontalOptions = LayoutOptions.CenterAndExpand,

    };
```

```c#
//------------------------------------
//  XAML
//------------------------------------
    <Label Text="Hello Forms with XAML" FontSize="7">
        <Label.FontFamily>
            <OnPlatform x:TypeArguments="x:String">
                <On Platform="iOS" Value="MarkerFelt-Thin" />
                <On Platform="Tizen" Value="Lobster" />
                <On Platform="Android" Value="Lobster-Regular.ttf#Lobster-Regular" />
                <On Platform="UWP" Value="Assets/Fonts/Lobster-Regular.ttf#Lobster" />
            </OnPlatform>
        </Label.FontFamily>
    </Label>
```


 ![][xamarin_forms_font]


## NUI application
You can use custom fonts for NUI applications on Tizen 5.0 and above.

This sample can be run on Tizen 5.0 and above.

If you try to install it on the previous version like Tizen 4.0 wearable emulator, you get the following error during installation:
 > processing result : Operation not allowed [-4] failed

- Specify font directory
```c#
    FontClient.Instance.AddCustomFontDirectory(this.DirectoryInfo.Resource);
```

- Set Font Family of TextLable in NUI Base Components.

```c#
    TextLabel text2 = new TextLabel("Apply a custom font to this label!");
    text2.FontFamily = "Lobster";
```
 ![][nui_font]

[xamarin_forms_font]: ./screenshots/tizen4.0-wearable-xamarin-custom-font-app.png
[nui_font]: ./screenshots/tizen5.0-wearable-nui-custom-font-app.png
[xamarin.samples]: https://github.com/xamarin/xamarin-forms-samples/tree/master/WorkingWithFonts