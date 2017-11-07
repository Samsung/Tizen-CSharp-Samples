/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SystemInfo
{
    /// <summary>
    /// a navigation page class description
    /// </summary>
    public class MainPage : NavigationPage
    {
        class Globals
        {
            public static string LogTag = "System-info";
        }

        /// <summary>
        /// The constructor of MainPage class
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        private bool _contentLoaded;
        private MainPage _myinstance;
        /// <summary>
        /// Init function of MainPage
        /// </summary>
        void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }

            _contentLoaded = true;
            _myinstance = this;
            #region UI
            IsVisible = true;
            BackgroundColor = new Color(200, 0, 0);
            #endregion
        }

    }

    /// <summary>
    /// a class about keyboard info
    /// </summary>
    public class KeyboardPage : ContentPage
    {
        /// <summary>
        /// a construct method for KeyboardPage
        /// </summary>
        /// <param name="title">title</param>
        public KeyboardPage(string title)
        {
            var SourceList = new List<Item>();
            SourceList.Add(new Item("Keyboard", Util.IsSupport("http://tizen.org/feature/input.keyboard")));
            SourceList.Add(new Item("Keyboard Layout", Util.IsSupport("http://tizen.org/feature/input.keyboard.layout")));

            /// <summary>
            /// Init ItermListView of KeyboardPage
            /// </summary>
            ListView ItemListView = new ListView()
            {
                ItemsSource = SourceList,
                ItemTemplate = MyItemTemplate.GetInstance(),
            };

            /// <summary>
            /// Declare Title label of KeyboardPage
            /// </summary>
            Label tt = new Label();
            tt.Text = title;
            tt.FontSize = 28;
            tt.TextColor = Color.White;
            tt.HeightRequest = 0.075 * App.screenHeight;
            tt.HorizontalTextAlignment = TextAlignment.Center;

            /// <summary>
            /// Init Content of KeyboardPage
            /// </summary>
            Content = new StackLayout()
            {
                Children =
                {
                    tt,
                    ItemListView,
                }
            };
        }
    }

    /// <summary>
    /// a class about Location info
    /// </summary>
    public class LocationPage : ContentPage
    {
        /// <summary>
        /// a construct method for LocationPage
        /// </summary>
        /// <param name="title">title</param>
        public LocationPage(string title)
        {
            var SourceList = new List<Item>();
            SourceList.Add(new Item("Location Positioning", Util.IsSupport("http://tizen.org/feature/location")));
            SourceList.Add(new Item("GPS", Util.IsSupport("http://tizen.org/feature/location.gps")));
            SourceList.Add(new Item("WPS", Util.IsSupport("http://tizen.org/feature/location.wps")));

            ListView ItemListView = new ListView()
            {
                ItemsSource = SourceList,
                ItemTemplate = MyItemTemplate.GetInstance(),
            };
            /// <summary>
            /// Declare Title label of LocationPage
            /// </summary>
            Label tt = new Label();
            tt.Text = title;
            tt.FontSize = 28;
            tt.TextColor = Color.White;
            tt.HeightRequest = 0.075 * App.screenHeight;
            tt.HorizontalTextAlignment = TextAlignment.Center;
            /// <summary>
            /// Init Content of LocationPage
            /// </summary>
            Content = new StackLayout()
            {
                Children =
                        {
                    tt,
                    ItemListView,
                        }

            };

        }
    }

    /// <summary>
    /// a class about Network info
    /// </summary>
    public class NetworkPage : ContentPage
    {
        /// <summary>
        /// a construct method for NetworkPage
        /// </summary>
        /// <param name="title">title</param>
        public NetworkPage(string title)
        {
            var GroupList = new List<Object>
            {
                new Group("Bluetooth")
                {
                    new Item("Bluetooth", Util.IsSupport("http://tizen.org/feature/network.bluetooth")),
                    new Item("Handsfree Profile", Util.IsSupport("http://tizen.org/feature/network.bluetooth.audio.call")),
                    new Item("Audio Distribute Profile", Util.IsSupport("http://tizen.org/feature/network.bluetooth.audio.media")),
                    new Item("Health Device Profile", Util.IsSupport("http://tizen.org/feature/network.bluetooth.health")),
                    new Item("Human Input Device", Util.IsSupport("http://tizen.org/feature/network.bluetooth.hid")),
                    new Item("Low Energy", Util.IsSupport("http://tizen.org/feature/network.bluetooth.le")),
                    new Item("Object Push Profile", Util.IsSupport("http://tizen.org/feature/network.bluetooth.opp")),
                },
                new Group("NFC")
                {
                    new Item("NFC", Util.IsSupport("http://tizen.org/feature/network.nfc")),
                    new Item("Card Readers", Util.IsSupport("http://tizen.org/feature/network.nfc.card_emulation")),
                    new Item("Reserved Push", Util.IsSupport("http://tizen.org/feature/network.nfc.reserved_push")),
                },
                new Group("PUSH")
                {
                    new Item("IP Push Service", Util.IsSupport("http://tizen.org/feature/network.push")),
                },
                new Group("Secure")
                {
                    new Item("Secure Elements", Util.IsSupport("http://tizen.org/feature/network.secure_element")),
                    new Item("eSe Secure Elements", Util.IsSupport("http://tizen.org/feature/network.secure_element.ese")),
                    new Item("UICC Secure Elements", Util.IsSupport("http://tizen.org/feature/network.secure_element.uicc")),
                },
                new Group("Telephony")
                {
                    new Item("Telephony", Util.IsSupport("http://tizen.org/feature/network.telephony")),
                    new Item("MMS", Util.IsSupport("http://tizen.org/feature/network.telephony.mms")),
                },
                new Group("Tethering")
                {
                    new Item("Tethering", Util.IsSupport("http://tizen.org/feature/network.tethering")),
                    new Item("Bluetooth Tethering", Util.IsSupport("http://tizen.org/feature/network.tethering.bluetooth")),
                    new Item("USB Tethering", Util.IsSupport("http://tizen.org/feature/network.tethering.usb")),
                    new Item("Wi-Fi Tethering", Util.IsSupport("http://tizen.org/feature/network.tethering.wifi")),
                },
                new Group("Wi-Fi")
                {
                    new Item("Wi-Fi", Util.IsSupport("http://tizen.org/feature/network.wifi")),
                    new Item("Wi-Fi Direct", Util.IsSupport("http://tizen.org/feature/network.wifi.direct")),
                    new Item("Wi-Fi Direct Display", Util.IsSupport("http://tizen.org/feature/network.wifi.direct.display")),
                    new Item("Wi-Fi Direct Service Discovery", Util.IsSupport("http://tizen.org/feature/network.wifi.direct.service_discovery")),
                },
            };
            ListView ItemListView = new ListView()
            {
                ItemsSource = GroupList,
                IsGroupingEnabled = true,
                ItemTemplate = MyItemTemplate.GetInstance(),
            };
            Label tt = new Label();
            tt.Text = title;
            tt.FontSize = 28;
            tt.TextColor = Color.White;
            tt.HeightRequest = 0.075 * App.screenHeight;
            tt.HorizontalTextAlignment = TextAlignment.Center;
            Content = new StackLayout()
            {
                Children =
                        {
                    tt,
                    ItemListView,
                        }

            };
        }
    }
    /// <summary>
    /// a class about OpenGL info
    /// </summary>
    public class OpenGLPage : ContentPage
    {
        /// <summary>
        /// a construct method for OpenGLPage
        /// </summary>
        /// <param name="title">title</param>
        public OpenGLPage(string title)
        {
            var GroupList = new List<Object>
            {
                new Item("OpenGL ES", Util.IsSupport("http://tizen.org/feature/opengles")),
                new Group("Format")
                {
                    new Item("3DC Texture Format", Util.IsSupport("http://tizen.org/feature/opengles.texture_format.3dc")),
                    new Item("ATC Texture Format", Util.IsSupport("http://tizen.org/feature/opengles.texture_format.atc")),
                    new Item("ETC Texture Format", Util.IsSupport("http://tizen.org/feature/opengles.texture_format.etc")),
                    new Item("PTC Texture Format", Util.IsSupport("http://tizen.org/feature/opengles.texture_format.ptc")),
                    new Item("PVRTC Texture Format", Util.IsSupport("http://tizen.org/feature/opengles.texture_format.pvrtc")),
                    new Item("UTC Texture Format", Util.IsSupport("http://tizen.org/feature/opengles.texture_format.utc")),
                },
                new Group("Version")
                {
                    new Item("OpenGL v.1.1", Util.IsSupport("http://tizen.org/feature/opengles.version.1_1")),
                    new Item("OpenGL v.2.0", Util.IsSupport("http://tizen.org/feature/opengles.version.2_0")),
                },

            };
            ListView ItemListView = new ListView()
            {
                ItemsSource = GroupList,
                IsGroupingEnabled = true,
                ItemTemplate = MyItemTemplate.GetInstance(),
            };

            Label tt = new Label();
            tt.Text = title;
            tt.FontSize = 28;
            tt.TextColor = Color.White;
            tt.HeightRequest = 0.075 * App.screenHeight;
            tt.HorizontalTextAlignment = TextAlignment.Center;
            Content = new StackLayout()
            {
                Children =
                        {
                    tt,
                    ItemListView,
                        }

            };
        }
    }

    /// <summary>
    /// a class about Platform info
    /// </summary>
    public class PlatformPage : ContentPage
    {
        /// <summary>
        /// a construct method for PlatformPage
        /// </summary>
        /// <param name="title">title</param>
        public PlatformPage(string title)
        {
            var GroupList = new List<Object>
            {
                new Group("CPU")
                {
                    new Item("CPU ARMv6", Util.IsSupport("http://tizen.org/feature/platform.core.cpu.arch.armv6")),
                    new Item("CPU ARMv7", Util.IsSupport("http://tizen.org/feature/platform.core.cpu.arch.armv7")),
                    new Item("CPU x86", Util.IsSupport("http://tizen.org/feature/platform.core.cpu.arch.x86")),
                },
                new Group("FPU")
                {
                    new Item("FPU SSE2", Util.IsSupport("http://tizen.org/feature/platform.core.fpu.arch.sse2")),
                    new Item("FPU SSE3", Util.IsSupport("http://tizen.org/feature/platform.core.fpu.arch.sse3")),
                    new Item("FPU SSSE3", Util.IsSupport("http://tizen.org/feature/platform.core.fpu.arch.ssse3")),
                    new Item("FPU VFPV2", Util.IsSupport("http://tizen.org/feature/platform.core.fpu.arch.vfpv2")),
                    new Item("FPU VFPV3", Util.IsSupport("http://tizen.org/feature/platform.core.fpu.arch.vfpv3")),
                },
                new Group("Version")
                {
                    new Item("Native API Version", Util.IsSupport("http://tizen.org/feature/platform.native.api.version")),
                    new Item("Platform Version", Util.IsSupport("http://tizen.org/feature/platform.version")),
                    new Item("Web API Version", Util.IsSupport("http://tizen.org/feature/platform.web.api.version")),
                },
            };
            ListView ItemListView = new ListView()
            {
                ItemsSource = GroupList,
                IsGroupingEnabled = true,
                ItemTemplate = MyItemTemplate.GetInstance(),
            };
            Label tt = new Label();
            tt.Text = title;
            tt.FontSize = 28;
            tt.TextColor = Color.White;
            tt.HeightRequest = 0.075 * App.screenHeight;
            tt.HorizontalTextAlignment = TextAlignment.Center;
            Content = new StackLayout()
            {
                Children =
                        {
                    tt,
                    ItemListView,
                        }

            };
        }
    }

    /// <summary>
    /// a class about Screen info
    /// </summary>
    public class ScreenPage : ContentPage
    {
        /// <summary>
        /// a construct method for ScreenPage
        /// </summary>
        /// <param name="title">title</param>
        public ScreenPage(string title)
        {
            var SourceList = new List<Item>();
            SourceList.Add(new Item("Bits per Pixel", Util.IsSupport("http://tizen.org/feature/screen.bpp")));
            SourceList.Add(new Item("Dot per Inch", Util.IsSupport("http://tizen.org/feature/screen.dpi")));
            SourceList.Add(new Item("Height", Util.IsSupport("http://tizen.org/feature/screen.height")));
            SourceList.Add(new Item("Width", Util.IsSupport("http://tizen.org/feature/screen.width")));
            SourceList.Add(new Item("Auto Rotation", Util.IsSupport("http://tizen.org/feature/screen.auto_rotation")));
            SourceList.Add(new Item("Large Screen for coordinate", Util.IsSupport("http://tizen.org/feature/screen.coordinate_system.size.large")));
            SourceList.Add(new Item("Normal Screen for coordinate", Util.IsSupport("http://tizen.org/feature/screen.coordinate_system.size.normal")));
            SourceList.Add(new Item("HDMI", Util.IsSupport("http://tizen.org/feature/screen.output.hdmi")));
            SourceList.Add(new Item("RCA", Util.IsSupport("http://tizen.org/feature/screen.output.rca")));
            SourceList.Add(new Item("Large Screen Size", Util.IsSupport("http://tizen.org/feature/screen.size.large")));
            SourceList.Add(new Item("Normal Screen Size", Util.IsSupport("http://tizen.org/feature/screen.size.normal")));
            SourceList.Add(new Item("All Resolution", Util.IsSupport("http://tizen.org/feature/screen.size.all")));

            ListView ItemListView = new ListView()
            {
                ItemsSource = SourceList,
                ItemTemplate = MyItemTemplate.GetInstance(),
            };
            Label tt = new Label();
            tt.Text = title;
            tt.FontSize = 28;
            tt.TextColor = Color.White;
            tt.HeightRequest = 0.075 * App.screenHeight;
            tt.HorizontalTextAlignment = TextAlignment.Center;
            Content = new StackLayout()
            {
                Children =
                        {
                    tt,
                    ItemListView,
                        }

            };
        }
    }
    /// <summary>
    /// a class about Sensors info
    /// </summary>
    public class SensorsPage : ContentPage
    {
        /// <summary>
        /// a construct method for SensorsPage
        /// </summary>
        /// <param name="title">title</param>
        public SensorsPage(string title)
        {
            var SourceList = new List<Item>();
            SourceList.Add(new Item("Accelerometer", Util.IsSupport("http://tizen.org/feature/sensor.accelerometer")));
            SourceList.Add(new Item("Activity Recognition", Util.IsSupport("http://tizen.org/feature/sensor.activity_recognition")));
            SourceList.Add(new Item("Barometer", Util.IsSupport("http://tizen.org/feature/sensor.barometer")));
            SourceList.Add(new Item("Gesture Recognition", Util.IsSupport("http://tizen.org/feature/sensor.gesture_recognition")));
            SourceList.Add(new Item("Gravity", Util.IsSupport("http://tizen.org/feature/sensor.gravity")));
            SourceList.Add(new Item("Gyroscope", Util.IsSupport("http://tizen.org/feature/sensor.gyroscope")));
            SourceList.Add(new Item("Heart Rate Monitor", Util.IsSupport("http://tizen.org/feature/sensor.heart_rate_monitor")));
            SourceList.Add(new Item("Humidity", Util.IsSupport("http://tizen.org/feature/sensor.humidity")));
            SourceList.Add(new Item("Linear Acceleration", Util.IsSupport("http://tizen.org/feature/sensor.linear_acceleration")));
            SourceList.Add(new Item("Magnetometer", Util.IsSupport("http://tizen.org/feature/sensor.magnetometer")));
            SourceList.Add(new Item("Pedometer", Util.IsSupport("http://tizen.org/feature/sensor.pedometer")));
            SourceList.Add(new Item("Photometer", Util.IsSupport("http://tizen.org/feature/sensor.photometer")));
            SourceList.Add(new Item("Proximity", Util.IsSupport("http://tizen.org/feature/sensor.proximity")));
            SourceList.Add(new Item("Rotation Vector", Util.IsSupport("http://tizen.org/feature/sensor.rotation_vector")));
            SourceList.Add(new Item("Temperature", Util.IsSupport("http://tizen.org/feature/sensor.temperature")));
            SourceList.Add(new Item("Tiltmeter", Util.IsSupport("http://tizen.org/feature/sensor.tiltmeter")));
            SourceList.Add(new Item("Ultraviolet", Util.IsSupport("http://tizen.org/feature/sensor.ultraviolet")));
            SourceList.Add(new Item("Wrist Up Action", Util.IsSupport("http://tizen.org/feature/sensor.wrist_up")));

            ListView ItemListView = new ListView()
            {
                ItemsSource = SourceList,
                ItemTemplate = MyItemTemplate.GetInstance(),
            };
            Label tt = new Label();
            tt.Text = title;
            tt.FontSize = 28;
            tt.TextColor = Color.White;
            tt.HeightRequest = 0.075 * App.screenHeight;
            tt.HorizontalTextAlignment = TextAlignment.Center;
            Content = new StackLayout()
            {
                Children =
                        {
                    tt,
                    ItemListView,
                        }

            };
        }
    }
    /// <summary>
    /// a class about Speech info
    /// </summary>
    public class SpeechPage : ContentPage
    {
        /// <summary>
        /// a construct method for SpeechPage
        /// </summary>
        /// <param name="title">title</param>
        public SpeechPage(string title)
        {
            var SourceList = new List<Item>();
            SourceList.Add(new Item("STT", Util.IsSupport("http://tizen.org/feature/speech.recognition")));
            SourceList.Add(new Item("TTS", Util.IsSupport("http://tizen.org/feature/speech.synthesis")));

            ListView ItemListView = new ListView()
            {
                ItemsSource = SourceList,
                ItemTemplate = MyItemTemplate.GetInstance(),
            };
            Label tt = new Label();
            tt.Text = title;
            tt.FontSize = 28;
            tt.TextColor = Color.White;
            tt.HeightRequest = 0.075 * App.screenHeight;
            tt.HorizontalTextAlignment = TextAlignment.Center;
            Content = new StackLayout()
            {
                Children =
                        {
                    tt,
                    ItemListView,
                        }

            };
        }
    }
    /// <summary>
    /// a class about Usb info
    /// </summary>
    public class UsbPage : ContentPage
    {
        /// <summary>
        /// a construct method for UsbPage
        /// </summary>
        /// <param name="title">title</param>
        public UsbPage(string title)
        {
            var SourceList = new List<Item>();
            SourceList.Add(new Item("Client or Accessory", Util.IsSupport("http://tizen.org/feature/usb.accessory")));
            SourceList.Add(new Item("Host", Util.IsSupport("http://tizen.org/feature/usb.host")));

            ListView ItemListView = new ListView()
            {
                ItemsSource = SourceList,
                ItemTemplate = MyItemTemplate.GetInstance(),
            };
            Label tt = new Label();
            tt.Text = title;
            tt.FontSize = 28;
            tt.TextColor = Color.White;
            tt.HeightRequest = 0.075 * App.screenHeight;
            tt.HorizontalTextAlignment = TextAlignment.Center;
            Content = new StackLayout()
            {
                Children =
                        {
                    tt,
                    ItemListView,
                        }

            };
        }

    }
    /// <summary>
    /// a class about Vision info
    /// </summary>
    public class VisionPage : ContentPage
    {
        /// <summary>
        /// a construct method for VisionPage
        /// </summary>
        /// <param name="title">title</param>
        public VisionPage(string title)
        {
            var SourceList = new List<Item>();
            SourceList.Add(new Item("Face Recognition", Util.IsSupport("http://tizen.org/feature/vision.face_recognition")));
            SourceList.Add(new Item("Image Recognition", Util.IsSupport("http://tizen.org/feature/vision.image_recognition")));
            SourceList.Add(new Item("QR Code Generation", Util.IsSupport("http://tizen.org/feature/vision.qrcode_generation")));
            SourceList.Add(new Item("QR Code Recognition", Util.IsSupport("http://tizen.org/feature/vision.qrcode_recognition")));

            ListView ItemListView = new ListView()
            {
                ItemsSource = SourceList,
                ItemTemplate = MyItemTemplate.GetInstance(),
            };
            Label tt = new Label();
            tt.Text = title;
            tt.FontSize = 28;
            tt.TextColor = Color.White;
            tt.HeightRequest = 0.075 * App.screenHeight;
            tt.HorizontalTextAlignment = TextAlignment.Center;
            Content = new StackLayout()
            {
                Children =
                        {
                    tt,
                    ItemListView,
                        }

            };

        }
    }

    /// <summary>
    /// a class about System info
    /// </summary>
    public class SystemPage : ContentPage
    {
        /// <summary>
        /// a construct method for SystemPage
        /// </summary>
        /// <param name="title">title</param>
        public SystemPage(string title)
        {
            var SourceList = new List<Item>();
            SourceList.Add(new Item("Tizen ID", Util.IsSupport("http://tizen.org/system/tizenid")));
            SourceList.Add(new Item("Build Date", Util.IsSupport("http://tizen.org/system/build.date")));
            SourceList.Add(new Item("Build Information", Util.IsSupport("http://tizen.org/system/build.string")));
            SourceList.Add(new Item("Build Time", Util.IsSupport("http://tizen.org/system/build.time")));
            SourceList.Add(new Item("Model Name", Util.IsSupport("http://tizen.org/system/model_name")));
            SourceList.Add(new Item("Communication Processor", Util.IsSupport("http://tizen.org/system/platform.communication_processor")));
            SourceList.Add(new Item("Platform Name", Util.IsSupport("http://tizen.org/system/platform.name")));
            SourceList.Add(new Item("Processor Name", Util.IsSupport("http://tizen.org/system/platform.processor")));
            SourceList.Add(new Item("Manufacturer", Util.IsSupport("http://tizen.org/system/manufacturer")));

            ListView ItemListView = new ListView()
            {
                ItemsSource = SourceList,
                ItemTemplate = MyItemTemplate.GetInstance(),
            };
            Label tt = new Label();
            tt.Text = title;
            tt.FontSize = 28;
            tt.TextColor = Color.White;
            tt.HeightRequest = 0.075 * App.screenHeight;
            tt.HorizontalTextAlignment = TextAlignment.Center;
            Content = new StackLayout()
            {
                Children =
                        {
                    tt,
                    ItemListView,
                        }

            };

        }
    }
    /// <summary>
    /// a class about Others info
    /// </summary>
    public class OthersPage : ContentPage
    {
        /// <summary>
        /// a construct method for OthersPage
        /// </summary>
        /// <param name="title">title</param>
        public OthersPage(string title)
        {
            var SourceList = new List<Item>();
            SourceList.Add(new Item("Profile", Util.IsSupport("http://tizen.org/feature/profile")));
            SourceList.Add(new Item("FM Radio", Util.IsSupport("http://tizen.org/feature/fmradio")));
            SourceList.Add(new Item("Graphics Acceleration", Util.IsSupport("http://tizen.org/feature/graphics.acceleration")));
            SourceList.Add(new Item("LED", Util.IsSupport("http://tizen.org/feature/led")));
            SourceList.Add(new Item("Microphone", Util.IsSupport("http://tizen.org/feature/microphone")));
            SourceList.Add(new Item("Transcoder", Util.IsSupport("http://tizen.org/feature/multimedia.transcoder")));
            SourceList.Add(new Item("AppWidget", Util.IsSupport("http://tizen.org/feature/shell.appwidget")));
            SourceList.Add(new Item("VoIP", Util.IsSupport("http://tizen.org/feature/sip.voip")));

            ListView ItemListView = new ListView()
            {
                ItemsSource = SourceList,
                ItemTemplate = MyItemTemplate.GetInstance(),
            };
            Label tt = new Label();
            tt.Text = title;
            tt.FontSize = 28;
            tt.TextColor = Color.White;
            tt.HeightRequest = 0.075 * App.screenHeight;
            tt.HorizontalTextAlignment = TextAlignment.Center;
            Content = new StackLayout()
            {
                Children =
                        {
                    tt,
                    ItemListView,
                        }

            };



        }


    }
    /// <summary>
    /// a class for start page
    /// </summary>
    public class StartPage : ContentPage
    {
        /// <summary>
        /// a construct method for StartPage
        /// </summary>
        public StartPage()
        {
            var SourceList = new List<String>();
            SourceList.Add("Camera");
            SourceList.Add("keyboard");
            SourceList.Add("Location");
            SourceList.Add("Network");
            SourceList.Add("OpenGL");
            SourceList.Add("Platform");
            SourceList.Add("Screen");
            SourceList.Add("Sensors");
            SourceList.Add("Speech");
            SourceList.Add("Usb");
            SourceList.Add("Vision");
            SourceList.Add("System");
            SourceList.Add("Others");

            var MainListView = new ListView()
            {
                ItemsSource = SourceList,
            };

            MainListView.ItemTapped += async (s, a) =>
            {
                if (a.Item.ToString() == "Camera")
                {
                    await Navigation.PushModalAsync(new CameraPage(a.Item.ToString()));
                }
                else if (a.Item.ToString() == "keyboard")
                {
                    await Navigation.PushModalAsync(new KeyboardPage(a.Item.ToString()));
                }
                else if (a.Item.ToString() == "Location")
                {
                    await Navigation.PushModalAsync(new LocationPage(a.Item.ToString()));
                }
                else if (a.Item.ToString() == "Network")
                {
                    await Navigation.PushModalAsync(new NetworkPage(a.Item.ToString()));
                }
                else if (a.Item.ToString() == "OpenGL")
                {
                    await Navigation.PushModalAsync(new OpenGLPage(a.Item.ToString()));
                }
                else if (a.Item.ToString() == "Platform")
                {
                    await Navigation.PushModalAsync(new PlatformPage(a.Item.ToString()));
                }
                else if (a.Item.ToString() == "Screen")
                {
                    await Navigation.PushModalAsync(new ScreenPage(a.Item.ToString()));
                }
                else if (a.Item.ToString() == "Sensors")
                {
                    await Navigation.PushModalAsync(new SensorsPage(a.Item.ToString()));
                }
                else if (a.Item.ToString() == "Speech")
                {
                    await Navigation.PushModalAsync(new SpeechPage(a.Item.ToString()));
                }
                else if (a.Item.ToString() == "Usb")
                {
                    await Navigation.PushModalAsync(new UsbPage(a.Item.ToString()));
                }
                else if (a.Item.ToString() == "Vision")
                {
                    await Navigation.PushModalAsync(new VisionPage(a.Item.ToString()));
                }
                else if (a.Item.ToString() == "System")
                {
                    await Navigation.PushModalAsync(new SystemPage(a.Item.ToString()));
                }
                else if (a.Item.ToString() == "Others")
                {
                    await Navigation.PushModalAsync(new OthersPage(a.Item.ToString()));
                }
            };
            Title = "System Info";
            Label title = new Label();
            title.Text = "System Info";
            title.TextColor = Color.White;
            title.FontSize = 28;
            title.HeightRequest = 0.075 * App.screenHeight;
            title.HorizontalTextAlignment = TextAlignment.Center;
            Content = new StackLayout
            {
                Children =
                {
                   title,MainListView,
                }

            };
        }
    }

    /// <summary>
    /// a class for Camera page
    /// </summary>
    public class CameraPage : ContentPage
    {

        /// <summary>
        /// a construct method for CameraPage
        /// </summary>
        /// <param name="title">title</param>
        public CameraPage(string title)
        {

            var SourceList = new List<Item>();
            SourceList.Add(new Item("Camera", Util.IsSupport("http://tizen.org/feature/camera")));
            SourceList.Add(new Item("Back Camera", Util.IsSupport("http://tizen.org/feature/camera.back")));
            SourceList.Add(new Item("Back Camera Flash", Util.IsSupport("http://tizen.org/feature/camera.back.flash")));
            SourceList.Add(new Item("Front Camera", Util.IsSupport("http://tizen.org/feature/camera.front")));
            SourceList.Add(new Item("Front Camera Flash", Util.IsSupport("http://tizen.org/feature/camera.front.flash")));
            ListView ItemListView = new ListView()
            {
                ItemsSource = SourceList,
                ItemTemplate = MyItemTemplate.GetInstance(),
            };
            Label tt = new Label();
            tt.Text = title;
            tt.FontSize = 28;
            tt.TextColor = Color.White;
            tt.HeightRequest = 0.075 * App.screenHeight;
            tt.HorizontalTextAlignment = TextAlignment.Center;
            Content = new StackLayout()
            {
                Children =
                {
                    tt,
                    ItemListView,
                }
            };
        }
    }

    /// <summary>
    /// a class for item
    /// </summary>
    public class Item
    {
        public string MainText { get; set; }
        public string SubText { get; set; }
        public Item(string main, string sub)
        {
            MainText = main;
            SubText = sub;
        }

    }
    /// <summary>
    /// a class for custom datatemplate
    /// </summary>
    public class MyItemTemplate
    {

        /// <summary>
        /// return a instance of Custom datatemplate
        /// </summary>
        /// <returns>DataTemplate</returns>
        public static DataTemplate GetInstance()
        {
            return new DataTemplate(() =>
            {
                TextCell TextCellItem = new TextCell()
                {
                    TextColor = Color.Black,
                    DetailColor = Color.Gray
                };

                TextCellItem.SetBinding(TextCell.TextProperty, "MainText");
                TextCellItem.SetBinding(TextCell.DetailProperty, "SubText");

                return TextCellItem;
            });


        }

    }

    /// <summary>
    /// a class for Group info
    /// </summary>
    public class Group : List<Item>
    {
        public string Name { get; set; }
        public Group(string name)
        {
            Name = name;
        }

    }
}